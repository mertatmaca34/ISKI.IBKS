using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ISKI.IBKS.Persistence.Contexts;
using ISKI.IBKS.Domain.Entities; // Added for LogLevel enum

namespace ISKI.IBKS.Presentation.WinForms.Features.ReportingPage
{
    public partial class ReportingPage : UserControl
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Microsoft.Extensions.Logging.ILogger<ReportingPage> _logger;

        public ReportingPage(IServiceScopeFactory scopeFactory, Microsoft.Extensions.Logging.ILogger<ReportingPage> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            InitializeComponent();
            InitializeControls();
            AttachEvents();
        }

        private void InitializeControls()
        {
            // Set double buffering for smoother UI
            this.DoubleBuffered = true;
            typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(DataGridViewDatas, true, null);

            // Set default values
            if (ComboBoxReportType != null)
            {
                ComboBoxReportType.SelectedIndex = 0;
            }

            if (RadioButtonDaily != null) RadioButtonDaily.Checked = true;
            if (RadioButtonSortByLast != null) RadioButtonSortByLast.Checked = true;

            // Set date ranges to today
            if (DateTimePickerFirstDate != null) DateTimePickerFirstDate.Value = DateTime.Today;
            if (DateTimePickerFirstTime != null) DateTimePickerFirstTime.Value = DateTime.Today;
            if (DateTimePickerLastDate != null) DateTimePickerLastDate.Value = DateTime.Today;
            if (DateTimePickerLastTime != null) DateTimePickerLastTime.Value = DateTime.Today.AddHours(23).AddMinutes(59);

            // Button hover effects
            if (ButtonGenerate != null)
            {
                ButtonGenerate.MouseEnter += (s, e) => ButtonGenerate.BackColor = Color.FromArgb(0, 100, 190);
                ButtonGenerate.MouseLeave += (s, e) => ButtonGenerate.BackColor = Color.FromArgb(0, 120, 215);
                ButtonGenerate.BackColor = Color.FromArgb(0, 120, 215); // Sync with designer update
            }

            // Apply initial period logic
            UpdateDateRangeByPeriod();
        }

        private void AttachEvents()
        {
            if (ButtonGenerate != null) ButtonGenerate.Click += ButtonGenerate_Click;
            if (ButtonSaveAsExcel != null) ButtonSaveAsExcel.Click += ButtonSaveAsExcel_Click;
            if (ButtonSaveAsPdf != null) ButtonSaveAsPdf.Click += ButtonSaveAsPdf_Click;

            // Enable custom date range when "Özel" is selected
            if (RadioButtonCustom != null)
            {
                RadioButtonCustom.CheckedChanged += (s, e) =>
                {
                    if (GroupBoxDate != null)
                        GroupBoxDate.Enabled = RadioButtonCustom.Checked;
                };
            }

            // Set date ranges based on period selection
            if (RadioButtonDaily != null) RadioButtonDaily.CheckedChanged += (s, e) => { if (RadioButtonDaily.Checked) UpdateDateRangeByPeriod(); };
            if (RadioButtonWeekly != null) RadioButtonWeekly.CheckedChanged += (s, e) => { if (RadioButtonWeekly.Checked) UpdateDateRangeByPeriod(); };
            if (RadioButtonMonthly != null) RadioButtonMonthly.CheckedChanged += (s, e) => { if (RadioButtonMonthly.Checked) UpdateDateRangeByPeriod(); };

            if (DataGridViewDatas != null)
            {
                DataGridViewDatas.CellFormatting += DataGridViewDatas_CellFormatting;
            }

            // Show/hide log level filter based on report type
            if (ComboBoxReportType != null)
            {
                ComboBoxReportType.SelectedIndexChanged += (s, e) =>
                {
                    if (GroupBoxLogLevel != null)
                    {
                        GroupBoxLogLevel.Visible = ComboBoxReportType.SelectedItem?.ToString() == "Kayıt";
                    }
                };
            }
        }

        private void DataGridViewDatas_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (DataGridViewDatas == null || e.RowIndex < 0 || e.RowIndex >= DataGridViewDatas.Rows.Count) return;

            var row = DataGridViewDatas.Rows[e.RowIndex];
            
