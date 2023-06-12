using hrOT.Application.BankAccounts.Queries;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using hrOT.Application.BankAccounts.Commands;
using hrOT.Application.BankAccounts.Commands.Create;
using hrOT.Application.BankAccounts.Commands.Update;
using hrOT.Application.BankAccounts.Commands.Delete;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers.BankAccounts;
[Route("api/[controller]")]
[ApiController]
public class BankAccountController : ApiControllerBase
{
    [HttpGet("GetListBankAccount")]
    public async Task<IActionResult> GetListBankAccount()
    {
        var result = await Mediator
            .Send(new GetAllBankAccountQuery());

        return result.Count > 0
            ? Ok(result)
            : BadRequest("Danh sách trống!");
    }
    [HttpPost("AddBankAccount")]
    [Authorize(Policy = "manager")]
    public async Task<IActionResult> AddBankAccount([FromForm] BankAccountCommandDTO bankAccount, Guid BankId)
    {
        if (BankId == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập BankId!");
        }

        var result = await Mediator
           .Send(new CreateBankAccountCommand(BankId, bankAccount));

        return Ok(result);
    }
    [HttpPut("UpdateBankAccount")]
    [Authorize(Policy = "manager")]
    public async Task<IActionResult> UpdateBankAccount(Guid BankId, [FromForm] BankAccountCommandDTO bankAccount)
    {
        if (BankId == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập BankId!");
        }

        var result = await Mediator
            .Send(new UpdateBankAccountCommand(BankId, bankAccount));
        return Ok(result);
    }
    [HttpDelete("DeleteBankAccount")]
    [Authorize(Policy = "manager")]
    public async Task<IActionResult> DeleteBankAccount(Guid BankId)
    {
        if (BankId == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập BankId!");
        }

        var result = await Mediator
            .Send(new DeleteBankAccountCommand(BankId));

        return Ok(result);
    }
}
