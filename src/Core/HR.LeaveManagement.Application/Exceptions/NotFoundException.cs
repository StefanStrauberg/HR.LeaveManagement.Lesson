namespace HR.LeaveManagement.Application.Exceptions;

public sealed class NotFoundException(string name, object key) : Exception($"{name} ({key}) wasn't found")
{
}