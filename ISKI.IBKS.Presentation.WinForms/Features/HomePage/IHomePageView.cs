using ISKI.IBKS.Application.Features.AnalogSensors.Dtos;
using ISKI.IBKS.Application.Features.StationStatus.Dtos;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Presentation.WinForms.Features.HomePage.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage;

public interface IHomePageView
{
    event EventHandler Load;
    event EventHandler Disposed;

    public void RenderAnalogChannels(IReadOnlyList<ChannelReadingDto> channelReadingDtos);
    public void RenderStationStatusBar(StationStatusDto? stationStatusDto);
}
