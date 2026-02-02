using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.Model;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.PlcSettings;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.WizardMain;
using ISKI.IBKS.Shared.Localization;
using System.Linq;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.PlcSettings;

public partial class PlcSettingsStep : UserControl, IPlcSettingsStepView, ISetupWizardStep
{
    private readonly PlcSettingsStepPresenter _presenter;

    public event EventHandler TestConnectionRequested;

    public string Title => Strings.Wizard_PlcTitle;
    public string Description => Strings.Wizard_PlcDesc;
    public int StepNumber => 1;

    public string IpAddress { get => _txtIpAddress.Text; set => _txtIpAddress.Text = value; }
    public decimal Rack { get => _numRack.Value; set => _numRack.Value = value; }
    public decimal Slot { get => _numSlot.Value; set => _numSlot.Value = value; }
    
    public List<string> AvailableSensors
    {
        set
        {
            _sensorList.Items.Clear();
            foreach (var sensor in value) _sensorList.Items.Add(sensor);
        }
    }

    public List<string> SelectedSensors
    {
        get
        {
            var selected = new List<string>();
            foreach (var item in _sensorList.CheckedItems)
                selected.Add(item.ToString()!);
            return selected;
        }
        set
        {
            for (int i = 0; i < _sensorList.Items.Count; i++)
                _sensorList.SetItemChecked(i, value.Contains(_sensorList.Items[i].ToString()!));
        }
    }

    public PlcSettingsStep(SetupState state)
    {
        InitializeComponent();
        InitializeLocalization();
        _presenter = new PlcSettingsStepPresenter(this, state);
        _btnTest.Click += (s, e) => TestConnectionRequested?.Invoke(this, EventArgs.Empty);
    }

    public Task LoadAsync(CancellationToken ct = default) => Task.CompletedTask;
    public (bool IsValid, string? ErrorMessage) Validate() => _presenter.Validate();
    public Task<bool> SaveAsync(CancellationToken ct = default)
    {
        _presenter.SaveData();
        return Task.FromResult(true);
    }

    public Control GetControl() => this;

    public void SetTestButtonState(bool enabled, string text)
    {
        _btnTest.Enabled = enabled;
        _btnTest.Text = text;
    }

    public void ShowMessage(string message, string title, bool isError)
    {
        MessageBox.Show(message, title, MessageBoxButtons.OK, isError ? MessageBoxIcon.Error : MessageBoxIcon.Information);
    }

    private void InitializeLocalization()
    {
        lblIp.Text = Strings.Plc_Ip;
        lblRack.Text = Strings.Plc_Rack;
        lblSlot.Text = Strings.Plc_Slot;
        lblSensors.Text = Strings.Plc_Sensors;
        _btnTest.Text = Strings.Plc_TestButton;
    }
}
