using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.Model;
using ISKI.IBKS.Application.Common.IoT.Plc;
using ISKI.IBKS.Shared.Localization;
using ISKI.IBKS.Infrastructure.Persistence.Contexts;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.Steps;

public sealed class FinalSummaryStepPresenter
{
    private readonly IFinalSummaryStepView _view;
    private readonly SetupState _state;
    private readonly IbksDbContext _dbContext;
    private readonly IPlcClient _plcClient;

    public FinalSummaryStepPresenter(
        IFinalSummaryStepView view, 
        SetupState state, 
        IbksDbContext dbContext,
        IPlcClient plcClient)
    {
        _view = view;
        _state = state;
        _dbContext = dbContext;
        _plcClient = plcClient;
    }

    public async Task RunTestsAsync()
    {
        _view.SetTestingInProgress(true);
        int errorCount = 0;

        // 1. DB Test
        try
        {
            var canConnect = await _dbContext.Database.CanConnectAsync();
            if (canConnect)
            {
                _view.SetDbStatus("OK", true);
            }
            else
            {
                // If CanConnect is false, it might be because the DB doesn't exist yet.
                // We try to check if we can at least reach the server.
                using var connection = new Microsoft.Data.SqlClient.SqlConnection(_dbContext.Database.GetConnectionString());
                var builder = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder(connection.ConnectionString);
                string dbName = builder.InitialCatalog;
                builder.InitialCatalog = "master"; // Try connecting to master instead
                
                using var masterConn = new Microsoft.Data.SqlClient.SqlConnection(builder.ConnectionString);
                await masterConn.OpenAsync();
                _view.SetDbStatus($"OK ({dbName} will be created)", true);
            }
        }
        catch (Exception ex)
        {
            _view.SetDbStatus($"Error: {ex.Message}", false);
            errorCount++;
        }

        await Task.Delay(500); // Visual pause

        // 2. PLC Test (Real Connection)
        try
        {
            var connected = await _plcClient.ConnectAsync(_state.PlcIpAddress.Trim(), _state.PlcRack, _state.PlcSlot);
            _view.SetPlcStatus(connected ? "OK" : "Fail", connected);
            if (connected) await _plcClient.DisconnectAsync();
        }
        catch (Exception ex)
        {
            _view.SetPlcStatus($"Error: {ex.Message}", false);
        }

        await Task.Delay(500);

        // 3. SAIS Test
        try
        {
            using var client = new System.Net.Http.HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10);
            var response = await client.GetAsync(_state.SaisApiUrl);
            _view.SetSaisStatus(response.IsSuccessStatusCode ? "OK" : $"Fail ({response.StatusCode})", response.IsSuccessStatusCode);
        }
        catch (Exception ex)
        {
            _view.SetSaisStatus($"Error: {ex.Message}", false);
        }

        await Task.Delay(500);

        // 4. Mail Test (Quick timeout like PLC)
        try
        {
            using var cts = new System.Threading.CancellationTokenSource(TimeSpan.FromSeconds(3));
            using var tcpClient = new System.Net.Sockets.TcpClient();
            await tcpClient.ConnectAsync(_state.SmtpHost.Trim(), _state.SmtpPort, cts.Token);
            _view.SetMailStatus("OK", true);
        }
        catch (OperationCanceledException)
        {
            _view.SetMailStatus("Timeout", false);
            errorCount++;
        }
        catch (Exception ex)
        {
            _view.SetMailStatus($"Error: {ex.Message}", false);
            errorCount++;
        }

        _view.SetTestingInProgress(false);
        _view.OnTestsCompleted(errorCount);
    }
}
