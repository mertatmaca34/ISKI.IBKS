using System;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Common.Configuration;

public interface IIisDeploymentService
{
    Task<bool> EnsureIisInstalledAsync(IProgress<string>? progress = null);
    Task<bool> DeployApiAsync(DeploymentConfig config, IProgress<string>? progress = null);
}
