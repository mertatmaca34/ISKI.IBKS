using ISKI.IBKS.Domain.Common.Entities;

namespace ISKI.IBKS.Domain.Entities;

public sealed class Calibration : AuditableEntity<Guid>
{
    public Guid StationId { get; private set; }
    public string SensorName { get; private set; } = string.Empty;
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
    public double ResultZero { get; private set; }
    public double ResultSpan { get; private set; }
    public double Result { get; private set; }
    public string PerformedBy { get; private set; } = string.Empty;
    public string Remarks { get; private set; } = string.Empty;

    public static Calibration Create(Guid stationId, string sensorName, DateTime calibrationDate, string remarks)
    {
        var calibration = new Calibration
        {
            Id = Guid.NewGuid(),
            StationId = stationId,
            SensorName = sensorName,
            DbColumnName = sensorName,
            CalibrationDate = calibrationDate,
            Remarks = remarks,
            PerformedBy = "System"
        };
        return calibration;
    }

    private Calibration() { }

    public Calibration(
        Guid stationId,
        string sensorName,
        string dbColumnName,
        double zeroRef, double zeroMeas, double zeroDiff, double zeroStd,
        double spanRef, double spanMeas, double spanDiff, double spanStd,
        double resultFactor, double resultZero, double resultSpan, double result,
        string performedBy, string remarks = "")
    {
        Id = Guid.NewGuid();
        StationId = stationId;
        SensorName = sensorName;
        DbColumnName = dbColumnName;
        CalibrationDate = DateTime.UtcNow;
        ZeroRef = zeroRef;
        ZeroMeas = zeroMeas;
        ZeroDiff = zeroDiff;
        ZeroStd = zeroStd;
        SpanRef = spanRef;
        SpanMeas = spanMeas;
        SpanDiff = spanDiff;
        SpanStd = spanStd;
        ResultFactor = resultFactor;
        ResultZero = resultZero;
        ResultSpan = resultSpan;
        Result = result;
        PerformedBy = performedBy;
        Remarks = remarks;
    }
}
