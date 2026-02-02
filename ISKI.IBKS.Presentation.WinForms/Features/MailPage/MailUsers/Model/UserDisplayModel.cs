namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailUsers.Model;

public class UserDisplayModel
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string Department { get; set; } = "";
    public string Title { get; set; } = "";
    public string Status { get; set; } = "";
    public string EmailNotifications { get; set; } = "";
    public string MinPriorityLevel { get; set; } = "";
}
