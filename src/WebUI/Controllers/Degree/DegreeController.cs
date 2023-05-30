
using MediatR;
using Microsoft.AspNetCore.Mvc;
using hrOT.Application.Degrees.Queries;
using hrOT.Application.Degrees;
using hrOT.Application.Degrees.Commands.Create;
using hrOT.Application.Degrees.Commands.Update;
using hrOT.Application.Degrees.Commands.Delete;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers.Degree;
[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "ManagerOrStaff")]
public class DegreeController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public DegreeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    //[Authorize(Policy = "manager")]
    public async Task<ActionResult<List<DegreeDto>>> GetAll()
    {
        return await _mediator.Send(new GetAllDegreeQuery());
    }
    [HttpGet("{EmployeeId}")]
    public async Task<ActionResult<List<DegreeDto>>> GetListByEmployeeId(Guid EmployeeId)
    {
        return await _mediator.Send(new GetListDegreeByEmployeeIdQuery(EmployeeId));
    }
    [HttpPost]
    //[Authorize(Policy = "employee")]
    public async Task<ActionResult<Guid>> Create(CreateDegreeCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await _mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return BadRequest("Thêm thất bại");
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateStatus(Guid id, UpdateDegreeCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Lỗi! Không tìm thấy Id");
        }
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest("Cập nhật thất bại");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, DeleteDegreeCommand command)
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
