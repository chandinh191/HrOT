using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using hrOT.Application.Departments.Queries.GetDepartment;
using hrOT.Application.Departments.Commands.CreateDepartment;
using hrOT.Application.Departments.Commands.UpdateDepartment;

using hrOT.Domain.Entities;
using hrOT.Application.Departments.Commands.DeleteDepartment;
using hrOT.Application.Departments;

namespace WebUI.Controllers.Departments;
[Authorize]
public class DepartmentController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<DepartmentDTO>>> Get()
    {
        return await Mediator.Send(new GetListDepartmentQuery());
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateDepartmentCommand command)
    {
        return await Mediator.Send(command);
    }
    

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateDepartmentCommand command)
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
        await Mediator.Send(new DeteleDepartmentCommand(id));

        return NoContent();

    }
}
