using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public sealed record GetLeaveTypeDetailsRequest(int Id) : IRequest<LeaveTypeDetailsDto>;