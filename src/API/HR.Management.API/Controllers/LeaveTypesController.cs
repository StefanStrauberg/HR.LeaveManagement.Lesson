using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.Management.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveTypesController(IMediator mediator)
        => _mediator = mediator;

    // GET: api/<LeaveTypesController>
    [HttpGet]
    public async Task<IReadOnlyList<LeaveTypeDto>> Get(CancellationToken cancellationToken)
        => await _mediator.Send(new GetLeaveTypesRequest(), cancellationToken);

    // GET: api/<LeaveTypesController>/5
    [HttpGet("{id:int}")]
    public async Task<LeaveTypeDetailsDto> Get(int id, 
                                               CancellationToken cancellationToken)
        => await _mediator.Send(new GetLeaveTypeDetailsRequest(id), cancellationToken);

    // POST: api/<LeaveTypesController>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Post(CreateLeaveTypeCommand leaveType, 
                                         CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(leaveType, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT api/<LeaveTypesController>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveTypeCommand leaveType, 
                                        CancellationToken cancellationToken)
    {
        await _mediator.Send(leaveType, cancellationToken);
        return NoContent();
    }

    // DELETE: api/<LeaveTypesController>/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id, 
                                           CancellationToken cancellationToken)
    {
        var command = new DeleteLeaveTypeCommand(id);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}