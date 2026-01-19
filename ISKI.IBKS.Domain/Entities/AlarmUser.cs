using ISKI.IBKS.Domain.Common.Entities;

namespace ISKI.IBKS.Domain.Entities;

/// <summary>
/// Alarm bildirimlerini alacak kullanıcıları temsil eder.
/// </summary>
public sealed class AlarmUser : AuditableEntity<Guid>
{
    public string FullName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string? PhoneNumber { get; private set; }
    public string? Department { get; private set; }
    public string? Title { get; private set; }
    
    /// <summary>
    /// Kullanıcı aktif mi?
    /// </summary>
    public bool IsActive { get; private set; } = true;
    
    /// <summary>
    /// E-posta bildirimleri açık mı?
    /// </summary>
    public bool ReceiveEmailNotifications { get; private set; } = true;
    
    /// <summary>
    /// Hangi öncelik seviyesinden itibaren bildirim alacak
    /// </summary>
    public AlarmPriority MinimumPriorityLevel { get; private set; } = AlarmPriority.Medium;

    private AlarmUser() { }

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public AlarmUser(string fullName, string email, string? phoneNumber = null, string? department = null, string? title = null)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        Department = department;
        Title = title;
        IsActive = true;
        ReceiveEmailNotifications = true;
    }

    public void Update(string fullName, string email, string? phoneNumber, string? department, string? title,
        bool isActive, bool receiveEmailNotifications, AlarmPriority minimumPriorityLevel)
    {
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        Department = department;
        Title = title;
        IsActive = isActive;
        ReceiveEmailNotifications = receiveEmailNotifications;
        MinimumPriorityLevel = minimumPriorityLevel;
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;

    public void SetNotificationPreferences(bool receiveEmail, AlarmPriority minimumPriority)
    {
        ReceiveEmailNotifications = receiveEmail;
        MinimumPriorityLevel = minimumPriority;
    }
}

/// <summary>
/// Alarm ve kullanıcı arasındaki ilişkiyi temsil eder.
/// Hangi kullanıcının hangi alarmları alacağını belirler.
/// </summary>
public sealed class AlarmUserSubscription : Entity<Guid>
{
    public Guid AlarmDefinitionId { get; private set; }
    public Guid AlarmUserId { get; private set; }
    public AlarmUser AlarmUser { get; private set; } = null!;
    public bool IsActive { get; private set; } = true;

    private AlarmUserSubscription() { }

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public AlarmUserSubscription(Guid alarmDefinitionId, Guid alarmUserId)
    {
        Id = Guid.NewGuid();
        AlarmDefinitionId = alarmDefinitionId;
        AlarmUserId = alarmUserId;
        IsActive = true;
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}
