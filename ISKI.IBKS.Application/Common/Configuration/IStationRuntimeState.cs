namespace ISKI.IBKS.Application.Common.Configuration;

public interface IStationRuntimeState
{
    DateTime? LastDataDate { get; set; }
    DateTime? LastSaisSync { get; set; }

    Task SaveAsync(CancellationToken ct = default);

    Task LoadAsync(CancellationToken ct = default);
}

