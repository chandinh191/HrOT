using hrOT.Application.LeaveLogs.Queries;

using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using hrOT.Application.LeaveLogs.Commands.Update;
using hrOT.Application.LeaveLogs.Commands.Create;
using hrOT.Application.LeaveLogs.Commands.Delete;

namespace WebUI.Controllers.LeaveLog;
public class LeaveLogController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<LeaveLogList>> GetList()
    {
        return await Mediator.Send(new Staff_GetListLeaveLogQuery());
    }
    [HttpPut("Staff/{id}")]
    public async Task<ActionResult> UpdateStatus(Guid id, Staff_UpdateLeaveLogCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }
    [HttpPut("Employee/{id}")]
    public async Task<ActionResult> Update(Guid id, Employee_UpdateLeaveLogCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(Employee_CreateLeaveLogCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, DeleteLeaveLogCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }
}
