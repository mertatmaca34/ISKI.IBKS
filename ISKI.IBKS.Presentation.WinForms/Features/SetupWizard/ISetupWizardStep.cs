namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard;

/// <summary>
/// Kurulum sihirbazı için temel adım arayüzü
/// </summary>
public interface ISetupWizardStep
{
    /// <summary>
    /// Adımın başlığı
    /// </summary>
    string Title { get; }

    /// <summary>
    /// Adımın açıklaması
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Adım numarası (1-based)
    /// </summary>
    int StepNumber { get; }

    /// <summary>
    /// Mevcut değerleri yükler
    /// </summary>
    Task LoadAsync(CancellationToken ct = default);

    /// <summary>
    /// Girilen değerleri doğrular
    /// </summary>
    /// <returns>Doğrulama başarılı ise true</returns>
    (bool IsValid, string? ErrorMessage) Validate();

    /// <summary>
    /// Girilen değerleri kaydeder
    /// </summary>
    Task<bool> SaveAsync(CancellationToken ct = default);

    /// <summary>
    /// Adım kontrolünü döndürür
    /// </summary>
    Control GetControl();
}
