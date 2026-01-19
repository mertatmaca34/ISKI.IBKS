using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Persistence.Contexts;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.ChildPages
{
    public partial class AlarmEditDialog : Form
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Guid? _alarmId;

        public AlarmEditDialog(IServiceScopeFactory scopeFactory, Guid? alarmId)
        {
            _scopeFactory = scopeFactory;
            _alarmId = alarmId;
            InitializeComponent();
            Load += AlarmEditDialog_Load;
            ComboBoxType.SelectedIndexChanged += ComboBoxType_SelectedIndexChanged;
            ButtonSave.Click += ButtonSave_Click;
        }

        private async void AlarmEditDialog_Load(object? sender, EventArgs e)
        {
            ComboBoxType.SelectedIndex = 0;
            ComboBoxPriority.SelectedIndex = 1;
            UpdateFieldVisibility();

            if (_alarmId.HasValue)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                    var alarm = await dbContext.AlarmDefinitions.FindAsync(_alarmId.Value);
                    if (alarm != null)
                    {
                        TextBoxName.Text = alarm.Name;
                        TextBoxDescription.Text = alarm.Description;
                        TextBoxSensorName.Text = alarm.SensorName;
                        ComboBoxType.SelectedIndex = (int)alarm.Type;
                        NumericMinThreshold.Value = (decimal)(alarm.MinThreshold ?? 0);
                        NumericMaxThreshold.Value = (decimal)(alarm.MaxThreshold ?? 0);
                        CheckBoxExpectedDigitalValue.Checked = alarm.ExpectedDigitalValue ?? false;
                        CheckBoxIsActive.Checked = alarm.IsActive;
                        ComboBoxPriority.SelectedIndex = (int)alarm.Priority;
                        UpdateFieldVisibility();
                    }
                }
                catch { }
            }
        }

        private void ComboBoxType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateFieldVisibility();
        }

        private void UpdateFieldVisibility()
        {
            bool isThreshold = ComboBoxType.SelectedIndex == 0;
            bool isDigital = ComboBoxType.SelectedIndex == 1;

            LabelMinThreshold.Visible = NumericMinThreshold.Visible = isThreshold;
            LabelMaxThreshold.Visible = NumericMaxThreshold.Visible = isThreshold;
            LabelExpectedDigitalValue.Visible = CheckBoxExpectedDigitalValue.Visible = isDigital;
        }

        private async void ButtonSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxName.Text) || string.IsNullOrWhiteSpace(TextBoxSensorName.Text))
            {
                MessageBox.Show("İsim ve Sensör Adı zorunludur.", "Doğrulama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();

                var type = (AlarmType)ComboBoxType.SelectedIndex;
                var priority = (AlarmPriority)ComboBoxPriority.SelectedIndex;
                double? minThreshold = type == AlarmType.Threshold ? (double?)NumericMinThreshold.Value : null;
                double? maxThreshold = type == AlarmType.Threshold ? (double?)NumericMaxThreshold.Value : null;
                bool? expectedDigital = type == AlarmType.Digital ? CheckBoxExpectedDigitalValue.Checked : null;

                if (_alarmId.HasValue)
                {
                    var alarm = await dbContext.AlarmDefinitions.FindAsync(_alarmId.Value);
                    if (alarm != null)
                    {
                        alarm.Update(
                            TextBoxName.Text.Trim(),
                            TextBoxDescription.Text.Trim(),
                            TextBoxSensorName.Text.Trim(),
                            type,
                            minThreshold,
                            maxThreshold,
                            expectedDigital,
                            priority,
                            CheckBoxIsActive.Checked
                        );
                    }
                }
                else
                {
                    AlarmDefinition newAlarm;
                    if (type == AlarmType.Threshold)
                    {
                        newAlarm = AlarmDefinition.CreateThresholdAlarm(
                            TextBoxName.Text.Trim(),
                            TextBoxSensorName.Text.Trim(),
                            minThreshold,
                            maxThreshold,
                            TextBoxDescription.Text.Trim(),
                            priority
                        );
                    }
                    else
                    {
                        newAlarm = AlarmDefinition.CreateDigitalAlarm(
                            TextBoxName.Text.Trim(),
                            TextBoxSensorName.Text.Trim(),
                            expectedDigital ?? false,
                            TextBoxDescription.Text.Trim(),
                            priority
                        );
                    }

                    if (!CheckBoxIsActive.Checked) newAlarm.Deactivate();
                    dbContext.AlarmDefinitions.Add(newAlarm);
                }

                await dbContext.SaveChangesAsync();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
