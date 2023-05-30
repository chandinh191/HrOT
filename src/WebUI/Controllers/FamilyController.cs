using hrOT.Application.Families.Commands.Create;
using hrOT.Application.Families.Commands.Delete;
using hrOT.Application.Families.Commands.Update;
using hrOT.Application.Families.Queries;
using hrOT.Application.Families;
using hrOT.WebUI.Controllers;
using MediatR;
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
    //[Authorize(Policy = "manager")]
    public async Task<ActionResult<List<FamilyDto>>> GetAll()
    {
        return await _mediator.Send(new GetAllFamilyQuery());
    }
    [HttpGet("{EmployeeId}")]
    public async Task<ActionResult<List<FamilyDto>>> GetListByEmployeeId(Guid EmployeeId)
    {
        return await _mediator.Send(new GetListFamilyByEmployeeIdQuery(EmployeeId));
    }
    [HttpPost]
    //[Authorize(Policy = "employee")]
    public async Task<ActionResult<Guid>> Create(CreateFamilyCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await _mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return BadRequest("Thêm thất bại");
    }
    [HttpPut("{id}")]
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

    [HttpDelete("{id}")]
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
