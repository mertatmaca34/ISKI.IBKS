namespace ISKI.IBKS.Domain.Entities;

public sealed class DiagnosticType
{
    public int Id { get; private set; }

    public int DiagnosticTypeNo { get; private set; }

    public required string DiagnosticTypeName { get; set; }

    public int DiagnosticLevel { get; private set; }

    public string? DiagnosticLevelTitle { get; private set; }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }

    private DiagnosticType() { }

    public DiagnosticType(int diagnosticTypeNo, string diagnosticTypeName, int diagnosticLevel, string? levelTitle)
    {
        DiagnosticTypeNo = diagnosticTypeNo;
        DiagnosticTypeName = diagnosticTypeName;
        DiagnosticLevel = diagnosticLevel;
        DiagnosticLevelTitle = levelTitle;
    }

    public void Update(string diagnosticTypeName, int diagnosticLevel, string? levelTitle)
    {
        DiagnosticTypeName = diagnosticTypeName;
        DiagnosticLevel = diagnosticLevel;
        DiagnosticLevelTitle = levelTitle;
        UpdatedAt = DateTime.UtcNow;
    }
}

