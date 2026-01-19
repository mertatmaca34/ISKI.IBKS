using ISKI.IBKS.Application.Features.Plc.Abstractions;
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
        IsConnected && _connectedTime.HasValue
            ? DateTimeOffset.UtcNow - _connectedTime.Value
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

    public void ForceReconnect(string ipAddress, int rack, int slot)
    {
        // Önce mevcut bağlantıyı kapat (hata olsa bile devam et)
        try
        {
            if (S7Client.Connected)
            {
                S7Client.Disconnect();
            }
        }
        catch
        {
            // Disconnect hatalarını yoksay
        }

        // Yeni S7Client oluştur (eski bağlantı durumunu temizle)
        S7Client = new S7Client
        {
            ConnTimeout = 5000,
            SendTimeout = 5000
        };

        _connectedTime = null;

        // Yeniden bağlan
        var res = S7Client.ConnectTo(ipAddress, rack, slot);

        if (res != 0)
        {
            var errorText = S7Client.ErrorText(res);
            throw new PlcConnectionException(ipAddress, rack, slot, errorText);
        }

        _connectedTime = DateTime.Now;
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
        return S7.GetDTLAt(buffer, byteOffset);
    }

    public byte ReadByte(byte[] buffer, int byteOffset)
    {
        return S7.GetByteAt(buffer, byteOffset);
    }

    public void WriteBit(int dbNumber, int byteOffset, int bitOffset, bool value)
    {
        if (S7Client.Connected)
        {
            var buffer = new byte[1];
            buffer[0] = value ? (byte)1 : (byte)0;
            
            // S7Consts.S7AreaDB = 0x84
            // Amount = 1 (1 bit)
            // WordLen = S7Consts.S7WLBit = 0x01
            // Start = (byteOffset * 8) + bitOffset
            int start = (byteOffset * 8) + bitOffset;
            
            var res = S7Client.WriteArea(S7Consts.S7AreaDB, dbNumber, start, 1, S7Consts.S7WLBit, buffer);

            if (res != 0)
            {
                var errorText = S7Client.ErrorText(res);
                throw new PlcReadException(dbNumber, byteOffset, 1, buffer, errorText);
            }
        }
        else
        {
            throw new PlcConnectionException();
        }
    }
}
