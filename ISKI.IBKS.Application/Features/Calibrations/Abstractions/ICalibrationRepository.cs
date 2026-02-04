using ISKI.IBKS.Application.Common.Persistence;
using ISKI.IBKS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Features.Calibrations.Abstractions;

public interface ICalibrationRepository : IAsyncRepository<Calibration, Guid>
{
}
