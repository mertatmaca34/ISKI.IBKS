using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Domain.Enums;
using ISKI.IBKS.Infrastructure.Persistence.Contexts;
using ISKI.IBKS.Shared.Localization;
using Microsoft.EntityFrameworkCore;

namespace ISKI.IBKS.Infrastructure.Persistence.Seeders;

public static class AlarmSeeder
{
    public static async Task SeedAsync(IbksDbContext context)
    {
        if (!await context.AlarmUsers.AnyAsync())
        {
            var adminUser = new AlarmUser(
                fullName: "admin",
                email: "mertatmaca34@gmail.com",
                phoneNumber: null,
                department: Strings.Seeder_Dept_Yazilim,
                title: Strings.Seeder_Title_YazilimGelistiricisi
            );

            context.AlarmUsers.Add(adminUser);
            await context.SaveChangesAsync();
        }

        if (!await context.AlarmDefinitions.AnyAsync())
        {
            var alarms = new List<AlarmDefinition>();

            alarms.Add(AlarmDefinition.CreateDigitalAlarm(Strings.Alarm_KabinSuBaskini_Name, "KabinSuBaskini", false, Strings.Alarm_KabinSuBaskini_Desc, AlarmPriority.Critical));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm(Strings.Alarm_KabinDuman_Name, "KabinDuman", false, Strings.Alarm_KabinDuman_Desc, AlarmPriority.Critical));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm(Strings.Alarm_AcilStop_Name, "KabinAcilStopBasili", false, Strings.Alarm_AcilStop_Desc, AlarmPriority.Critical));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm(Strings.Alarm_KabinKapi_Name, "KabinKapiAcildi", false, Strings.Alarm_KabinKapi_Desc, AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm(Strings.Alarm_SebekeEnerji_Name, "KabinEnerjiYok", false, Strings.Alarm_SebekeEnerji_Desc, AlarmPriority.High));

            alarms.Add(AlarmDefinition.CreateDigitalAlarm(Strings.Alarm_Pompa1Termik_Name, "Pompa1Termik", false, Strings.Alarm_Pompa1Termik_Desc, AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm(Strings.Alarm_Pompa2Termik_Name, "Pompa2Termik", false, Strings.Alarm_Pompa2Termik_Desc, AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm(Strings.Alarm_Pompa3Termik_Name, "Pompa3Termik", false, Strings.Alarm_Pompa3Termik_Desc, AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm(Strings.Alarm_TankBos_Name, "TankDolu", true, Strings.Alarm_TankBos_Desc, AlarmPriority.Medium));

            alarms.Add(AlarmDefinition.CreateThresholdAlarm(Strings.Alarm_KabinSicaklik_Name, "KabinSicaklik", null, 40.0, Strings.Alarm_KabinSicaklik_Desc, AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateThresholdAlarm(Strings.Alarm_UpsKapasite_Name, "UpsKapasite", 20.0, null, Strings.Alarm_UpsKapasite_Desc, AlarmPriority.High));

            alarms.Add(AlarmDefinition.CreateThresholdAlarm(Strings.Alarm_PhYuksek_Name, "Ph", null, 9.0, Strings.Alarm_PhYuksek_Desc, AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateThresholdAlarm(Strings.Alarm_PhDusuk_Name, "Ph", 6.0, null, Strings.Alarm_PhDusuk_Desc, AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateThresholdAlarm(Strings.Alarm_KoiSinirAsimi_Name, "Koi", null, 120.0, Strings.Alarm_KoiSinirAsimi_Desc, AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateThresholdAlarm(Strings.Alarm_AkmSinirAsimi_Name, "Akm", null, 50.0, Strings.Alarm_AkmSinirAsimi_Desc, AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateThresholdAlarm(Strings.Alarm_IletkenlikYuksek_Name, "Iletkenlik", null, 2000.0, Strings.Alarm_IletkenlikYuksek_Desc, AlarmPriority.Medium));
            alarms.Add(AlarmDefinition.CreateThresholdAlarm(Strings.Alarm_OksijenDusuk_Name, "CozunmusOksijen", 2.0, null, Strings.Alarm_OksijenDusuk_Desc, AlarmPriority.Medium));

            alarms.Add(AlarmDefinition.CreateDigitalAlarm(Strings.Alarm_PlcBaglanti_Name, "PLC Bağlantı", true, Strings.Alarm_PlcBaglanti_Desc, AlarmPriority.Critical));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm(Strings.Alarm_SaisApi_Name, "SAIS API", true, Strings.Alarm_SaisApi_Desc, AlarmPriority.Critical));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm(Strings.Alarm_VeriGonderimAsimi_Name, "Veri Gönderim", true, Strings.Alarm_VeriGonderimAsimi_Desc, AlarmPriority.Critical));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm(Strings.Alarm_KalibrasyonBasarisiz_Name, "Kalibrasyon", true, Strings.Alarm_KalibrasyonBasarisiz_Desc, AlarmPriority.High));

            alarms.Add(AlarmDefinition.CreateDigitalAlarm(Strings.Alarm_AkmTetik_Name, "AkmTetik", false, Strings.Alarm_AkmTetik_Desc, AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm(Strings.Alarm_KoiTetik_Name, "KoiTetik", false, Strings.Alarm_KoiTetik_Desc, AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm(Strings.Alarm_PhTetik_Name, "PhTetik", false, Strings.Alarm_PhTetik_Desc, AlarmPriority.High));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm(Strings.Alarm_SimNumuneTetik_Name, "SimNumuneTetik", false, Strings.Alarm_SimNumuneTetik_Desc, AlarmPriority.Low));
            alarms.Add(AlarmDefinition.CreateDigitalAlarm(Strings.Alarm_NumuneAlma_Name, "Numune Alma Cihazı", false, Strings.Alarm_NumuneAlma_Desc, AlarmPriority.Medium));

            context.AlarmDefinitions.AddRange(alarms);
            await context.SaveChangesAsync();
        }
    }
}

