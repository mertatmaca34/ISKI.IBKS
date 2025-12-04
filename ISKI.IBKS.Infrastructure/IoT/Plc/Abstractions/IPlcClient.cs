using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.IoT.Plc.Abstractions;

public interface IPlcClient
{
     public void Connect(string ipAddress, int rack, int slot);
     public void Disconnect();
     public byte[] ReadBytes(int dbNumber, int startByteAddress, byte[] buffer);
     public void WriteBytes(int dbNumber, int startByteAddress, byte[] buffer);
}
