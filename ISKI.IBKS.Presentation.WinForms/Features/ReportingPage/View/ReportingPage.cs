using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using System.Security.AccessControl;
using Microsoft.Extensions.DependencyInjection;
using ISKI.IBKS.Shared.Localization;
using ISKI.IBKS.Presentation.WinForms.Features.ReportingPage.Presenter;

namespace ISKI.IBKS.Presentation.WinForms.Features.ReportingPage.View;

public partial class ReportingPage : UserControl, IReportingPageView
{
        public event EventHandler<ReportCriteriaEventArgs> GenerateReportRequested;
        public event EventHandler<ExportRequestedEventArgs> ExportRequested;

        public ReportingPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            InitializeLocalization();
            InitializeControls();
            AttachEvents();
            ActivatorUtilities.CreateInstance<ReportingPagePresenter>(serviceProvider, this);
        }

        private void InitializeControls()
        {
            this.DoubleBuffered = true;
            typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(DataGridViewDatas, true, null);

            ComboBoxReportType.SelectedIndex = 0;
            RadioButtonDaily.Checked = true;
            RadioButtonSortByLast.Checked = true;

            DateTimePickerFirstDate.Value = DateTime.Today;
            DateTimePickerFirstTime.Value = DateTime.Today;
            DateTimePickerLastDate.Value = DateTime.Today;
            DateTimePickerLastTime.Value = DateTime.Today.AddHours(23).AddMinutes(59);

            ButtonGenerate.BackColor = Color.FromArgb(0, 120, 215);

            UpdateDateRangeByPeriod();
        }

        private void InitializeLocalization()
        {
            titleBarControl1.TitleBarText = Strings.Report_Title;
            GroupBoxReportTypes.Text = Strings.Report_Type;
            groupBox3.Text = Strings.Report_Sort;
            RadioButtonSortByLast.Text = Strings.Report_SortLast;
            RadioButtonSortByFirst.Text = Strings.Report_SortFirst;
            
            GroupBoxDate.Text = Strings.Report_Date;
            LabelStartDate.Text = Strings.Report_StartDate;
            LabelEndDate.Text = Strings.Report_EndDate;
            
            groupBox1.Text = Strings.Report_Period;
            RadioButtonCustom.Text = Strings.Report_Custom;
            RadioButtonDaily.Text = Strings.Report_Daily;
            RadioButtonWeekly.Text = Strings.Report_Weekly;
            RadioButtonMonthly.Text = Strings.Report_Monthly;
            
            GroupBoxLogLevel.Text = Strings.Report_LogLevel;
            
            ButtonGenerate.Text = Strings.Report_Generate;

            ComboBoxReportType.Items.Clear();
            ComboBoxReportType.Items.Add(Strings.Report_Type_Measurement);
            ComboBoxReportType.Items.Add(Strings.Report_Type_Calibration);
            ComboBoxReportType.Items.Add(Strings.Report_Type_Sample);
            ComboBoxReportType.Items.Add(Strings.Report_Type_Record);
            ComboBoxReportType.Items.Add(Strings.Report_Type_Validity);
            ComboBoxReportType.SelectedIndex = 0;
        }

        private void AttachEvents()
        {
            ButtonGenerate.Click += (s, e) => OnGenerateReport();
            ButtonSaveAsExcel.Click += (s, e) => ExportRequested?.Invoke(this, new ExportRequestedEventArgs { Format = "CSV", Data = (DataTable)DataGridViewDatas.DataSource });
            ButtonSaveAsPdf.Click += (s, e) => ExportRequested?.Invoke(this, new ExportRequestedEventArgs { Format = "HTML", Data = (DataTable)DataGridViewDatas.DataSource });

            RadioButtonCustom.CheckedChanged += (s, e) => GroupBoxDate.Enabled = RadioButtonCustom.Checked;
            RadioButtonDaily.CheckedChanged += (s, e) => { if (RadioButtonDaily.Checked) UpdateDateRangeByPeriod(); };
            RadioButtonWeekly.CheckedChanged += (s, e) => { if (RadioButtonWeekly.Checked) UpdateDateRangeByPeriod(); };
            RadioButtonMonthly.CheckedChanged += (s, e) => { if (RadioButtonMonthly.Checked) UpdateDateRangeByPeriod(); };

            DataGridViewDatas.CellFormatting += DataGridViewDatas_CellFormatting;

            ComboBoxReportType.SelectedIndexChanged += (s, e) => GroupBoxLogLevel.Visible = ComboBoxReportType.SelectedItem?.ToString() == Strings.Report_Type_Record;
        }

        private void OnGenerateReport()
        {
            var args = new ReportCriteriaEventArgs
            {
                ReportType = ComboBoxReportType.SelectedItem?.ToString() ?? Strings.Report_Type_Measurement,
                StartDate = DateTimePickerFirstDate.Value.Date.Add(DateTimePickerFirstTime.Value.TimeOfDay),
                EndDate = DateTimePickerLastDate.Value.Date.Add(DateTimePickerLastTime.Value.TimeOfDay),
                SortNewestFirst = RadioButtonSortByLast.Checked,
                LogInfo = CheckBoxLogInfo.Checked,
                LogWarning = CheckBoxLogWarning.Checked,
                LogError = CheckBoxLogError.Checked
            };
            GenerateReportRequested?.Invoke(this, args);
        }

        public void DisplayReport(DataTable data)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => DisplayReport(data)));
                return;
            }
            DataGridViewDatas.DataSource = data;
            DataGridViewDatas.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, Strings.Common_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowInfo(string message)
        {
            MessageBox.Show(message, Strings.Common_Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void SetLoading(bool isLoading)
        {
            ButtonGenerate.Enabled = !isLoading;
            Cursor = isLoading ? Cursors.WaitCursor : Cursors.Default;
        }

        public string ShowSaveFileDialog(string filter, string defaultFileName)
        {
            using var saveDialog = new SaveFileDialog
            {
                Filter = filter,
                FileName = defaultFileName
            };
            return saveDialog.ShowDialog() == DialogResult.OK ? saveDialog.FileName : string.Empty;
        }

        private void DataGridViewDatas_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = DataGridViewDatas.Rows[e.RowIndex];
            
            // Cell formatting logic remains here as it's purely UI/Presentation layer
            // (Same as before but cleaned up)
            int levelColIndex = DataGridViewDatas.Columns.Cast<DataGridViewColumn>().FirstOrDefault(c => c.HeaderText == Strings.Common_Level)?.Index ?? -1;
            if (levelColIndex >= 0)
            {
                var levelStr = row.Cells[levelColIndex].Value?.ToString();
                if (levelStr == "Warning") { row.DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 205); row.DefaultCellStyle.ForeColor = Color.Black; }
                else if (levelStr == "Error") { row.DefaultCellStyle.BackColor = Color.FromArgb(255, 205, 210); row.DefaultCellStyle.ForeColor = Color.Black; }
            }

            int saisColIndex = DataGridViewDatas.Columns.Cast<DataGridViewColumn>().FirstOrDefault(c => c.HeaderText == Strings.Common_SaisStatus)?.Index ?? -1;
            if (saisColIndex >= 0)
            {
                var status = row.Cells[saisColIndex].Value?.ToString();
                if (status == Strings.Common_Sent) row.DefaultCellStyle.BackColor = Color.FromArgb(193, 225, 193);
                else if (status == Strings.Common_NotSent) row.DefaultCellStyle.BackColor = Color.FromArgb(255, 204, 153);
            }
        }

        private void UpdateDateRangeByPeriod()
        {
            if (RadioButtonCustom.Checked) return;
            DateTime now = DateTime.Now;
            DateTime start = DateTime.Today;
            DateTime end = DateTime.Today;

            if (RadioButtonDaily.Checked) { start = DateTime.Today; end = DateTime.Today.AddDays(1).AddTicks(-1); }
            else if (RadioButtonWeekly.Checked) { int diff = (7 + (now.DayOfWeek - DayOfWeek.Monday)) % 7; start = DateTime.Today.AddDays(-1 * diff); end = start.AddDays(7).AddTicks(-1); }
            else if (RadioButtonMonthly.Checked) { start = new DateTime(now.Year, now.Month, 1); end = start.AddMonths(1).AddTicks(-1); }

            DateTimePickerFirstDate.Value = start;
            DateTimePickerFirstTime.Value = start;
            DateTimePickerLastDate.Value = end;
            DateTimePickerLastTime.Value = end;
        }
    }

