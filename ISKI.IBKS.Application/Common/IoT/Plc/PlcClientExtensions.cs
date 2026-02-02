using System;
using System.Buffers.Binary;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Common.IoT.Plc;

public static class PlcClientExtensions
{
    public static async Task<float> ReadRealAsync(this IPlcClient client, int db, int offset, CancellationToken ct = default)
    {
        var bytes = await client.ReadBytesAsync(db, offset, 4, ct);

        return BinaryPrimitives.ReadSingleBigEndian(bytes);
    }

    public static async Task<short> ReadIntAsync(this IPlcClient client, int db, int offset, CancellationToken ct = default)
    {
        var bytes = await client.ReadBytesAsync(db, offset, 2, ct);
        return BinaryPrimitives.ReadInt16BigEndian(bytes);
    }

    public static async Task<int> ReadDIntAsync(this IPlcClient client, int db, int offset, CancellationToken ct = default)
    {
        var bytes = await client.ReadBytesAsync(db, offset, 4, ct);
        return BinaryPrimitives.ReadInt32BigEndian(bytes);
    }

    public static async Task<bool> ReadBoolAsync(this IPlcClient client, int db, int offset, int bit, CancellationToken ct = default)
    {
        var bytes = await client.ReadBytesAsync(db, offset, 1, ct);
        return (bytes[0] & (1 << bit)) != 0;
    }
}
