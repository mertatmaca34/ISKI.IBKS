using System;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Common.IoT.Plc;

public interface IPlcClient : IDisposable
{
    bool IsConnected { get; }
    TimeSpan Uptime { get; }
    Task<bool> ConnectAsync(string ip, int rack, int slot, CancellationToken ct = default);
    Task DisconnectAsync(CancellationToken ct = default);
    Task<byte[]> ReadBytesAsync(int db, int offset, int size, CancellationToken ct = default);
    Task<bool> WriteBitAsync(int db, int offset, int bit, bool value, CancellationToken ct = default);
}
