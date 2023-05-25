using hrOT.Application.Departments.Commands.CreateDepartment;
using hrOT.Application.Departments.Commands.DeleteDepartment;
using hrOT.Application.Departments.Commands.UpdateDepartment;
using hrOT.Application.Departments.Queries.GetDepartment;
using hrOT.Application.Levels;
using hrOT.Application.Levels.Commands.CreateLevel;
using hrOT.Application.Levels.Commands.DeleteLevel;
using hrOT.Application.Levels.Commands.UpdateLevel;
using hrOT.Application.Levels.Queries.GetLevel;
using hrOT.Domain.Entities;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Levels;


[Authorize]
public class LevelController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<LevelDTO>>> Get()
    {
        return await Mediator.Send(new GetListLevelQuery());
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateLevelCommand command)
    {
        return await Mediator.Send(command);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateLevelCommand command)
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
        await Mediator.Send(new DeleteLevelCommand(id));

        return NoContent();

    }
}

