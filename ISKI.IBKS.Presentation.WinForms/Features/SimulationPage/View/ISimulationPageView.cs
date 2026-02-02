using System.Threading.Tasks;
using ISKI.IBKS.Domain.IoT;

namespace ISKI.IBKS.Presentation.WinForms.Features.SimulationPage.View;

public interface ISimulationPageView
{
    event EventHandler Load;
    event EventHandler Disposed;

    void UpdateDisplay(PlcDataSnapshot snapshot, bool blinkState);
    void ShowError(string message);
}

