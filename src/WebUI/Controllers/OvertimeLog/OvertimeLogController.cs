using hrOT.Application.OvertimeLogs.Queries;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using hrOT.Application.OvertimeLogs.Commands.Update;
using hrOT.Application.OvertimeLogs.Commands.Delete;
using hrOT.Application.OvertimeLogs.Commands.Create;

namespace WebUI.Controllers.OvertimeLog;
public class OvertimeLogController : ApiControllerBase
{
/*    public IActionResult Index()
    {
        return View();
    }*/

    [HttpGet]
    public async Task<ActionResult<OvertimeLogList>> GetList()
    {
        return await Mediator.Send(new Staff_GetListOvertimeLogQuery());
    }
    [HttpPut("Staff/{id}")]
    public async Task<ActionResult> UpdateStatus(Guid id, Staff_UpdateOvertimeLogCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }
    [HttpPut("Employee/{id}")]
    public async Task<ActionResult> Update(Guid id, Employee_UpdateOvertimeLogCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(Employee_CreateOvertimeLogCommand command)
    {
        return await Mediator.Send(command);
    }
     [HttpDelete("{id}")]
     public async Task<ActionResult> Delete(Guid id, DeleteOvertimeLogCommand command)
     {
         await Mediator.Send(command);

         return NoContent();
     }

}
