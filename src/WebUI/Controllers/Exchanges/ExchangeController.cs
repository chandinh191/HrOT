using hrOT.Application.Exchanges.Commands.CreateExchange;
using hrOT.Application.Exchanges.Commands.DeleteExchange;
using hrOT.Application.Exchanges.Commands.UpdateExchange;
using hrOT.Application.Exchanges.Queries;
using hrOT.Domain.Entities;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Exchanges;
public class ExchangeController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Exchange>>> Get()
    {
        return await Mediator.Send(new GetListExchangeQuery());
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromForm]CreateExchangeCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateExchangeCommand command)
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
        await Mediator.Send(new DeleteExchangeCommand(id));

        return NoContent();
    }
}
