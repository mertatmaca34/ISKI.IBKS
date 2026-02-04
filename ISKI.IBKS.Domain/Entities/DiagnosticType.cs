namespace ISKI.IBKS.Domain.Entities;

/// <summary>
/// Diagnostik Tipleri - SAIS'ten alınan diagnostik tip tanımları
/// </summary>
public sealed class DiagnosticType
{
    public int Id { get; private set; }
    
    /// <summary>Diagnostik tip numarası (SAIS'ten)</summary>
    public int DiagnosticTypeNo { get; private set; }
    
    /// <summary>Diagnostik tip adı (örn: "Çevrimiçi", "Çevrimdışı")</summary>
    public required string DiagnosticTypeName { get; set; }
    
    /// <summary>Diagnostik seviyesi</summary>
    public int DiagnosticLevel { get; private set; }
    
    /// <summary>Seviye başlığı (örn: "Başarılı", "Alarm", "Uyarı")</summary>
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
