using System;

namespace ISKI.IBKS.Application.Common.Features.Operations.StartSample;

public record StartSampleCommand(Guid StationId, string Code);
