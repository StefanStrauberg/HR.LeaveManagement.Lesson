using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

internal sealed class UpdateLeaveTypeCommandHandler(IMapper mapper,
                                                    ILeaveTypeRepository leaveTypeRepository,
                                                    IAppLogger<UpdateLeaveTypeCommandHandler> logger) : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));
    readonly ILeaveTypeRepository _leaveTypeRepository = leaveTypeRepository
        ?? throw new ArgumentNullException(nameof(leaveTypeRepository));
    readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger = logger
        ?? throw new ArgumentNullException(nameof(logger));

    async Task<Unit> IRequestHandler<UpdateLeaveTypeCommand, Unit>.Handle(UpdateLeaveTypeCommand request, 
                                                                          CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new UpdateLeaveTypeCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request,
                                                             cancellationToken);

        if (validationResult.Errors.Count != 0)
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}",
                               nameof(LeaveType),
                               request.Id);
            throw new BadRequestException("Invalid Leave type",
                                          validationResult);
        }

        // Convert to domain entity object
        var leaveTypeToUpdate = await _leaveTypeRepository.GetByIdAsync(request.Id, cancellationToken);
        _mapper.Map(request, leaveTypeToUpdate, typeof(UpdateLeaveTypeCommand), typeof(Domain.LeaveType));

        // Add to DB
        await _leaveTypeRepository.UpdateTask(leaveTypeToUpdate!,
                                              cancellationToken);

        // Return
        return Unit.Value;
    }
}