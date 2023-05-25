using hrOT.Application.TaxInComes.Commands.CreateTaxInCome;
using hrOT.Application.TaxInComes.Commands.DeleteTaxInCome;
using hrOT.Application.TaxInComes.Commands.UpdateTaxInCome;
using hrOT.Application.TaxInComes.Queries;
using hrOT.Domain.Entities;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.TaxInComes;
public class TaxInComeController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TaxInCome>>> Get()
    {
        return await Mediator.Send(new GetListTaxInComeQuery());
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateTaxInComeCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateTaxInComeCommand command)
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
        await Mediator.Send(new DeleteTaxInComeCommand(id));

        return NoContent();
    }
}
