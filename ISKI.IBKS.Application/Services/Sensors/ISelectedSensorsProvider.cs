namespace ISKI.IBKS.Application.Services.Sensors;

/// <summary>
/// Seçili sensör listesini sağlamak için soyutlama
/// </summary>
public interface ISelectedSensorsProvider
{
    /// <summary>
    /// Veritabanından seçili sensörlerin listesini getirir
    /// </summary>
    Task<IReadOnlyList<string>> GetSelectedSensorsAsync(CancellationToken cancellationToken = default);
}
