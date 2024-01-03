using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

internal class GetLeaveRequestsRequestHandler(ILeaveRequestRepository leaveRequestRepository,
                                              IMapper mapper) : IRequestHandler<GetLeaveRequestsRequest, List<LeaveRequestDto>>
{
    readonly ILeaveRequestRepository _leaveRequestRepository = leaveRequestRepository;
    readonly IMapper _mapper = mapper;

    async Task<List<LeaveRequestDto>> IRequestHandler<GetLeaveRequestsRequest, List<LeaveRequestDto>>.Handle(GetLeaveRequestsRequest request,
                                                                                                             CancellationToken cancellationToken)
    {
        var leaveRequests = await _leaveRequestRepository.GetAsync(cancellationToken);
        return _mapper.Map<List<LeaveRequestDto>>(leaveRequests);
    }
}
