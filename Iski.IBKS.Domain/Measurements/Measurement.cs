using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iski.IBKS.Domain.Measurements
{
    internal class Measurement
    {
        public Guid Id { get; private set; }
        public Guid StationId { get; private set; }
        public DateTime Readtime { get; private set; }
        public string SoftwareVersion { get; private set; }
        public decimal AkisHizi { get; private set; }
        public decimal Akm { get; set; }
        public decimal CozunmusOksijen { get; set; }
        public decimal Debi { get; private set; }
        public decimal DesarjDebi { get; private set; }
        public decimal HariciDebi { get; private set; }
        public decimal HariciDebi2 { get; private set; }
        public decimal Koi { get; private set; }
        public decimal Ph { get; private set; }
        public decimal Sicaklik { get; private set; }
        public decimal Iletkenlik { get; private set; }
        public int Period { get; private set; }
        public MeasurementStatus Status { get; private set; }
    }
}
