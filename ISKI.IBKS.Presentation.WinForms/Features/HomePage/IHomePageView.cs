using ISKI.IBKS.Application.Features.AnalogSensors.Dtos;
using ISKI.IBKS.Application.Features.DigitalSensors.Dtos;
using ISKI.IBKS.Application.Features.StationStatus.Dtos;
using ISKI.IBKS.Application.Features.HealthSummary.Dtos;
using ISKI.IBKS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISKI.IBKS.Application.Common.Results;

namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage;

public interface IHomePageView
{
    event EventHandler Load;
    event EventHandler Disposed;

    public void RenderAnalogChannels(IDataResult<IReadOnlyList<ChannelReadingDto>> channelReadingDtos);
    public void RenderStationStatusBar(IDataResult<StationStatusDto>? stationStatusDto);
    public void RenderDigitalSensors(IDataResult<IReadOnlyList<DigitalReadingDto>> digitalReadingDtos);
    public void RenderHealthSummary(HealthSummaryDto dto);
}
