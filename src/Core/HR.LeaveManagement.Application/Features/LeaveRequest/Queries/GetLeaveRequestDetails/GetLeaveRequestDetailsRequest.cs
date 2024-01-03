using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public sealed record GetLeaveRequestDetailsRequest(int Id) : IRequest<LeaveRequestDetailsDto>;
