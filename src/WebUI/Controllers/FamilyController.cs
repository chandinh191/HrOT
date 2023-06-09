using hrOT.Application.Families.Commands.Create;
using hrOT.Application.Families.Commands.Delete;
using hrOT.Application.Families.Commands.Update;
using hrOT.Application.Families.Queries;
using hrOT.Application.Families;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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
    [HttpGet("{EmployeeId}")]
    public async Task<ActionResult<List<FamilyDto>>> GetListByEmployeeId(Guid EmployeeId)
    {
        return await _mediator.Send(new GetListFamilyByEmployeeIdQuery(EmployeeId));
    }
    [HttpPost("Create")]
    [Authorize(Policy = "employee")]
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
    public async Task<ActionResult> UpdateStatus(Guid id, UpdateFamilyCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Lỗi! Không tìm thấy Id");
        }
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

    [HttpPut("{id}")]
    public async Task<ActionResult> Delete(Guid id, DeleteFamilyCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Lỗi! Không tìm thấy Id");
        }
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
