
using hrOT.Application.Positions;
using hrOT.Application.Positions.Commands.CreatePosition;
using hrOT.Application.Positions.Commands.DeletePosition;
using hrOT.Application.Positions.Commands.UpdatePosition;
using hrOT.Application.Positions.Queries.GetPosition;
using hrOT.Domain.Entities;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Positions;


[Authorize(Policy = "manager")]
public class PositionController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PositionDTO>>> Get()
    {
        return await Mediator.Send(new GetListPositionQuery());
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreatePositionCommand command)
    {
        return await Mediator.Send(command);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdatePositionCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeletePositionCommand(id));

        return NoContent();

    }
}


