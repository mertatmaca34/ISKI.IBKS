namespace ISKI.IBKS.Infrastructure.IoT.Plc.Abstractions;

public interface IPlcClient
{
    public bool IsConnected { get; }
    public void Connect(string ipAddress, int rack, int slot);
    public void Disconnect();
    public byte[] ReadBytes(int dbNumber, int startByteAddress, byte[] buffer);
    public void WriteBytes(int dbNumber, int startByteAddress, byte[] buffer);
    public float ReadReal(byte[] buffer, int byteOffset);
    public bool ReadBit(byte[] buffer, int byteOffset, int bitOffset);
    public DateTime ReadDateTime(byte[] buffer, int byteOffset);
}
