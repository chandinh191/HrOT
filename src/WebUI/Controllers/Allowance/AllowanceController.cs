using hrOT.Application.Allowances.Command.Update;
using hrOT.Application.Allowances.Queries;
using hrOT.Application.Allowances.Command.Create;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using hrOT.Application.Allowances.Command.Delete;


namespace WebUI.Controllers.Allowance;
public class AllowanceController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<AllowanceList>> GetList()
    {
        return await Mediator.Send(new GetListAllowanceQuery());
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateAllowanceCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateAllowanceCommand command)
    {
        return await Mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, DeleteAllowanceCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }
}
