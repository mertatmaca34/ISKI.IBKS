using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Models;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Models.Channel;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Models.Login;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Models.SendData;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;

public interface ISaisApiClient
{
    Task<SaisResultEnvelope<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default);
    Task<SaisResultEnvelope<SendDataResponse>> SendDataAsync(SendDataRequest request, CancellationToken ct = default);
    Task<SaisResultEnvelope<GetChannelInformationResponse>> GetChannelInformation(GetChannelInformationRequest request, CancellationToken ct = default);
    Task<SaisResultEnvelope<GetUnitsResponse>> GetUnits(CancellationToken ct = default);
}
