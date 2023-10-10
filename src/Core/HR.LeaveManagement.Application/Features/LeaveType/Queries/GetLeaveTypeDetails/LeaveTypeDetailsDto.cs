namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public sealed class LeaveTypeDetailsDto
{
    public int Id { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}