            // "Seviye" kolonu kontrolü (Log seviyesi için renklendirme)
            int levelColIndex = -1;
            foreach(DataGridViewColumn col in DataGridViewDatas.Columns)
            {
                if (col.HeaderText == "Seviye")
                {
                    levelColIndex = col.Index;
                    break;
                }
            }

            if (levelColIndex >= 0)
            {
                var levelCell = row.Cells[levelColIndex].Value;
                if (levelCell != null)
                {
                    // Convert enum to string for display and comparison
                    var levelStr = levelCell.ToString();
                    
                    if (levelStr == "Warning")
                    {
                        // Pastel Yellow/Orange for Warning
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 205); // Pastel yellow
                        row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 193, 7);
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else if (levelStr == "Error" || levelStr == "Critical")
                    {
                        // Pastel Red for Error/Critical
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 205, 210); // Pastel red
                        row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(244, 67, 54);
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
            
            // SAIS Durumu column check (for measurement reports)
            int saisColIndex = -1;
            foreach(DataGridViewColumn col in DataGridViewDatas.Columns)
            {
                if (col.HeaderText == "SAIS Durumu")
                {
                    saisColIndex = col.Index;
                    break;
                }
            }

            if (saisColIndex >= 0)
            {
                var status = row.Cells[saisColIndex].Value?.ToString();
                if (status == "Gönderildi")
                {
                    // Pastel Green
                    row.DefaultCellStyle.BackColor = Color.FromArgb(193, 225, 193); 
                    row.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (status == "Gönderilmedi")
                {
                    // Pastel Orange / Yellow
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 204, 153);
                    row.DefaultCellStyle.SelectionBackColor = Color.OrangeRed;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void UpdateDateRangeByPeriod()
        {
            if (RadioButtonCustom == null || RadioButtonCustom.Checked) return;

            DateTime now = DateTime.Now;
            DateTime start = DateTime.Today;
            DateTime end = DateTime.Today;

            if (RadioButtonDaily.Checked)
            {
                // Bugün 00:00 - Yarın 00:00
                start = DateTime.Today;
                end = DateTime.Today.AddDays(1);
            }
            else if (RadioButtonWeekly.Checked)
            {
                // Son Pazartesi 00:00 - Bir sonraki Pazartesi 00:00
                int diff = (7 + (now.DayOfWeek - DayOfWeek.Monday)) % 7;
                start = DateTime.Today.AddDays(-1 * diff);
                end = start.AddDays(7);
            }
            else if (RadioButtonMonthly.Checked)
            {
                // Bulunduğumuz ayın 1'inden (00:00) bir sonraki ayın 1'ine (00:00)
                start = new DateTime(now.Year, now.Month, 1);
                end = start.AddMonths(1);
            }

            if (DateTimePickerFirstDate != null) DateTimePickerFirstDate.Value = start;
            if (DateTimePickerFirstTime != null) DateTimePickerFirstTime.Value = start;
            if (DateTimePickerLastDate != null) DateTimePickerLastDate.Value = end;
            if (DateTimePickerLastTime != null) DateTimePickerLastTime.Value = end;
        }

        private async void ButtonGenerate_Click(object? sender, EventArgs e)
        {
            var reportType = ComboBoxReportType?.SelectedItem?.ToString() ?? "Ölçüm";
            
            DateTime startDate = DateTimePickerFirstDate?.Value.Date.Add(DateTimePickerFirstTime?.Value.TimeOfDay ?? TimeSpan.Zero) ?? DateTime.Today;
            DateTime endDate = DateTimePickerLastDate?.Value.Date.Add(DateTimePickerLastTime?.Value.TimeOfDay ?? TimeSpan.Zero) ?? DateTime.Today;

            if (startDate > endDate)
            {
                _logger.LogWarning("Rapor oluşturulamadı: Başlangıç tarihi bitiş tarihinden sonra");
                MessageBox.Show("Başlangıç tarihi bitiş tarihinden sonra olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _logger.LogInformation("Rapor oluşturuluyor: Tip={ReportType}, Başlangıç={StartDate}, Bitiş={EndDate}", reportType, startDate, endDate);

            bool sortNewestFirst = RadioButtonSortByLast?.Checked ?? false;
            ButtonGenerate.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                var reportData = await GenerateReportDataAsync(reportType, startDate, endDate, sortNewestFirst);

                if (DataGridViewDatas != null)
                {
                    DataGridViewDatas.DataSource = reportData;
                    
                    if (DataGridViewDatas.Columns.Contains("Tarih"))
                    {
                        DataGridViewDatas.Columns["Tarih"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    
                    DataGridViewDatas.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }

                MessageBox.Show($"{reportData.Rows.Count} kayıt bulundu.", "Rapor Oluşturuldu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Rapor oluşturulurken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ButtonGenerate.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private async Task<DataTable> GenerateReportDataAsync(string reportType, DateTime startDate, DateTime endDate, bool sortNewestFirst)
        {
            var dt = new DataTable();
            
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();

            // Universal Date Sorting Helper
            string sortOrder = sortNewestFirst ? "desc" : "asc";

            switch (reportType)
            {

                case "Ölçüm":
                    dt.Columns.Add("Tarih");
                    dt.Columns.Add("pH");
                    dt.Columns.Add("İletkenlik");
                    dt.Columns.Add("Debi (m³/h)");
                    dt.Columns.Add("Akış Hızı (m/s)"); // New
                    dt.Columns.Add("Çözünmüş Oksijen (mg/L)"); // New
                    dt.Columns.Add("Sıcaklık (°C)"); // New
                    dt.Columns.Add("KOİ (mg/L)");
                    dt.Columns.Add("AKM (mg/L)");
                    dt.Columns.Add("SAIS Durumu");

                    var measureQuery = dbContext.SensorDatas
                        .Where(x => x.ReadTime >= startDate && x.ReadTime <= endDate);
                    
                    measureQuery = sortNewestFirst 
                        ? measureQuery.OrderByDescending(x => x.ReadTime) 
                        : measureQuery.OrderBy(x => x.ReadTime);

                    var measurements = await measureQuery.ToListAsync();

                    foreach (var m in measurements)
                    {
                        dt.Rows.Add(
                            m.ReadTime.ToString("dd.MM.yyyy HH:mm:ss"),
                            m.Ph?.ToString("F2") ?? "-",
                            m.Iletkenlik?.ToString("F0") ?? "-",
                            m.TesisDebi?.ToString("F1") ?? "-",
                            m.AkisHizi?.ToString("F2") ?? "-",
                            m.CozunmusOksijen?.ToString("F2") ?? "-",
                            m.Sicaklik?.ToString("F1") ?? "-",
                            m.Koi?.ToString("F1") ?? "-",
                            m.Akm?.ToString("F1") ?? "-",
                            m.IsSentToSais ? "Gönderildi" : "Gönderilmedi"
                        );
                    }
                    break;

                case "Kalibrasyon":
                    dt.Columns.Add("Tarih");
                    dt.Columns.Add("Sensör");
                    dt.Columns.Add("Tip");
                    dt.Columns.Add("Referans");
                    dt.Columns.Add("Ölçülen");
                    dt.Columns.Add("Fark");
                    dt.Columns.Add("Sonuç");

                    var calQuery = dbContext.Calibrations
                        .Where(x => x.CalibrationDate >= startDate && x.CalibrationDate <= endDate);

                    calQuery = sortNewestFirst
                        ? calQuery.OrderByDescending(x => x.CalibrationDate)
                        : calQuery.OrderBy(x => x.CalibrationDate);

                    var calibrations = await calQuery.ToListAsync();

                    foreach (var c in calibrations)
                    {
                        string sensorName = c.DbColumnName; 
                        var resultText = c.Result ? "Başarılı" : "Başarısız";
                        // Using ZeroRef for logic assuming if ZeroRef != 0 then likely Span (or check Duration? Entity doesn't store step type explicitly but we can infer)
                        // Actually better logic: If SpanDiff > 0 it's likely a Span or just show both if relevant.
                        // For now keeping simple: If ZeroRef > 0 implies Span? No, ZeroRef can be 7.0 for pH. 
                        // Let's use SpanMeas > 0 to imply Span step involved effectively? 
                        // Or just show "Zero" if SpanRef == 0? 
                        // pH Span is 4.0 or 10.0. pH Zero is 7.0.
                        // Iletkenlik Zero is 0. 
                        
                        string type = "Kalibrasyon";
                        if (c.ResultZero && !c.ResultSpan) type = "Zero";
                        else if (!c.ResultZero && c.ResultSpan) type = "Span";
                        else if (c.ResultZero && c.ResultSpan) type = "Full";
                        
                        dt.Rows.Add(
                            c.CalibrationDate.ToString("dd.MM.yyyy HH:mm"),
                            sensorName,
                            type,
                            type == "Zero" ? c.ZeroRef : c.SpanRef,
                            type == "Zero" ? c.ZeroMeas : c.SpanMeas,
                            type == "Zero" ? c.ZeroDiff : c.SpanDiff,
                            resultText
                        );
                    }
                    break;

                case "Numune": // Assuming we utilize SampleRequests or similar
                    dt.Columns.Add("Tarih");
                    dt.Columns.Add("Numune Kodu");
                    dt.Columns.Add("Durum");

                    // Fallback to LogEntry for now if SampleRequests not mapped
                     var sampleLogs = await dbContext.LogEntries
                        .Where(x => x.LogCreatedDate >= startDate && x.LogCreatedDate <= endDate && x.LogTitle.Contains("Numune"))
                        .OrderByDescending(x => x.LogCreatedDate)
                        .ToListAsync();
                    
                    foreach (var l in sampleLogs)
                    {
                        dt.Rows.Add(l.LogCreatedDate.ToString("dd.MM.yyyy HH:mm"), l.LogDescription, "Kaydedildi");
                    }
                    break;

                case "Kayıt":
                    dt.Columns.Add("Tarih");
                    dt.Columns.Add("Başlık");
                    dt.Columns.Add("Açıklama");
                    dt.Columns.Add("Seviye");

                    // Build list of selected log levels
                    var selectedLevels = new List<Domain.Entities.LogLevel>();
                    if (CheckBoxLogDebug?.Checked == true) selectedLevels.Add(Domain.Entities.LogLevel.Debug);
                    if (CheckBoxLogInfo?.Checked == true) selectedLevels.Add(Domain.Entities.LogLevel.Info);
                    if (CheckBoxLogWarning?.Checked == true) selectedLevels.Add(Domain.Entities.LogLevel.Warning);
                    if (CheckBoxLogError?.Checked == true) selectedLevels.Add(Domain.Entities.LogLevel.Error);
                    if (CheckBoxLogCritical?.Checked == true) selectedLevels.Add(Domain.Entities.LogLevel.Critical);

                    // Filter logs by selected levels
                    var logQuery = dbContext.LogEntries
                        .Where(x => x.LogCreatedDate >= startDate && x.LogCreatedDate <= endDate
                                 && selectedLevels.Contains(x.Level));

                    logQuery = sortNewestFirst
                        ? logQuery.OrderByDescending(x => x.LogCreatedDate)
                        : logQuery.OrderBy(x => x.LogCreatedDate);

                    var logs = await logQuery.ToListAsync();

                    foreach (var l in logs)
                    {
                        dt.Rows.Add(
                            l.LogCreatedDate.ToString("dd.MM.yyyy HH:mm:ss"),
                            l.LogTitle,
                            l.LogDescription,
                            l.Level
                        );
                    }
                    break;

                default: // Veri Geçerlilik / Eksik Veri
                    dt.Columns.Add("Tarih");
                    dt.Columns.Add("Beklenen");
                    dt.Columns.Add("Alınan");
                    dt.Columns.Add("Eksik");
                    dt.Columns.Add("Başarım (%)");

                    // Daily rollup
                    for (var d = startDate.Date; d <= endDate.Date; d = d.AddDays(1))
                    {
                        var dayStart = d;
                        var dayEnd = d.AddDays(1).AddTicks(-1);
                        
                        var count = await dbContext.SensorDatas.CountAsync(x => x.ReadTime >= dayStart && x.ReadTime <= dayEnd);
                        // Expected: 1 per minute = 1440
                        int expected = 1440;
                        int missing = expected - count;
                        if (missing < 0) missing = 0;
                        double success = (double)count / expected * 100.0;
                        if (success > 100) success = 100;

                        dt.Rows.Add(
                            d.ToString("dd.MM.yyyy"),
                            expected,
                            count,
                            missing,
                            success.ToString("F2")
                        );
                    }
                    break;
            }

            return dt;
        }

        private void ButtonSaveAsExcel_Click(object? sender, EventArgs e)
        {
            if (DataGridViewDatas == null || DataGridViewDatas.Rows.Count == 0)
            {
                MessageBox.Show("Önce rapor oluşturun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var saveDialog = new SaveFileDialog
            {
                Filter = "Excel Dosyası|*.csv",
                Title = "Excel olarak kaydet",
                FileName = $"Rapor_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportToCsv(saveDialog.FileName);
                    MessageBox.Show("Excel dosyası kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Kayıt hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportToCsv(string filePath)
        {
            var sb = new StringBuilder();

            // Headers
            var headers = new List<string>();
            foreach (DataGridViewColumn column in DataGridViewDatas!.Columns)
            {
                headers.Add(column.HeaderText);
            }
            sb.AppendLine(string.Join(";", headers));

            // Data
            foreach (DataGridViewRow row in DataGridViewDatas.Rows)
            {
                if (row.IsNewRow) continue;
                var cells = new List<string>();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cells.Add(cell.Value?.ToString() ?? "");
                }
                sb.AppendLine(string.Join(";", cells));
            }

            System.IO.File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private void ButtonSaveAsPdf_Click(object? sender, EventArgs e)
        {
            if (DataGridViewDatas == null || DataGridViewDatas.Rows.Count == 0)
            {
                MessageBox.Show("Önce rapor oluşturun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var saveDialog = new SaveFileDialog
            {
                Filter = "HTML Dosyası|*.html",
                Title = "Raporu Kaydet (Yazdır -> PDF)",
                FileName = $"Rapor_{DateTime.Now:yyyyMMdd_HHmmss}.html"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportToHtml(saveDialog.FileName);
                    
                    // Try to open the file
                    var result = MessageBox.Show(
                        "Rapor HTML olarak kaydedildi.\nPDF olarak kaydetmek için tarayıcıda açılan sayfayı yazdırabilirsiniz (Ctrl+P).\n\nŞimdi açmak ister misiniz?", 
                        "Başarılı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    
                    if (result == DialogResult.Yes)
                    {
                        var p = new System.Diagnostics.Process();
                        p.StartInfo = new System.Diagnostics.ProcessStartInfo(saveDialog.FileName) { UseShellExecute = true };
                        p.Start();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Kayıt hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportToHtml(string filePath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<html><head><meta charset='utf-8'>");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: Arial, sans-serif; margin: 20px; }");
            sb.AppendLine("table { border-collapse: collapse; width: 100%; margin-top: 20px; }");
            sb.AppendLine("th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }");
            sb.AppendLine("th { background-color: #f2f2f2; }");
            sb.AppendLine("h2 { color: #333; }");
            sb.AppendLine("</style></head><body>");
            
            sb.AppendLine($"<h2>ISKI İBKS Raporu - {DateTime.Now:dd.MM.yyyy HH:mm}</h2>");
            sb.AppendLine($"<p>Rapor Türü: {ComboBoxReportType?.Text ?? "Genel"}</p>");
            
            sb.AppendLine("<table>");
            
            // Headers
            sb.AppendLine("<thead><tr>");
            foreach (DataGridViewColumn column in DataGridViewDatas!.Columns)
            {
                 if (column.Visible)
                    sb.AppendLine($"<th>{column.HeaderText}</th>");
            }
            sb.AppendLine("</tr></thead>");

            // Data
            sb.AppendLine("<tbody>");
            foreach (DataGridViewRow row in DataGridViewDatas.Rows)
            {
                if (row.IsNewRow) continue;
                sb.AppendLine("<tr>");
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Visible) // Only export visible columns
                         sb.AppendLine($"<td>{cell.Value?.ToString() ?? "&nbsp;"}</td>");
                }
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</tbody></table>");
            
            sb.AppendLine("<br/><p><i>Bu rapor otomatik oluşturulmuştur.</i></p>");
            sb.AppendLine("</body></html>");

            System.IO.File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }
    }
}

