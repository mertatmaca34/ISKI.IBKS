namespace ISKI.IBKS.Application.Features.Plc.Abstractions;

public interface IPlcClient
{
    public bool IsConnected { get; }
    public TimeSpan Uptime { get; }
    public void Connect(string ipAddress, int rack, int slot);
    public void Disconnect();
    public byte[] ReadBytes(int dbNumber, int startByteAddress, byte[] buffer);
    public byte ReadByte(byte[] buffer, int byteOffset);
    public void WriteBytes(int dbNumber, int startByteAddress, byte[] buffer);
    public float ReadReal(byte[] buffer, int byteOffset);
    public bool ReadBit(byte[] buffer, int byteOffset, int bitOffset);
    public void WriteBit(int dbNumber, int byteOffset, int bitOffset, bool value);
    public DateTime ReadDateTime(byte[] buffer, int byteOffset);
}
