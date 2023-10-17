using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public sealed record GetLeaveTypesRequest : IRequest<IReadOnlyList<LeaveTypeDto>>;