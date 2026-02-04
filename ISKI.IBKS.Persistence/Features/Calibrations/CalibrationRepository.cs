using ISKI.IBKS.Application.Features.Calibrations.Abstractions;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Persistence.Common.Persistence;
using ISKI.IBKS.Persistence.Contexts;

namespace ISKI.IBKS.Persistence.Features.Calibrations;

public class CalibrationRepository(IbksDbContext dbContext) : EfAsyncRepositoryBase<Calibration, Guid, IbksDbContext>(dbContext), ICalibrationRepository
{

}
