using hrOT.Application.Families;
using hrOT.Application.Families.Commands.Create;
using hrOT.Application.Families.Commands.Delete;
using hrOT.Application.Families.Commands.Update;
using hrOT.Application.Families.Queries;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

public class FamilyController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public FamilyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<List<FamilyDto>>> GetAll()
    {
        return await _mediator.Send(new GetAllFamilyQuery());
    }

    [HttpGet("GetFamilyByEmployeeId")]
    public async Task<ActionResult<List<FamilyDto>>> GetListByEmployeeId(Guid EmployeeId)
    {
        return await _mediator.Send(new GetListFamilyByEmployeeIdQuery(EmployeeId));
    }

    [HttpPost("Create")]
    [Authorize(Policy = "ManagerOrStaff")]
    public async Task<ActionResult<String>> Create(CreateFamilyCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            var entityId = await _mediator.Send(command);
            return entityId;
        }
        return BadRequest("Thêm thất bại");
    }

    [HttpPut("Update")]
    public async Task<ActionResult> UpdateStatus([FromForm] UpdateFamilyCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok("Cập nhật thành công");
        }
        catch (Exception ex)
        {
            return BadRequest("Cập nhật thất bại");
        }
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> Delete([FromForm] DeleteFamilyCommand command)
    {
        
        try
        {
            await _mediator.Send(command);
            return Ok("Xóa thành công");
        }
        catch (Exception ex)
        {
            return BadRequest("Xóa thất bại");
        }
    }
}