namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public record GetUnitsResponse(
    bool Result,
    string Message,
    object? Objects = null);
