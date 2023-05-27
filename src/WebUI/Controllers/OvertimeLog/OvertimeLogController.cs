using hrOT.Application.OvertimeLogs.Queries;
using hrOT.Application.TodoLists.Queries.GetTodos;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using hrOT.Application.OvertimeLogs.Commands.Update;
using hrOT.Application.TodoLists.Commands.CreateTodoList;
using hrOT.Application.OvertimeLogs.Commands.Create;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers.OvertimeLog;
[Authorize(Policy = "manager")]
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
    [HttpPut("{id}")]
   
    public async Task<ActionResult> Update(Guid id, Staff_UpdateOvertimeLogCommand command)
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
        if ( User.IsInRole("Staff"))
        {
            return await Mediator.Send(command);
        }
        return Unauthorized();
    }

}
