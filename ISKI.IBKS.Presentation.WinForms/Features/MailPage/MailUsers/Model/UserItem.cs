namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailUsers.Model;

public class UserItem
{
    public Guid UserId { get; }
    public string DisplayName { get; }

    public UserItem(Guid userId, string displayName)
    {
        UserId = userId;
        DisplayName = displayName;
    }

    public override string ToString() => DisplayName;
}
