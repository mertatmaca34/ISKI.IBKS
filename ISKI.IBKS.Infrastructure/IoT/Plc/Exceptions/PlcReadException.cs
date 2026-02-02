using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.IoT.Plc.Exceptions;

public class PlcReadException : Exception
{
    public int DbNumber { get; }
    public int StartByteAddress { get; }
    public int Size { get; }
    public byte[] Buffer { get; }

    public PlcReadException(int dbNumber, int startByteAddress, int size, byte[] buffer, string message)
        : base($"Plc'de DB okuması yaparken hata oluştu. DB: {dbNumber} Başlangıç Biti: {startByteAddress}, Boyut: {size}, Byte Dizisi: {buffer} Error: {message}")
    {
        DbNumber = dbNumber;
        StartByteAddress = startByteAddress;
        Size = size;
        Buffer = buffer;
    }
}

