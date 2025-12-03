using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.IoT.Plc.Abstract;

public interface IPlcClient
{
    Task ConnectAsync(string ipAddress, int rack, int slot);
    Task DisconnectAsync();
    Task<byte[]> ReadBytesAsync(int dbNumber, int startByteAddress, int size);
    Task WriteBytesAsync(int dbNumber, int startByteAddress, byte[] data);
}
