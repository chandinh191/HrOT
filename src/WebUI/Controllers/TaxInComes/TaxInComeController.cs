﻿using hrOT.Application.Exchanges.Commands.DeleteExchange;
using hrOT.Application.TaxInComes.Commands.CreateTaxInCome;
using hrOT.Application.TaxInComes.Commands.DeleteTaxInCome;
using hrOT.Application.TaxInComes.Commands.UpdateTaxInCome;
using hrOT.Application.TaxInComes.Queries;
using hrOT.Domain.Entities;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.TaxInComes;
public class TaxInComeController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TaxInCome>>> Get()
    {
        return await Mediator.Send(new GetListTaxInComeQuery());
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateTaxInComeCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await Mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return BadRequest("Thêm thất bại");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateTaxInComeCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Lỗi! Không tìm thấy Id");
        }
        try
        {
            await Mediator.Send(command);
            return Ok("Cập nhật thành công");

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
            await Mediator.Send(new DeleteTaxInComeCommand(id));
            return Ok("Xóa thành công");
        }
        catch (Exception ex)
        {
            return BadRequest("Xóa thất bại");
        }
    }
}
