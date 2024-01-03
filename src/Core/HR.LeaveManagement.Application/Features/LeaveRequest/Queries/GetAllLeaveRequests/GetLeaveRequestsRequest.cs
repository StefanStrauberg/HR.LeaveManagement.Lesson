using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

public sealed record GetLeaveRequestsRequest : IRequest<List<LeaveRequestDto>>;
