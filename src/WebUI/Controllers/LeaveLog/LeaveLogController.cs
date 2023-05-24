using hrOT.Application.LeaveLogs.Queries;

using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using hrOT.Application.LeaveLogs.Commands.Update;
using hrOT.Application.OvertimeLogs.Commands.Create;

namespace WebUI.Controllers.LeaveLog;
public class LeaveLogController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<LeaveLogList>> GetList()
    {
        return await Mediator.Send(new Staff_GetListLeaveLogQuery());
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateStatus(Guid id, Staff_UpdateLeaveLogCommand command)
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
}
