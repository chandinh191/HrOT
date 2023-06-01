
using hrOT.Application.Departments.Commands.DeleteDepartment;
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
    public async Task<ActionResult> Create(CreatePositionCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        return BadRequest("Thêm thất bại");
    }


    [HttpPut]
    public async Task<ActionResult> Update(UpdatePositionCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        return BadRequest("Cập nhật thất bại");
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await Mediator.Send(new DeletePositionCommand(id));
            return Ok("Xóa thành công");
        }
        catch (Exception ex)
        {
            return BadRequest("Xóa thất bại");
        }

    }
}


