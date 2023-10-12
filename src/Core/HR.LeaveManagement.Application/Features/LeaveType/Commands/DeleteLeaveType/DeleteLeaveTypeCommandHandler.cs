using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

internal sealed class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
        => _leaveTypeRepository = leaveTypeRepository;

    async Task<Unit> IRequestHandler<DeleteLeaveTypeCommand, Unit>.Handle(DeleteLeaveTypeCommand request, 
                                                                          CancellationToken cancellationToken)
    {
        // Retrieve domain entity object and verify that record exists
        var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new Exception();
        // Remove from DB
        await _leaveTypeRepository.DeleteTask(leaveTypeToDelete, cancellationToken);
        // Return
        return Unit.Value;
    }
}