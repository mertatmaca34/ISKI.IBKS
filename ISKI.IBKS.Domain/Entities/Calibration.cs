using ISKI.IBKS.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Domain.Entities;

public sealed class Calibration : AuditableEntity<Guid>
{
    public Guid StationId { get; private set; }
    public string DbColumnName { get; private set; } = string.Empty;
    public DateTime CalibrationDate { get; private set; }

    public double ZeroRef { get; private set; }
    public double ZeroMeas { get; private set; }
    public double ZeroDiff { get; private set; }
    public double ZeroStd { get; private set; }

    public double SpanRef { get; private set; }
    public double SpanMeas { get; private set; }
    public double SpanDiff { get; private set; }
    public double SpanStd { get; private set; }

    public double ResultFactor { get; private set; }
    public bool ResultZero { get; private set; }
    public bool ResultSpan { get; private set; }
    public bool Result { get; private set; }

    private Calibration() { }

    public Calibration(
        Guid stationId,
        string dbColumnName,
        DateTime calibrationDate,
        double zeroRef, double zeroMeas, double zeroDiff, double zeroStd,
        double spanRef, double spanMeas, double spanDiff, double spanStd,
        double resultFactor, bool resultZero, bool resultSpan, bool result)
    {
        StationId = stationId;
        DbColumnName = dbColumnName;
        CalibrationDate = calibrationDate;

        ZeroRef = zeroRef; ZeroMeas = zeroMeas; ZeroDiff = zeroDiff; ZeroStd = zeroStd;
        SpanRef = spanRef; SpanMeas = spanMeas; SpanDiff = spanDiff; SpanStd = spanStd;

        ResultFactor = resultFactor;
        ResultZero = resultZero;
        ResultSpan = resultSpan;
        Result = result;
    }
}

