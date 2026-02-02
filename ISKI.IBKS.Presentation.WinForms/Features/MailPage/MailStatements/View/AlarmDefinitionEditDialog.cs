using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Domain.Enums;
using ISKI.IBKS.Infrastructure.Persistence.Contexts;
using ISKI.IBKS.Shared.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailStatements.View;

public class AlarmDefinitionEditDialog : Form
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly Guid? _alarmId;

    private TextBox _txtName;
    private TextBox _txtDescription;
    private TextBox _txtSensorName;
    private TextBox _txtDbColumnName;
    private ComboBox _cmbType;
    private NumericUpDown _numMin;
    private NumericUpDown _numMax;
    private CheckBox _chkExpectedValue;
    private ComboBox _cmbPriority;
    private CheckBox _chkIsActive;
    private Button _btnSave;
    private Button _btnCancel;

    public AlarmDefinitionEditDialog(IServiceScopeFactory scopeFactory, Guid? alarmId)
    {
        _scopeFactory = scopeFactory;
        _alarmId = alarmId;
        InitializeComponents();
        Load += AlarmDefinitionEditDialog_Load;
    }

    private void InitializeComponents()
    {
        Text = _alarmId.HasValue ? "Alarm Düzenle" : "Yeni Alarm Ekle";
        Size = new Size(450, 500);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 11,
            Padding = new Padding(15)
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        int row = 0;
        layout.Controls.Add(new Label { Text = "Ad:", Anchor = AnchorStyles.Left }, 0, row);
        _txtName = new TextBox { Dock = DockStyle.Fill };
        layout.Controls.Add(_txtName, 1, row++);

        layout.Controls.Add(new Label { Text = "Açıklama:", Anchor = AnchorStyles.Left }, 0, row);
        _txtDescription = new TextBox { Dock = DockStyle.Fill };
        layout.Controls.Add(_txtDescription, 1, row++);

        layout.Controls.Add(new Label { Text = "Sensör Adı:", Anchor = AnchorStyles.Left }, 0, row);
        _txtSensorName = new TextBox { Dock = DockStyle.Fill };
        layout.Controls.Add(_txtSensorName, 1, row++);

        layout.Controls.Add(new Label { Text = "Kolon Adı:", Anchor = AnchorStyles.Left }, 0, row);
        _txtDbColumnName = new TextBox { Dock = DockStyle.Fill };
        layout.Controls.Add(_txtDbColumnName, 1, row++);

        layout.Controls.Add(new Label { Text = "Tip:", Anchor = AnchorStyles.Left }, 0, row);
        _cmbType = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
        _cmbType.Items.AddRange(new object[] { "Eşik Değer", "Dijital" });
        _cmbType.SelectedIndex = 0;
        _cmbType.SelectedIndexChanged += CmbType_SelectedIndexChanged;
        layout.Controls.Add(_cmbType, 1, row++);

        layout.Controls.Add(new Label { Text = "Min Değer:", Anchor = AnchorStyles.Left }, 0, row);
        _numMin = new NumericUpDown { Dock = DockStyle.Fill, Minimum = -10000, Maximum = 10000, DecimalPlaces = 2 };
        layout.Controls.Add(_numMin, 1, row++);

        layout.Controls.Add(new Label { Text = "Max Değer:", Anchor = AnchorStyles.Left }, 0, row);
        _numMax = new NumericUpDown { Dock = DockStyle.Fill, Minimum = -10000, Maximum = 10000, DecimalPlaces = 2, Value = 100 };
        layout.Controls.Add(_numMax, 1, row++);

        layout.Controls.Add(new Label { Text = "Beklenen Değer:", Anchor = AnchorStyles.Left }, 0, row);
        _chkExpectedValue = new CheckBox { Text = "True", Anchor = AnchorStyles.Left };
        layout.Controls.Add(_chkExpectedValue, 1, row++);

        layout.Controls.Add(new Label { Text = "Öncelik:", Anchor = AnchorStyles.Left }, 0, row);
        _cmbPriority = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
        _cmbPriority.Items.AddRange(new object[] { "Düşük", "Orta", "Yüksek", "Kritik" });
        _cmbPriority.SelectedIndex = 1;
        layout.Controls.Add(_cmbPriority, 1, row++);

        layout.Controls.Add(new Label { Text = "", Anchor = AnchorStyles.Left }, 0, row);
        _chkIsActive = new CheckBox { Text = "Aktif", Checked = true, Anchor = AnchorStyles.Left };
        layout.Controls.Add(_chkIsActive, 1, row++);

        var buttonPanel = new FlowLayoutPanel 
        { 
            Dock = DockStyle.Fill, 
            FlowDirection = FlowDirection.RightToLeft,
            Padding = new Padding(0, 10, 0, 0)
        };
        _btnCancel = new Button { Text = "İptal", Width = 80 };
        _btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
        _btnSave = new Button { Text = "Kaydet", Width = 80, BackColor = Color.FromArgb(0, 131, 200), ForeColor = Color.White };
        _btnSave.Click += BtnSave_Click;
        buttonPanel.Controls.Add(_btnCancel);
        buttonPanel.Controls.Add(_btnSave);
        layout.Controls.Add(buttonPanel, 0, row);
        layout.SetColumnSpan(buttonPanel, 2);

        Controls.Add(layout);
    }

    private void CmbType_SelectedIndexChanged(object? sender, EventArgs e)
    {
        bool isThreshold = _cmbType.SelectedIndex == 0;
        _numMin.Enabled = isThreshold;
        _numMax.Enabled = isThreshold;
        _chkExpectedValue.Enabled = !isThreshold;
    }

    private async void AlarmDefinitionEditDialog_Load(object? sender, EventArgs e)
    {
        CmbType_SelectedIndexChanged(null, EventArgs.Empty);
        
        if (_alarmId.HasValue)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                var alarm = await dbContext.AlarmDefinitions.FindAsync(_alarmId.Value);
                if (alarm != null)
                {
                    _txtName.Text = alarm.Name;
                    _txtDescription.Text = alarm.Description;
                    _txtSensorName.Text = alarm.SensorName;
                    _txtDbColumnName.Text = alarm.DbColumnName;
                    _cmbType.SelectedIndex = alarm.Type == AlarmType.Threshold ? 0 : 1;
                    _numMin.Value = (decimal)(alarm.MinThreshold ?? 0);
                    _numMax.Value = (decimal)(alarm.MaxThreshold ?? 100);
                    _chkExpectedValue.Checked = alarm.ExpectedDigitalValue ?? true;
                    _cmbPriority.SelectedIndex = (int)alarm.Priority;
                    _chkIsActive.Checked = alarm.IsActive;
                }
            }
            catch { }
        }
    }

    private async void BtnSave_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_txtName.Text))
        {
            MessageBox.Show("Alarm adı zorunludur.", Strings.Common_Validation, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();

            var type = _cmbType.SelectedIndex == 0 ? AlarmType.Threshold : AlarmType.Digital;
            var priority = (AlarmPriority)_cmbPriority.SelectedIndex;

            if (_alarmId.HasValue)
            {
                var alarm = await dbContext.AlarmDefinitions.FindAsync(_alarmId.Value);
                if (alarm != null)
                {
                    alarm.Update(
                        _txtName.Text.Trim(),
                        _txtDescription.Text.Trim(),
                        _txtSensorName.Text.Trim(),
                        _txtDbColumnName.Text.Trim(),
                        type,
                        type == AlarmType.Threshold ? (double?)_numMin.Value : null,
                        type == AlarmType.Threshold ? (double?)_numMax.Value : null,
                        type == AlarmType.Digital ? _chkExpectedValue.Checked : null,
                        priority,
                        _chkIsActive.Checked
                    );
                }
            }
            else
            {
                var newAlarm = new AlarmDefinition(
                    _txtName.Text.Trim(),
                    _txtDescription.Text.Trim(),
                    _txtSensorName.Text.Trim(),
                    _txtDbColumnName.Text.Trim(),
                    type,
                    type == AlarmType.Threshold ? (double?)_numMin.Value : null,
                    type == AlarmType.Threshold ? (double?)_numMax.Value : null,
                    type == AlarmType.Digital ? _chkExpectedValue.Checked : null,
                    priority,
                    _chkIsActive.Checked
                );
                dbContext.AlarmDefinitions.Add(newAlarm);
            }

            await dbContext.SaveChangesAsync();
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(string.Format(Strings.Common_SaveError, ex.Message), Strings.Common_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
