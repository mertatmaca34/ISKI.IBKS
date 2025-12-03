using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.IoT.Plc.Exceptions;

public class PlcConnectionException : Exception
{
    public string? IpAdress { get; set; }
    public int? Rack { get; set; }
    public int? Slot { get; set; }
    public string? ErrorText { get; set; }

    public PlcConnectionException(
           string ipAdress,
           int rack,
           int slot,
           string errorText)
         : base($"Plc'ye bağlanırken sorun oluştu, IP Adres: {ipAdress} Rack: {rack} Slot: {slot} Error: {errorText}")
    {
        IpAdress = ipAdress;
        Rack = rack;
        Slot = slot;
        ErrorText = errorText;
    }

    public PlcConnectionException(string errorText)
        : base($"Plc'den bağlantı kesilirken bir hata oluştu. Error: {errorText}")
    {
    }

    public PlcConnectionException()
        : base($"Plc bağlantısı stabil değil, okuma/yazma yapılamıyor.")
    {
    }
}
