using System;

namespace ISKI.IBKS.Application.Common.Configuration;

public record DeploymentConfig(
    string ZipPath,
    string DestinationPath,
    Guid StationId,
    string LocalIp,
    int Port);
