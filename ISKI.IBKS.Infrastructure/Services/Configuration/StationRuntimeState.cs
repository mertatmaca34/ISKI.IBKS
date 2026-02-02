using ISKI.IBKS.Application.Common.Configuration;
using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.Services.Configuration;

public class StationRuntimeState : IStationRuntimeState
{
    public DateTime? LastDataDate { get; set; }
    public DateTime? LastSaisSync { get; set; }

    public async Task SaveAsync(CancellationToken ct = default)
    {
        try
        {
            var filePath = GetStateFilePath();
            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, json, ct);
        }
        catch { }
    }

    public async Task LoadAsync(CancellationToken ct = default)
    {
        try
        {
            var filePath = GetStateFilePath();
            if (File.Exists(filePath))
            {
                var json = await File.ReadAllTextAsync(filePath, ct);
                var state = JsonSerializer.Deserialize<StationRuntimeState>(json);
                if (state != null)
                {
                    this.LastDataDate = state.LastDataDate;
                    this.LastSaisSync = state.LastSaisSync;
                }
            }
        }
        catch { }
    }

    private string GetStateFilePath()
    {
        return Path.Combine(AppContext.BaseDirectory, "Configuration", "state.json");
    }
}
