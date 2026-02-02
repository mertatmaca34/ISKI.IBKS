using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public sealed record GetCalibrationRequest
{
    public Guid StationId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}

