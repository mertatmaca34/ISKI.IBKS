using ISKI.IBKS.Domain.Common.Entities;
using ISKI.IBKS.Domain.Enums;

namespace ISKI.IBKS.Domain.Entities;

public sealed class AlarmUser : AuditableEntity<Guid>
{
    public string FullName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string? PhoneNumber { get; private set; }
    public string? Department { get; private set; }
    public string? Title { get; private set; }

    public bool IsActive { get; private set; } = true;

    public bool ReceiveEmailNotifications { get; private set; } = true;

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

