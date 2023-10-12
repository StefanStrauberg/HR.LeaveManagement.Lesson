namespace HR.LeaveManagement.Application.Models.Email;

public sealed class EmailSettings
{
    public string FromAddress { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
}