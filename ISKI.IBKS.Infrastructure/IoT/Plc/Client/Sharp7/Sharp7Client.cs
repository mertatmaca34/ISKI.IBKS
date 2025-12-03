using ISKI.IBKS.Infrastructure.IoT.Plc.Abstract;
using ISKI.IBKS.Infrastructure.IoT.Plc.Exceptions;
using Sharp7;
using System.Net;

namespace ISKI.IBKS.Infrastructure.IoT.Plc.Client.Sharp7;

public class Sharp7Client : IPlcClientSync
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

    public void Connect(string ipAddress, int rack, int slot)
    {
        var res = S7Client.ConnectTo(ipAddress, rack, slot);

        if (res != 0)
        {
            var errorText = S7Client.ErrorText(res);

            throw new PlcConnectionException(ipAddress, rack, slot, errorText);
        }
    }

    public void Disconnect()
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
    }

    public byte[] ReadBytes(int dbNumber, int startByteAddress, byte[] buffer)
    {
        if (S7Client.Connected)
        {
            var res = S7Client.DBRead(dbNumber, startByteAddress, buffer.Length, buffer);

            if (res != 0)
            {
                var errorText = S7Client.ErrorText(res);

                throw new PlcReadException(dbNumber, startByteAddress, buffer.Length, buffer, errorText);
            }

            return buffer;
        }
        else
        {
            throw new PlcConnectionException();
        }
    }

    public void WriteBytes(int dbNumber, int startByteAddress, byte[] data)
    {
        if (S7Client.Connected) 
        {
            var res = S7Client.DBWrite(dbNumber, startByteAddress, data.Length, data);

            if (res != 0)
            {
                var errorText = S7Client.ErrorText(res);

                throw new PlcReadException(dbNumber, startByteAddress, data.Length, data, errorText);
            }
        }
        else
        {
            throw new PlcConnectionException();
        }
    }
}
