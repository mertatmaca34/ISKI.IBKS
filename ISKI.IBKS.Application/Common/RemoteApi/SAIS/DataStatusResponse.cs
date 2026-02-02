namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public record DataStatusResponse(
    bool Result,
    string Message,
    object? Objects = null);
