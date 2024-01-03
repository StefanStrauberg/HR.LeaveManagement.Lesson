using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

internal class GetLeaveRequestDetailsRequestHandler(ILeaveRequestRepository leaveRequestRepository,
                                                    IMapper mapper) : IRequestHandler<GetLeaveRequestDetailsRequest, LeaveRequestDetailsDto>
{
    readonly ILeaveRequestRepository _leaveRequestRepository = leaveRequestRepository
        ?? throw new ArgumentNullException(nameof(leaveRequestRepository));
    readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    async Task<LeaveRequestDetailsDto> IRequestHandler<GetLeaveRequestDetailsRequest, LeaveRequestDetailsDto>.Handle(GetLeaveRequestDetailsRequest request,
                                                                                                                     CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id,
                                                                      cancellationToken);
        
        if (leaveRequest is null)
            throw new NotFoundException(nameof(leaveRequest), request.Id);
        
        return _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);
    }
}
