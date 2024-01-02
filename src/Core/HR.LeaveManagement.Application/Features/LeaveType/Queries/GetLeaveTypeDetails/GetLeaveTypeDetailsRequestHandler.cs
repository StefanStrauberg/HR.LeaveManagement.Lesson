using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

internal sealed class GetLeaveTypeDetailsRequestHandler(IMapper mapper,
                                                        ILeaveTypeRepository leaveTypeRepository) : IRequestHandler<GetLeaveTypeDetailsRequest, LeaveTypeDetailsDto>
{
    readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));
    readonly ILeaveTypeRepository _leaveTypeRepository = leaveTypeRepository
        ?? throw new ArgumentNullException(nameof(leaveTypeRepository));

    async Task<LeaveTypeDetailsDto> IRequestHandler<GetLeaveTypeDetailsRequest, LeaveTypeDetailsDto>.Handle(GetLeaveTypeDetailsRequest request,
                                                                                                            CancellationToken cancellationToken)
    {
        // Query the DB
        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.Id,
                                                                cancellationToken);

        // Convert data object to DTO object
        var data = _mapper.Map<LeaveTypeDetailsDto>(leaveType);

        // Return DTO object
        return data;
    }
}