using ISKI.IBKS.Infrastructure.IoT.Plc.Abstract;
using ISKI.IBKS.Infrastructure.IoT.Plc.Exceptions;
using Sharp7;
using System.Net;

namespace ISKI.IBKS.Infrastructure.IoT.Plc.Sharp7;

public class Sharp7Client : IPlcClient
{
    private S7Client S7Client { get; set; }

    public Sharp7Client()
    {
        S7Client = new S7Client
        {
            ConnTimeout = 5000,
            SendTimeout = 5000
        };
    }

    public Task ConnectAsync(string ipAddress, int rack, int slot)
    {
        var res = S7Client.ConnectTo(ipAddress, rack, slot);

        if (res != 0)
        {
            var errorText = S7Client.ErrorText(res);

            throw new PlcConnectionException(ipAddress, rack, slot, errorText);
        }

        return Task.CompletedTask;
    }

    public Task DisconnectAsync()
    {
        if (S7Client.Connected)
        {
            var res = S7Client.Disconnect();

            if (res != 0)
            {
                var errorText = S7Client.ErrorText(res);

                throw new PlcConnectionException(errorText);
            }
        }

        return Task.CompletedTask;
    }

    public Task<byte[]> ReadBytesAsync(int dbNumber, int startByteAddress, int size)
    {
        throw new NotImplementedException();
    }

    public Task WriteBytesAsync(int dbNumber, int startByteAddress, byte[] data)
    {
        throw new NotImplementedException();
    }
}
