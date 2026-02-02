namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public record ParameterResponse(
    bool Result,
    string Message,
    object? Objects = null);
