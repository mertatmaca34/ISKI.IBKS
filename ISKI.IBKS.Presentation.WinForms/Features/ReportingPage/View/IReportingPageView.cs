using System;
using System.Data;

namespace ISKI.IBKS.Presentation.WinForms.Features.ReportingPage.View;

public interface IReportingPageView
{
    event EventHandler Load;
    event EventHandler<ReportCriteriaEventArgs> GenerateReportRequested;
    event EventHandler<ExportRequestedEventArgs> ExportRequested;

    void DisplayReport(DataTable data);
    void ShowError(string message);
    void ShowInfo(string message);
    void SetLoading(bool isLoading);
    string ShowSaveFileDialog(string filter, string defaultFileName);
}

public class ReportCriteriaEventArgs : EventArgs
{
    public string ReportType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool SortNewestFirst { get; set; }
    public bool LogInfo { get; set; }
    public bool LogWarning { get; set; }
    public bool LogError { get; set; }
}

public class ExportRequestedEventArgs : EventArgs
{
    public string Format { get; set; }
    public DataTable Data { get; set; }
}
