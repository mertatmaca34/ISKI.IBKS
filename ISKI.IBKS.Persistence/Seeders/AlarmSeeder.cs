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
        // Cooldown values: 1 (1dk), 10 (10dk), 30 (30dk), 60 (1 saat), 180 (3 saat), 1440 (1 gün)
        if (!await context.AlarmDefinitions.AnyAsync())
        {
            var alarms = new List<AlarmDefinition>();

            // --- GRUP 1: KRİTİK GÜVENLİK VE ORTAM ---
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Kabin Su Baskını Alarmı", "KabinSuBaskini", false, "Kabin zemininde su tespit edildi.", cooldownMinutes: 10));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Kabin Duman/Yangın Alarmı", "KabinDuman", false, "Kabin içinde duman dedektörü aktif oldu.", cooldownMinutes: 10));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Acil Stop Butonu Basıldı", "KabinAcilStopBasili", false, "Sahada operatör tarafından Acil Stop butonuna basıldı.", cooldownMinutes: 10));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Kabin Kapısı İzinsiz Açıldı", "KabinKapiAcildi", false, "Yetkisiz veya beklenmedik bir zamanda kabin kapısı açıldı.", cooldownMinutes: 30));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Şebeke Enerjisi Kesintisi", "KabinEnerjiYok", false, "Ana şebeke enerjisi kesildi, sistem UPS üzerinden besleniyor.", cooldownMinutes: 30));

            // --- GRUP 2: MEKANİK VE DONANIM ---
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Numune Pompası 1 (Pompa 1) Termik Arızası", "Pompa1Termik", false, "1. Pompa aşırı ısındı veya termik attı.", cooldownMinutes: 30));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Numune Pompası 2 (Pompa 2) Termik Arızası", "Pompa2Termik", false, "2. Pompa aşırı ısındı veya termik attı.", cooldownMinutes: 30));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Temiz Su Pompası (Pompa 3) Termik Arızası", "Pompa3Termik", false, "Yıkama işleminde kullanılan temiz su pompası arızalı.", cooldownMinutes: 30));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Yıkama Tankı Boş Uyarısı", "TankDolu", true, "Temiz su tankında su bitti. Otomatik yıkama yapılamayacak.", cooldownMinutes: 60)); // True=Dolu (Expected), False=Bos (Alarm)

            // Analog Donanım
            alarms.Add(AlarmDefinition.CreateThresholdAlarm("Kabin İçi Yüksek Sıcaklık", "KabinSicaklik", null, 40.0, "Kabin içi sıcaklık elektronik kartlara zarar verebilecek seviyeye ulaştı.", cooldownMinutes: 30));
            alarms.Add(AlarmDefinition.CreateThresholdAlarm("UPS Kapasitesi Kritik Seviyede", "UpsKapasite", 20.0, null, "Enerji kesintisi devam ediyor, UPS aküleri bitmek üzere.", cooldownMinutes: 30)); // Min threshold kontrolü (eğer < 20 ise alarm)

            // --- GRUP 3: PROSES VE SU KALİTESİ ---
            // Varsayılan limitler (Kullanıcı panelden düzeltebilir)
            alarms.Add(AlarmDefinition.CreateThresholdAlarm("pH Yüksek Alarmı", "Ph", null, 9.0, "Çıkış suyu pH değeri yasal üst sınırı aştı.", cooldownMinutes: 60));
            alarms.Add(AlarmDefinition.CreateThresholdAlarm("pH Düşük Alarmı", "Ph", 6.0, null, "Çıkış suyu pH değeri yasal alt sınırın altına düştü.", cooldownMinutes: 60));
            alarms.Add(AlarmDefinition.CreateThresholdAlarm("KOİ Sınır Aşımı", "Koi", null, 120.0, "Su kirlilik oranı (KOİ) belirlenen limitin üzerinde.", cooldownMinutes: 60));
            alarms.Add(AlarmDefinition.CreateThresholdAlarm("AKM Sınır Aşımı", "Akm", null, 50.0, "AKM değeri limitin üzerinde.", cooldownMinutes: 60));
            alarms.Add(AlarmDefinition.CreateThresholdAlarm("İletkenlik Yüksek Alarmı", "Iletkenlik", null, 2000.0, "Suyun iletkenlik değeri normalin üzerinde.", cooldownMinutes: 60));
            alarms.Add(AlarmDefinition.CreateThresholdAlarm("Çözünmüş Oksijen Düşük", "CozunmusOksijen", 2.0, null, "Sudaki oksijen miktarı kritik seviyenin altında.", cooldownMinutes: 60));

            // --- GRUP 4: SİSTEM VE İLETİŞİM ---
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("PLC Bağlantı Hatası", "PLC Bağlantı", true, "Masaüstü uygulaması PLC ile haberleşemiyor.", cooldownMinutes: 10)); // True=Connected
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("SAIS API Veri Gönderim Hatası", "SAIS API", true, "Veriler bakanlık sunucularına gönderilemiyor.", cooldownMinutes: 180)); // True=Success
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Kritik Veri Gönderim Süresi Aşımı", "Veri Gönderim", true, "Veri gönderilememe süresi 24 saati aştı.", cooldownMinutes: 1440)); // True=Success
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Kalibrasyon Başarısız", "Kalibrasyon", true, "Sensör kalibrasyonu tutmadı (Sapma > %10).", cooldownMinutes: 60)); // True=Success

            // --- GRUP 5: OPERASYONEL VE NUMUNE ---
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("AKM Eşik Aşımı (PLC Numune)", "AkmTetik", false, "PLC tarafından AKM eşik aşımı tespit edildi ve numune alımı başlatıldı.", cooldownMinutes: 60));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("KOİ Eşik Aşımı (PLC Numune)", "KoiTetik", false, "PLC tarafından KOİ eşik aşımı tespit edildi ve numune alımı başlatıldı.", cooldownMinutes: 60));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("pH Eşik Aşımı (PLC Numune)", "PhTetik", false, "PLC tarafından pH eşik aşımı tespit edildi ve numune alımı başlatıldı.", cooldownMinutes: 60));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Simülasyon Numune Tetik", "SimNumuneTetik", false, "Sistem testi için simülasyon numune alımı başlatıldı.", cooldownMinutes: 10));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm("Numune Alımı Başlatıldı", "Numune Alma Cihazı", false, "Sistem tarafından numune alma komutu gönderildi.", cooldownMinutes: 30));

            context.AlarmDefinitions.AddRange(alarms);
            await context.SaveChangesAsync();
        }
    }
}
