namespace HR.LeaveManagement.Application.Exceptions;

public sealed class NotFoundException : Exception
{
    public NotFoundException(string name, object key) : base($"{name} ({key}) wasn't found")
    {
    }
}