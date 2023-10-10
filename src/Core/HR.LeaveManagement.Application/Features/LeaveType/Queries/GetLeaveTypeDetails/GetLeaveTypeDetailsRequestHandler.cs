using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

internal sealed class GetLeaveTypeDetailsRequestHandler : IRequestHandler<GetLeaveTypeDetailsRequest, LeaveTypeDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public GetLeaveTypeDetailsRequestHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }

    async Task<LeaveTypeDetailsDto> IRequestHandler<GetLeaveTypeDetailsRequest, LeaveTypeDetailsDto>.Handle(GetLeaveTypeDetailsRequest request, CancellationToken cancellationToken)
    {
        // Query the DB
        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.Id, cancellationToken);
        // Convert data object to DTO object
        var data = _mapper.Map<LeaveTypeDetailsDto>(leaveType);
        // Return DTO object
        return data;
    }
}