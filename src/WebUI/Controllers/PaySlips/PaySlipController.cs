﻿using hrOT.Application.PaySlips.Commands.CreatePaySlip;
using hrOT.Application.PaySlips.Commands.Queries;
using hrOT.Application.PaySlips.Commands;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.PaySlips;
public class PaySlipController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PaySlipDto>>> Get(Guid EmployeeId)
    {
        return await Mediator.Send(new GetListPaySlipByEmployeeIdQuery(EmployeeId));
    }
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreatePaySlipCommand command)
    {
        return await Mediator.Send(command);
    }
}
