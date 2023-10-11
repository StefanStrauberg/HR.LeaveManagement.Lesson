using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public sealed record DeleteLeaveTypeCommand(int Id) : IRequest<Unit>;