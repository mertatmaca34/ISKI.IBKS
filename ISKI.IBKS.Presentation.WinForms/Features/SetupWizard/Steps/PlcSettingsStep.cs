using System.Net.NetworkInformation;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard;

/// <summary>
/// PLC Ayarları adımı
/// </summary>
public partial class PlcSettingsStep : UserControl, ISetupWizardStep
{
    private readonly SetupState _state;

    private static readonly string[] AvailableSensors = new[]
    {
        // Analog Sensörler (Zorunlu)
        "TesisDebi", "AkisHizi", "Ph", "Iletkenlik", "CozunmusOksijen",
        "Koi", "Akm", "KabinSicakligi", "Pompa1Hz", "Pompa2Hz",
        // Opsiyonel
        "DesarjDebi", "HariciDebi", "HariciDebi2"
    };

    public string Title => "PLC Ayarları";
    public string Description => "PLC bağlantı bilgilerini ve kullanılacak sensörleri yapılandırın.";
    public int StepNumber => 1;

    public PlcSettingsStep(SetupState state)
    {
        _state = state;
        InitializeComponent();
        SetupEvents();
        PopulateSensors();
    }

    private void SetupEvents()
    {
        _btnTest.Click += async (s, e) => await TestConnectionAsync();
    }

    private void PopulateSensors()
    {
        _sensorList.Items.Clear();
        foreach (var sensor in AvailableSensors)
        {
            _sensorList.Items.Add(sensor, _state.SelectedSensors.Contains(sensor));
        }
    }

    public Task LoadAsync(CancellationToken ct = default)
    {
        _txtIpAddress.Text = _state.PlcIpAddress;
        _numRack.Value = _state.PlcRack;
        _numSlot.Value = _state.PlcSlot;

        for (int i = 0; i < _sensorList.Items.Count; i++)
        {
            var sensor = _sensorList.Items[i].ToString();
            _sensorList.SetItemChecked(i, _state.SelectedSensors.Contains(sensor));
        }

        return Task.CompletedTask;
    }

    public (bool IsValid, string? ErrorMessage) Validate()
    {
        if (string.IsNullOrWhiteSpace(_txtIpAddress.Text))
            return (false, "PLC IP adresi boş olamaz.");

        if (!System.Net.IPAddress.TryParse(_txtIpAddress.Text, out _))
            return (false, "Geçerli bir IP adresi giriniz.");

        if (_sensorList.CheckedItems.Count == 0)
            return (false, "En az bir sensör seçmelisiniz.");

        return (true, null);
    }

    public Task<bool> SaveAsync(CancellationToken ct = default)
    {
        _state.PlcIpAddress = _txtIpAddress.Text;
        _state.PlcRack = (int)_numRack.Value;
        _state.PlcSlot = (int)_numSlot.Value;

        _state.SelectedSensors.Clear();
        foreach (var item in _sensorList.CheckedItems)
        {
            _state.SelectedSensors.Add(item.ToString()!);
        }

        return Task.FromResult(true);
    }

    public Control GetControl() => this;

    private async Task TestConnectionAsync()
    {
        _btnTest.Enabled = false;
        _btnTest.Text = "Test Ediliyor...";

        try
        {
            using var ping = new Ping();
            var reply = await ping.SendPingAsync(_txtIpAddress.Text, 3000);

            if (reply.Status == IPStatus.Success)
            {
                MessageBox.Show($"PLC bağlantısı başarılı!\nSüre: {reply.RoundtripTime}ms",
                    "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"PLC'ye erişilemiyor.\nDurum: {reply.Status}",
                    "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Bağlantı testi başarısız.\n{ex.Message}",
                "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _btnTest.Enabled = true;
            _btnTest.Text = "PLC Bağlantısını Test Et";
        }
    }
}
