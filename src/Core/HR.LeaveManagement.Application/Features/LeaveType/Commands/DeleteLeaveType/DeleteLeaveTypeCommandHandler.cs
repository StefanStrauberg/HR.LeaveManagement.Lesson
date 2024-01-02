using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

internal sealed class DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository) : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    readonly ILeaveTypeRepository _leaveTypeRepository = leaveTypeRepository
        ?? throw new ArgumentNullException(nameof(leaveTypeRepository));

    async Task<Unit> IRequestHandler<DeleteLeaveTypeCommand, Unit>.Handle(DeleteLeaveTypeCommand request, 
                                                                          CancellationToken cancellationToken)
    {
        // Retrieve domain entity object and verify that record exists
        var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id,
                                                                        cancellationToken) 
            ?? throw new Exception();

        // Remove from DB
        await _leaveTypeRepository.DeleteTask(leaveTypeToDelete,
                                              cancellationToken);

        // Return
        return Unit.Value;
    }
}