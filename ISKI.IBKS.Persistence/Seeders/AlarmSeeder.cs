using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ISKI.IBKS.Persistence.Seeders;

public static class AlarmSeeder
{
    public static async Task SeedAsync(IbksDbContext context)
    {
        // 1. Default Admin User
        if (!await context.AlarmUsers.AnyAsync())
        {
            var adminUser = new AlarmUser(
                fullName: "Admin",
                email: "admin@iski.istanbul",
                phoneNumber: null,
                department: "Yönetim",
                title: "Yönetici"
            );
            
            context.AlarmUsers.Add(adminUser);
            await context.SaveChangesAsync();
        }

        // 2. Alarm Definitions (21 Critical Alarms)
        if (!await context.AlarmDefinitions.AnyAsync())
        {
            var alarms = new List<AlarmDefinition>();

            // --- GRUP 1: KRİTİK GÜVENLİK VE ORTAM ---
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Kabin Su Baskını Alarmı", "KabinSuBaskini", false, "Kabin zemininde su tespit edildi.", AlarmPriority.Critical));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Kabin Duman/Yangın Alarmı", "KabinDuman", false, "Kabin içinde duman dedektörü aktif oldu.", AlarmPriority.Critical));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Acil Stop Butonu Basıldı", "KabinAcilStopBasili", false, "Sahada operatör tarafından Acil Stop butonuna basıldı.", AlarmPriority.Critical));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Kabin Kapısı İzinsiz Açıldı", "KabinKapiAcildi", false, "Yetkisiz veya beklenmedik bir zamanda kabin kapısı açıldı.", AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Şebeke Enerjisi Kesintisi", "KabinEnerjiYok", false, "Ana şebeke enerjisi kesildi, sistem UPS üzerinden besleniyor.", AlarmPriority.High));

            // --- GRUP 2: MEKANİK VE DONANIM ---
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Numune Pompası 1 (Pompa 1) Termik Arızası", "Pompa1Termik", false, "1. Pompa aşırı ısındı veya termik attı.", AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Numune Pompası 2 (Pompa 2) Termik Arızası", "Pompa2Termik", false, "2. Pompa aşırı ısındı veya termik attı.", AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Temiz Su Pompası (Pompa 3) Termik Arızası", "Pompa3Termik", false, "Yıkama işleminde kullanılan temiz su pompası arızalı.", AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Yıkama Tankı Boş Uyarısı", "TankDolu", true, "Temiz su tankında su bitti. Otomatik yıkama yapılamayacak.", AlarmPriority.Medium)); // True=Dolu (Expected), False=Bos (Alarm)

            // Analog Donanım
            alarms.Add(AlarmDefinition.CreateThresholdAlarm("Kabin İçi Yüksek Sıcaklık", "KabinSicaklik", null, 40.0, "Kabin içi sıcaklık elektronik kartlara zarar verebilecek seviyeye ulaştı.", AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateThresholdAlarm("UPS Kapasitesi Kritik Seviyede", "UpsKapasite", 20.0, null, "Enerji kesintisi devam ediyor, UPS aküleri bitmek üzere.", AlarmPriority.High)); // Min threshold kontrolü (eğer < 20 ise alarm)

            // --- GRUP 3: PROSES VE SU KALİTESİ ---
            // Varsayılan limitler (Kullanıcı panelden düzeltebilir)
            alarms.Add(AlarmDefinition.CreateThresholdAlarm("pH Yüksek Alarmı", "Ph", null, 9.0, "Çıkış suyu pH değeri yasal üst sınırı aştı.", AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateThresholdAlarm("pH Düşük Alarmı", "Ph", 6.0, null, "Çıkış suyu pH değeri yasal alt sınırın altına düştü.", AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateThresholdAlarm("KOİ Sınır Aşımı", "Koi", null, 120.0, "Su kirlilik oranı (KOİ) belirlenen limitin üzerinde.", AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateThresholdAlarm("AKM Sınır Aşımı", "Akm", null, 50.0, "AKM değeri limitin üzerinde.", AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateThresholdAlarm("İletkenlik Yüksek Alarmı", "Iletkenlik", null, 2000.0, "Suyun iletkenlik değeri normalin üzerinde.", AlarmPriority.Medium));
            alarms.Add(AlarmDefinition.CreateThresholdAlarm("Çözünmüş Oksijen Düşük", "CozunmusOksijen", 2.0, null, "Sudaki oksijen miktarı kritik seviyenin altında.", AlarmPriority.Medium));

            // --- GRUP 4: SİSTEM VE İLETİŞİM ---
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("PLC Bağlantı Hatası", "PLC Bağlantı", true, "Masaüstü uygulaması PLC ile haberleşemiyor.", AlarmPriority.Critical)); // True=Connected
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("SAIS API Veri Gönderim Hatası", "SAIS API", true, "Veriler bakanlık sunucularına gönderilemiyor.", AlarmPriority.Critical)); // True=Success
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Kritik Veri Gönderim Süresi Aşımı", "Veri Gönderim", true, "Veri gönderilememe süresi 24 saati aştı.", AlarmPriority.Critical)); // True=Success
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Kalibrasyon Başarısız", "Kalibrasyon", true, "Sensör kalibrasyonu tutmadı (Sapma > %10).", AlarmPriority.High)); // True=Success

            // --- GRUP 5: OPERASYONEL ---
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Numune Alımı Başlatıldı", "Numune Alma Cihazı", false, "Sistem tarafından numune alma komutu gönderildi.", AlarmPriority.Medium));

            context.AlarmDefinitions.AddRange(alarms);
            await context.SaveChangesAsync();
        }
    }
}
