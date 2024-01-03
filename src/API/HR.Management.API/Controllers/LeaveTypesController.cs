using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.Management.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class LeaveTypesController(IMediator mediator) : ControllerBase
{
    readonly IMediator _mediator = mediator
        ?? throw new ArgumentNullException(nameof(mediator));

    // GET: api/v1/<LeaveTypesController>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<LeaveTypeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetLeaveTypesRequest(),
                                   cancellationToken));

    // GET: api/v1/<LeaveTypesController>/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(LeaveTypeDetailsDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(int id,
                                         CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetLeaveTypeDetailsRequest(id),
                                   cancellationToken));

    // POST: api/v1/<LeaveTypesController>
    [HttpPost]
    [ProducesResponseType(typeof(LeaveTypeDetailsDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post(CreateLeaveTypeCommand leaveType,
                                          CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(leaveType,
                                            cancellationToken);
        return CreatedAtAction(nameof(Get),
                               new { id = response });
    }

    // PUT api/<LeaveTypesController>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Put(UpdateLeaveTypeCommand leaveType,
                                         CancellationToken cancellationToken)
    {
        await _mediator.Send(leaveType,
                             cancellationToken);
        return NoContent();
    }

    // DELETE: api/<LeaveTypesController>/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id,
                                            CancellationToken cancellationToken)
    {
        var command = new DeleteLeaveTypeCommand(id);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}