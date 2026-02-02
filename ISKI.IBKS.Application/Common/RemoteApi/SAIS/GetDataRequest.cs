namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public record GetDataRequest(
    Guid StationId,
    DateTime StartDate,
    DateTime EndDate,
    int Period);
