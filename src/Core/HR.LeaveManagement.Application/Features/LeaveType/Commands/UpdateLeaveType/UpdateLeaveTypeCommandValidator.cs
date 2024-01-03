using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

internal sealed class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(x => x.Id).NotNull()
                          .MustAsync(LeaveTypeMustExists);

        RuleFor(x => x.Name).NotEmpty()
                            .WithMessage("{PropertyName} is required")
                            .NotNull()
                            .MaximumLength(70)
                            .WithMessage("{PropertyName} have to be fewer than 70 characters");

        RuleFor(x => x.DefaultDays).GreaterThan(1)
                                   .WithMessage("{PropertyName} can't be less than 1")
                                   .LessThan(100)
                                   .WithMessage("{PropertyName} can't exceed 100");
        
        RuleFor(x => x).MustAsync(LeaveTypeNameUnique)
                       .WithMessage("Leave type already exists");
    }

    private async Task<bool> LeaveTypeMustExists(int id,
                                                 CancellationToken cancellationToken)
        => await _leaveTypeRepository.CheckEntityExistsByIdAsync(id,
                                                                 cancellationToken);
    
    private async Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command,
                                                 CancellationToken cancellationToken)
        => !await _leaveTypeRepository.IsLeaveTypeUnique(command.Name,
                                                         command.Id,
                                                         cancellationToken);
}