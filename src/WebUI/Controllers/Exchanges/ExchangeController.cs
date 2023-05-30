using hrOT.Application.Exchanges.Commands.CreateExchange;
using hrOT.Application.Exchanges.Commands.DeleteExchange;
using hrOT.Application.Exchanges.Commands.UpdateExchange;
using hrOT.Application.Exchanges.Queries;
using hrOT.Domain.Entities;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebUI.Controllers.Exchanges;
[Authorize(Policy = "Manager")]
public class ExchangeController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Exchange>>> Get()
    {
        return await Mediator.Send(new GetListExchangeQuery());
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateExchangeCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await Mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return BadRequest("Thêm thất bại");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateExchangeCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Lỗi! Không tìm thấy Id");
        }
        try
        {
            var result = await Mediator.Send(command);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest("Cập nhật thất bại");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await Mediator.Send(new DeleteExchangeCommand(id));
            return Ok("Xóa thành công");
        }
        catch (Exception ex)
        {
            return BadRequest("Xóa thất bại");
        }
    }
}
