using ISKI.IBKS.Infrastructure.IoT.Plc.Abstractions;
using ISKI.IBKS.Infrastructure.IoT.Plc.Exceptions;
using Sharp7;
using System.Net;

namespace ISKI.IBKS.Infrastructure.IoT.Plc.Client.Sharp7;

public class Sharp7Client : IPlcClient
{
    private S7Client S7Client { get; set; }

    public bool IsConnected => S7Client.Connected;

    private DateTimeOffset? _connectedTime;
    public TimeSpan Uptime =>
        IsConnected
            ? DateTimeOffset.UtcNow - _connectedTime!.Value
            : TimeSpan.Zero;

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
        if (S7Client.Connected)
            return;

        var res = S7Client.ConnectTo(ipAddress, rack, slot);

        if (res != 0)
        {
            var errorText = S7Client.ErrorText(res);

            throw new PlcConnectionException(ipAddress, rack, slot, errorText);
        }

        _connectedTime = DateTime.Now;
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

    public float ReadReal(byte[] buffer, int byteOffset)
    {
        return S7.GetRealAt(buffer, byteOffset);
    }

    public bool ReadBit(byte[] buffer, int byteOffset, int bitOffset)
    {
        return S7.GetBitAt(buffer, byteOffset, bitOffset);
    }

    public DateTime ReadDateTime(byte[] buffer, int byteOffset)
    {
        return S7.GetDateTimeAt(buffer, byteOffset);
    }

    public byte ReadByte(byte[] buffer, int byteOffset)
    {
        return S7.GetByteAt(buffer, byteOffset);
    }
}
