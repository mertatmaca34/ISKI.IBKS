using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Features.StationStatus.Dtos;

public sealed record StationStatusDto
{
    public bool? IsConnected { get; init; }
    public TimeSpan? UpTime { get; init; }
    public TimeSpan? DailyWashRemainingTime { get; init; }
    public TimeSpan? WeeklyWashRemainingTime { get; init; }
    public DateTime? SystemTime { get; init; }
}
