namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailStatements.Model;

public class AlarmDisplayModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string AlarmUsers { get; set; } = "";
    public string Description { get; set; } = "";
    public string Type { get; set; } = "";
    public string Priority { get; set; } = "";
    public string Status { get; set; } = "";
}
