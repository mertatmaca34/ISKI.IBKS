using ISKI.IBKS.Application.Common.Configuration;
using ISKI.IBKS.Domain.Enums;
using ISKI.IBKS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISKI.IBKS.Presentation.WinForms.Features.ReportingPage.View;
using ISKI.IBKS.Infrastructure.Persistence.Contexts;

namespace ISKI.IBKS.Presentation.WinForms.Features.ReportingPage.Presenter;

public sealed class ReportingPagePresenter
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ReportingPagePresenter> _logger;
    private readonly IStationConfiguration _stationConfig;
    private readonly IReportingPageView _view;

    public ReportingPagePresenter(
        IServiceScopeFactory scopeFactory,
        ILogger<ReportingPagePresenter> logger,
        IStationConfiguration stationConfig,
        IReportingPageView view)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _stationConfig = stationConfig;
        _view = view;

        _view.GenerateReportRequested += async (s, e) => await HandleGenerateReportAsync(e);
        _view.ExportRequested += (s, e) => HandleExport(e);
    }

    private async Task HandleGenerateReportAsync(ReportCriteriaEventArgs e)
    {
        _view.SetLoading(true);
        try
        {
            var data = await GenerateReportDataAsync(e);
            _view.DisplayReport(data);
            _view.ShowInfo($"{data.Rows.Count} kayıt bulundu.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating report");
            _view.ShowError($"Rapor oluşturulurken hata: {ex.Message}");
        }
        finally
        {
            _view.SetLoading(false);
        }
    }

    private async Task<DataTable> GenerateReportDataAsync(ReportCriteriaEventArgs e)
    {
        var dt = new DataTable();
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();

        switch (e.ReportType)
        {
            case "Ölçüm":
                var selectedSensors = _stationConfig.SelectedSensors;
                dt.Columns.Add("Veri Tarihi");
                dt.Columns.Add("Saise Gönderim Tarihi");
                foreach (var sensorKey in selectedSensors) dt.Columns.Add(GetFriendlyName(sensorKey));
                dt.Columns.Add("SAIS Durumu");

                var measureQuery = dbContext.SensorDatas.Where(x => x.ReadTime >= e.StartDate && x.ReadTime <= e.EndDate);
                measureQuery = e.SortNewestFirst ? measureQuery.OrderByDescending(x => x.ReadTime) : measureQuery.OrderBy(x => x.ReadTime);
                var measurements = await measureQuery.ToListAsync();

                foreach (var m in measurements)
                {
                    var row = dt.NewRow();
                    row["Veri Tarihi"] = m.ReadTime.ToString("dd.MM.yyyy HH:mm:ss");
                    row["Saise Gönderim Tarihi"] = m.SentToSaisAt?.ToLocalTime().ToString("dd.MM.yyyy HH:mm:ss") ?? "-";
                    foreach (var sensorKey in selectedSensors) row[GetFriendlyName(sensorKey)] = GetSensorValue(m, sensorKey);
                    row["SAIS Durumu"] = m.IsSentToSais ? "Gönderildi" : "Gönderilmedi";
                    dt.Rows.Add(row);
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

                var calQuery = dbContext.Calibrations.Where(x => x.CalibrationDate >= e.StartDate && x.CalibrationDate <= e.EndDate);
                calQuery = e.SortNewestFirst ? calQuery.OrderByDescending(x => x.CalibrationDate) : calQuery.OrderBy(x => x.CalibrationDate);
                var calibrations = await calQuery.ToListAsync();

                foreach (var c in calibrations)
                {
                    var type = (c.ResultZero != 0 && c.ResultSpan == 0) ? "Zero" : (c.ResultZero == 0 && c.ResultSpan != 0) ? "Span" : "Full";
                    dt.Rows.Add(c.CalibrationDate.ToString("dd.MM.yyyy HH:mm"), c.DbColumnName, type, type == "Zero" ? c.ZeroRef : c.SpanRef, type == "Zero" ? c.ZeroMeas : c.SpanMeas, type == "Zero" ? c.ZeroDiff : c.SpanDiff, (c.Result != 0) ? "Başarılı" : "Başarısız");
                }
                break;

            case "Numune":
                dt.Columns.Add("Tarih");
                dt.Columns.Add("Numune Kodu");
                dt.Columns.Add("Durum");
                var sampleLogs = await dbContext.LogEntries.Where(x => x.CreatedAt >= e.StartDate && x.CreatedAt <= e.EndDate && x.Source.Contains("Numune")).OrderByDescending(x => x.CreatedAt).ToListAsync();
                foreach (var l in sampleLogs) dt.Rows.Add(l.CreatedAt.ToString("dd.MM.yyyy HH:mm"), l.Message, "Kaydedildi");
                break;

            case "Kayıt":
                dt.Columns.Add("Tarih");
                dt.Columns.Add("Başlık");
                dt.Columns.Add("Açıklama");
                dt.Columns.Add("Seviye");
                var selectedLevels = new List<ISKI.IBKS.Domain.Enums.LogLevel>();
                if (e.LogInfo) selectedLevels.Add(ISKI.IBKS.Domain.Enums.LogLevel.Info);
                if (e.LogWarning) selectedLevels.Add(ISKI.IBKS.Domain.Enums.LogLevel.Warning);
                if (e.LogError) selectedLevels.Add(ISKI.IBKS.Domain.Enums.LogLevel.Error);
                var logQuery = dbContext.LogEntries.Where(x => x.CreatedAt >= e.StartDate && x.CreatedAt <= e.EndDate && selectedLevels.Contains(x.Level));
                logQuery = e.SortNewestFirst ? logQuery.OrderByDescending(x => x.CreatedAt) : logQuery.OrderBy(x => x.CreatedAt);
                var logs = await logQuery.ToListAsync();
                foreach (var l in logs) dt.Rows.Add(l.CreatedAt.ToString("dd.MM.yyyy HH:mm:ss"), l.Source, l.Message, l.Level);
                break;

            default:
                dt.Columns.Add("Tarih");
                dt.Columns.Add("Beklenen");
                dt.Columns.Add("Alınan");
                dt.Columns.Add("Eksik");
                dt.Columns.Add("Başarım (%)");
                for (var d = e.StartDate.Date; d <= e.EndDate.Date; d = d.AddDays(1))
                {
                    var count = await dbContext.SensorDatas.CountAsync(x => x.ReadTime >= d && x.ReadTime <= d.AddDays(1).AddTicks(-1));
                    int expected = 1440;
                    dt.Rows.Add(d.ToString("dd.MM.yyyy"), expected, count, Math.Max(0, expected - count), Math.Min(100.0, (double)count / expected * 100.0).ToString("F2"));
                }
                break;
        }
        return dt;
    }

    private void HandleExport(ExportRequestedEventArgs e)
    {
        var filter = e.Format == "CSV" ? "Excel Dosyası|*.csv" : "HTML Dosyası|*.html";
        var fileName = $"Rapor_{DateTime.Now:yyyyMMdd_HHmmss}.{(e.Format == "CSV" ? "csv" : "html")}";
        var path = _view.ShowSaveFileDialog(filter, fileName);
        if (string.IsNullOrEmpty(path)) return;

        try
        {
            if (e.Format == "CSV") ExportToCsv(e.Data, path);
            else ExportToHtml(e.Data, path);
            _view.ShowInfo("Rapor kaydedildi.");
        }
        catch (Exception ex)
        {
            _view.ShowError($"Kayıt hatası: {ex.Message}");
        }
    }

    private void ExportToCsv(DataTable dt, string path)
    {
        var sb = new StringBuilder();
        var columns = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
        sb.AppendLine(string.Join(";", columns));
        foreach (DataRow row in dt.Rows) sb.AppendLine(string.Join(";", row.ItemArray.Select(i => i?.ToString() ?? "")));
        System.IO.File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
    }

    private void ExportToHtml(DataTable dt, string path)
    {
        var sb = new StringBuilder();
        sb.AppendLine("<html><head><meta charset='utf-8'><style>body { font-family: Arial; } table { border-collapse: collapse; width: 100%; } th, td { border: 1px solid #ddd; padding: 8px; }</style></head><body>");
        sb.AppendLine($"<h2>ISKI İBKS Raporu - {DateTime.Now:dd.MM.yyyy HH:mm}</h2><table><thead><tr>");
        foreach (DataColumn col in dt.Columns) sb.AppendLine($"<th>{col.ColumnName}</th>");
        sb.AppendLine("</tr></thead><tbody>");
        foreach (DataRow row in dt.Rows)
        {
            sb.AppendLine("<tr>");
            foreach (var item in row.ItemArray) sb.AppendLine($"<td>{item ?? "&nbsp;"}</td>");
            sb.AppendLine("</tr>");
        }
        sb.AppendLine("</tbody></table></body></html>");
        System.IO.File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
    }

    private string GetFriendlyName(string sensorKey) => sensorKey switch
    {
        "TesisDebi" => "Debi (m³/h)",
        "OlcumCihaziAkisHizi" => "Akış Hızı (m/s)",
        "Ph" => "pH",
        "Iletkenlik" => "İletkenlik (µS/cm)",
        "CozunmusOksijen" => "Çözünmüş Oksijen (mg/L)",
        "Koi" => "KOİ (mg/L)",
        "Akm" => "AKM (mg/L)",
        "KabinSicakligi" => "Kabin Sıcaklığı (°C)",
        _ => sensorKey
    };

    private string GetSensorValue(SensorData m, string sensorKey) => sensorKey switch
    {
        "TesisDebi" => m.TesisDebi?.ToString("F1") ?? "-",
        "OlcumCihaziAkisHizi" => m.AkisHizi?.ToString("F2") ?? "-",
        "Ph" => m.Ph?.ToString("F2") ?? "-",
        "Iletkenlik" => m.Iletkenlik?.ToString("F0") ?? "-",
        "CozunmusOksijen" => m.CozunmusOksijen?.ToString("F2") ?? "-",
        "Koi" => m.Koi?.ToString("F1") ?? "-",
        "Akm" => m.Akm?.ToString("F1") ?? "-",
        "KabinSicakligi" => m.Sicaklik?.ToString("F1") ?? "-",
        _ => "-"
    };
}
