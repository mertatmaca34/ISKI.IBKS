namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public record MissingDatesResponse(
    bool Result,
    string Message,
    List<string> Objects);
