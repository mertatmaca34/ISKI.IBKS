using Microsoft.Data.Sqlite;
using System;

namespace ISKI.IBKS.DatabaseMigrator
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbPath = @"C:\Users\Lenovo\source\repos\ISKI.IBKS\ISKI.IBKS.Presentation.WinForms\ibks.db";
            var connectionString = $"Data Source={dbPath}";

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS AlarmDefinitions (
                    Id TEXT PRIMARY KEY,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    SensorName TEXT,
                    Type INTEGER NOT NULL,
                    MinThreshold REAL,
                    MaxThreshold REAL,
                    ExpectedDigitalValue INTEGER,
                    IsActive INTEGER NOT NULL,
                    Priority INTEGER NOT NULL,
                    CreatedWhen TEXT,
                    CreatedBy TEXT,
                    LastModifiedWhen TEXT,
                    LastModifiedBy TEXT
                );

                -- pH Alarms
                INSERT INTO AlarmDefinitions (Id, Name, Description, SensorName, Type, MinThreshold, MaxThreshold, Priority, IsActive, CreatedWhen, CreatedBy)
                SELECT lower(hex(randomblob(4)) || '-' || hex(randomblob(2)) || '-4' || substr(hex(randomblob(2)),2) || '-' || substr('89ab',abs(random()) % 4 + 1, 1) || substr(hex(randomblob(2)),2) || '-' || hex(randomblob(6))), 
                'pH Yüksek Limit Alarmı', 'pH değeri kritik seviyenin (9.0) üzerine çıktı.', 'pH', 0, NULL, 9.0, 2, 1, date('now'), 'System'
                WHERE NOT EXISTS (SELECT 1 FROM AlarmDefinitions WHERE Name = 'pH Yüksek Limit Alarmı');

                INSERT INTO AlarmDefinitions (Id, Name, Description, SensorName, Type, MinThreshold, MaxThreshold, Priority, IsActive, CreatedWhen, CreatedBy)
                SELECT lower(hex(randomblob(4)) || '-' || hex(randomblob(2)) || '-4' || substr(hex(randomblob(2)),2) || '-' || substr('89ab',abs(random()) % 4 + 1, 1) || substr(hex(randomblob(2)),2) || '-' || hex(randomblob(6))), 
                'pH Düşük Limit Alarmı', 'pH değeri kritik seviyenin (6.0) altına düştü.', 'pH', 0, 6.0, NULL, 2, 1, date('now'), 'System'
                WHERE NOT EXISTS (SELECT 1 FROM AlarmDefinitions WHERE Name = 'pH Düşük Limit Alarmı');

                -- Conductivity Alarms
                INSERT INTO AlarmDefinitions (Id, Name, Description, SensorName, Type, MinThreshold, MaxThreshold, Priority, IsActive, CreatedWhen, CreatedBy)
                SELECT lower(hex(randomblob(4)) || '-' || hex(randomblob(2)) || '-4' || substr(hex(randomblob(2)),2) || '-' || substr('89ab',abs(random()) % 4 + 1, 1) || substr(hex(randomblob(2)),2) || '-' || hex(randomblob(6))), 
                'İletkenlik Yüksek Uyarısı', 'İletkenlik değeri 2000 uS/cm sınırını aştı.', 'İletkenlik', 0, NULL, 2000.0, 1, 1, date('now'), 'System'
                WHERE NOT EXISTS (SELECT 1 FROM AlarmDefinitions WHERE Name = 'İletkenlik Yüksek Uyarısı');

                -- AKM Alarms
                INSERT INTO AlarmDefinitions (Id, Name, Description, SensorName, Type, MinThreshold, MaxThreshold, Priority, IsActive, CreatedWhen, CreatedBy)
                SELECT lower(hex(randomblob(4)) || '-' || hex(randomblob(2)) || '-4' || substr(hex(randomblob(2)),2) || '-' || substr('89ab',abs(random()) % 4 + 1, 1) || substr(hex(randomblob(2)),2) || '-' || hex(randomblob(6))), 
                'AKM Kritik Seviye', 'Askıda Katı Madde sensörü yüksek değer okuyor (>50 mg/L).', 'AKM', 0, NULL, 50.0, 3, 1, date('now'), 'System'
                WHERE NOT EXISTS (SELECT 1 FROM AlarmDefinitions WHERE Name = 'AKM Kritik Seviye');

                -- KOI Alarms
                INSERT INTO AlarmDefinitions (Id, Name, Description, SensorName, Type, MinThreshold, MaxThreshold, Priority, IsActive, CreatedWhen, CreatedBy)
                SELECT lower(hex(randomblob(4)) || '-' || hex(randomblob(2)) || '-4' || substr(hex(randomblob(2)),2) || '-' || substr('89ab',abs(random()) % 4 + 1, 1) || substr(hex(randomblob(2)),2) || '-' || hex(randomblob(6))), 
                'KOİ Limit Aşımı', 'Kimyasal Oksijen İhtiyacı 100 mg/L üzerine çıktı.', 'KOİ', 0, NULL, 100.0, 3, 1, date('now'), 'System'
                WHERE NOT EXISTS (SELECT 1 FROM AlarmDefinitions WHERE Name = 'KOİ Limit Aşımı');

                -- System Alarms (Simulated Digital)
                INSERT INTO AlarmDefinitions (Id, Name, Description, SensorName, Type, ExpectedDigitalValue, Priority, IsActive, CreatedWhen, CreatedBy)
                SELECT lower(hex(randomblob(4)) || '-' || hex(randomblob(2)) || '-4' || substr(hex(randomblob(2)),2) || '-' || substr('89ab',abs(random()) % 4 + 1, 1) || substr(hex(randomblob(2)),2) || '-' || hex(randomblob(6))), 
                'Ana Pompa Arızası', 'Ana besleme pompası çalışmıyor (Sinyal 0).', 'Ana Pompa', 1, 1, 3, 1, date('now'), 'System'
                WHERE NOT EXISTS (SELECT 1 FROM AlarmDefinitions WHERE Name = 'Ana Pompa Arızası');

                INSERT INTO AlarmDefinitions (Id, Name, Description, SensorName, Type, ExpectedDigitalValue, Priority, IsActive, CreatedWhen, CreatedBy)
                SELECT lower(hex(randomblob(4)) || '-' || hex(randomblob(2)) || '-4' || substr(hex(randomblob(2)),2) || '-' || substr('89ab',abs(random()) % 4 + 1, 1) || substr(hex(randomblob(2)),2) || '-' || hex(randomblob(6))), 
                'Şebeke Enerjisi Kesintisi', 'Sistem UPS modunda çalışıyor.', 'Enerji', 1, 1, 2, 1, date('now'), 'System'
                WHERE NOT EXISTS (SELECT 1 FROM AlarmDefinitions WHERE Name = 'Şebeke Enerjisi Kesintisi');

                INSERT INTO AlarmDefinitions (Id, Name, Description, SensorName, Type, ExpectedDigitalValue, Priority, IsActive, CreatedWhen, CreatedBy)
                SELECT lower(hex(randomblob(4)) || '-' || hex(randomblob(2)) || '-4' || substr(hex(randomblob(2)),2) || '-' || substr('89ab',abs(random()) % 4 + 1, 1) || substr(hex(randomblob(2)),2) || '-' || hex(randomblob(6))), 
                'PLC İletişim Hatası', 'PLC ile bağlantı koptu.', 'PLC', 2, 1, 3, 1, date('now'), 'System'
                WHERE NOT EXISTS (SELECT 1 FROM AlarmDefinitions WHERE Name = 'PLC İletişim Hatası');
            ";
            

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("✅ Database updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
        }
    }
}
