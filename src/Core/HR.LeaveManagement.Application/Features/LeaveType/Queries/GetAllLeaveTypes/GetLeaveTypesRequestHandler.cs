using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

internal sealed class GetLeaveTypesRequestHandler(IMapper mapper,
                                                  ILeaveTypeRepository leaveTypeRepository,
                                                  IAppLogger<GetLeaveTypesRequestHandler> logger) : IRequestHandler<GetLeaveTypesRequest, IReadOnlyList<LeaveTypeDto>>
{
    readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));
    readonly ILeaveTypeRepository _leaveTypeRepository = leaveTypeRepository
        ?? throw new ArgumentNullException(nameof(leaveTypeRepository));
    readonly IAppLogger<GetLeaveTypesRequestHandler> _logger = logger
        ?? throw new ArgumentNullException(nameof(logger));

    async Task<IReadOnlyList<LeaveTypeDto>> IRequestHandler<GetLeaveTypesRequest, IReadOnlyList<LeaveTypeDto>>.Handle(GetLeaveTypesRequest request, 
                                                                                                                      CancellationToken cancellationToken)
    {
        // Query the DB
        var leaveTypes = await _leaveTypeRepository.GetAsync(cancellationToken);

        // Convert data objects to DTO objects
        var data = _mapper.Map<IReadOnlyList<LeaveTypeDto>>(leaveTypes);
        
        // Return list of DTO objects
        _logger.LogInformation("LeaveTypes were retrieved successfully");
        return data;
    }
}