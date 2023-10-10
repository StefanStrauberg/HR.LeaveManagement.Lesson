using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

internal sealed class GetLeaveTypesRequestHandler : IRequestHandler<GetLeaveTypesRequest, List<LeaveTypeDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public GetLeaveTypesRequestHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }

    async Task<List<LeaveTypeDto>> IRequestHandler<GetLeaveTypesRequest, List<LeaveTypeDto>>.Handle(GetLeaveTypesRequest request, CancellationToken cancellationToken)
    {
        // Query the DB
        var leaveTypes = await _leaveTypeRepository.GetAsync(cancellationToken);
        // Convert data objects to DTO objects
        var data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);
        // Return list of DTO objects
        return data;
    }
}