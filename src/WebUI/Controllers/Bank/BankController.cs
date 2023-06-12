using hrOT.Application.Banks.Queries;
using hrOT.Application.Banks;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using hrOT.Application.Banks.Commands.Create;
using hrOT.Application.Banks.Commands.Update;
using hrOT.Application.Banks.Commands.Delete;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers.Bank;
[Route("api/[controller]")]
[ApiController]
public class BankController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public BankController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    //[Authorize(Policy = "manager")]
    public async Task<ActionResult<List<BankDTO>>> GetAll()
    {
        return await _mediator.Send(new GetAllBankQuery());
    }
    [HttpPost]
    //[Authorize(Policy = "manager")]
    public async Task<ActionResult<Guid>> Create(CreateBankCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await _mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return BadRequest("Thêm thất bại");
    }
    [HttpPut("{id}")]
    //[Authorize(Policy = "manager")]
    public async Task<ActionResult> UpdateStatus([FromForm] UpdateBankCommand command, Guid id)
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
    //[Authorize(Policy = "manager")]
    public async Task<ActionResult> Delete([FromForm] Guid id, DeleteBankCommand command)
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
