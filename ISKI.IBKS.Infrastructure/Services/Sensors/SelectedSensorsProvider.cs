using ISKI.IBKS.Application.Services.Sensors;
using ISKI.IBKS.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Infrastructure.Services.Sensors;

/// <summary>
/// Seçili sensör listesini veritabanından sağlayan servis
/// </summary>
public sealed class SelectedSensorsProvider : ISelectedSensorsProvider
{
    private readonly IServiceScopeFactory _scopeFactory;

    public SelectedSensorsProvider(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }

    public async Task<IReadOnlyList<string>> GetSelectedSensorsAsync(CancellationToken cancellationToken = default)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
        var settings = await dbContext.StationSettings.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        return settings?.GetSelectedSensors() ?? new List<string>();
    }
}
