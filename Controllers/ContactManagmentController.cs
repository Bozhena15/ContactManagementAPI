using ContactManagementAPI.Commands;
using ContactManagementAPI.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactManagmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContactManagmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ContactDto>> CreateContact(
        [FromBody] ContactDto contactDto,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateContactCommand(contactDto), cancellationToken);
        return CreatedAtAction(nameof(CreateContact), new { id = result.Id }, result);
    }

    [HttpPut]
    public async Task<ActionResult<ContactDto?>> UpdateContact(
        [FromBody] ContactDto contactDto,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateContactCommand(contactDto), cancellationToken);

        return result is not null
            ? Ok(result)
            : NotFound(); ;
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteContact(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteContactCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<PagedResultDto<ContactDto>>> GetContacts(
        [FromQuery] ContactQueryDto query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetContactsCommand(query), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ContactDto?>> GetContactById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetContactByIdCommand(id), cancellationToken);
        return result is not null
            ? Ok(result)
            : NotFound();
    }
}
