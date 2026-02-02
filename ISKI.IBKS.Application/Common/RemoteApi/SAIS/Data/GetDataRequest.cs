namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS.Data;

public sealed record GetDataRequest
{
    public Guid StationId { get; init; }
    public int Period { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}

