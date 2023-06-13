using hrOT.Application.BankAccounts.Queries;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using hrOT.Application.BankAccounts.Commands;
using hrOT.Application.BankAccounts.Commands.Create;
using hrOT.Application.BankAccounts.Commands.Update;
using hrOT.Application.BankAccounts.Commands.Delete;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using hrOT.Application.BankAccounts;

namespace WebUI.Controllers.BankAccounts;
[Route("api/[controller]")]
[ApiController]
public class BankAccountController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public BankAccountController(IMediator mediator)
    {
        _mediator = mediator;
    }
    /*[HttpGet("GetListBankAccount")]
    public async Task<IActionResult> GetListBankAccount()
    {
        var result = await Mediator
            .Send(new GetAllBankAccountQuery());

        return result.Count > 0
            ? Ok(result)
            : BadRequest("Danh sách trống!");
    }*/
    [HttpGet]
    public async Task<ActionResult<List<BankAccountDTO>>> GetAll()
    {
        return await _mediator.Send(new GetAllBankAccountQuery());
    }
    /*[HttpPost("AddBankAccount")]
    //[Authorize(Policy = "manager")]
    public async Task<Guid> AddBankAccount([FromForm] BankAccountCommandDTO bankAccount, Guid BankId)
    {
        if (BankId == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập BankId!");
        }

        var result = await Mediator
           .Send(new CreateBankAccountCommand(BankId, bankAccount));

        return Ok(result);
    }*/
    [HttpPost]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<Guid>> Create(CreateBankAccountCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await _mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return BadRequest("Thêm thất bại");
    }
    /*[HttpPut("UpdateBankAccount")]
    //[Authorize(Policy = "manager")]
    public async Task<IActionResult> UpdateBankAccount(Guid BankId, [FromForm] BankAccountCommandDTO bankAccount)
    {
        if (BankId == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập BankId!");
        }

        var result = await Mediator
            .Send(new UpdateBankAccountCommand(BankId, bankAccount));
        return Ok(result);
    }*/
    [HttpPut("{id}")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult> UpdateStatus([FromForm] UpdateBankAccountCommand command, Guid id)
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
    /*[HttpDelete("DeleteBankAccount")]
    //[Authorize(Policy = "manager")]
    public async Task<IActionResult> DeleteBankAccount(Guid BankId)
    {
        if (BankId == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập BankId!");
        }

        var result = await Mediator
            .Send(new DeleteBankAccountCommand(BankId));

        return Ok(result);
    }*/
    [HttpDelete("{id}")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await Mediator.Send(new DeleteBankAccountCommand(id));
            return Ok("Xóa thành công");
        }
        catch (Exception ex)
        {
            return BadRequest("Xóa thất bại");
        }
    }
}
