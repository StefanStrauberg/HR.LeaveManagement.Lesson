using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

internal sealed class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandHandler(IMapper mapper, 
                                         ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }

    async Task<int> IRequestHandler<CreateLeaveTypeCommand, int>.Handle(CreateLeaveTypeCommand request, 
                                                                        CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid Leave type", validationResult);
        // Convert to domain entity object
        var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);
        // Add to DB
        await _leaveTypeRepository.CreateAsync(leaveTypeToCreate, cancellationToken);
        // Return record Id
        return leaveTypeToCreate.Id;
    }
}