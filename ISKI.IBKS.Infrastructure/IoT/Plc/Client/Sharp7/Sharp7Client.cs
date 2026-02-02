using System;
using System.Threading;
using System.Threading.Tasks;
using ISKI.IBKS.Application.Common.IoT.Plc;
using Sharp7;

namespace ISKI.IBKS.Infrastructure.IoT.Plc.Client.Sharp7;

public sealed class Sharp7Client : IPlcClient
{
    private readonly S7Client _client = new();
    private DateTime? _connectedAt;

    public bool IsConnected => _client.Connected;

    public TimeSpan Uptime => (IsConnected && _connectedAt.HasValue) ? DateTime.Now - _connectedAt.Value : TimeSpan.Zero;

    public async Task<bool> ConnectAsync(string ip, int rack, int slot, CancellationToken ct = default)
    {
        if (IsConnected) return true;

        return await Task.Run(() =>
        {
            int res = _client.ConnectTo(ip, rack, slot);
            if (res == 0) _connectedAt = DateTime.Now;
            return res == 0;
        }, ct);
    }

    public async Task DisconnectAsync(CancellationToken ct = default)
    {
        await Task.Run(() =>
        {
            _client.Disconnect();
            _connectedAt = null;
        }, ct);
    }

    public async Task<bool> WriteBitAsync(int db, int offset, int bit, bool value, CancellationToken ct = default)
    {
        return await Task.Run(() =>
        {
            byte[] buf = new byte[1];
            _client.DBRead(db, offset, 1, buf);
            if (value) buf[0] |= (byte)(1 << bit);
            else buf[0] &= (byte)~(1 << bit);
            return _client.DBWrite(db, offset, 1, buf) == 0;
        }, ct);
    }

    public async Task<byte[]> ReadBytesAsync(int db, int offset, int size, CancellationToken ct = default)
    {
        return await Task.Run(() =>
        {
            byte[] buf = new byte[size];
            int res = _client.DBRead(db, offset, size, buf);
            return res == 0 ? buf : throw new Exception($"PLC read error: {res}");
        }, ct);
    }

    public void Dispose()
    {
        _client.Disconnect();
    }
}
