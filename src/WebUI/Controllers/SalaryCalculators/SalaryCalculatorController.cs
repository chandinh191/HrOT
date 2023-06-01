using hrOT.Application.Common.Exceptions;
using hrOT.Application.SalaryCalculators;
using hrOT.Application.SalaryCalculators.Commands;
using hrOT.Application.PaySlips.Queries;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.SalaryCalculators;
[Authorize(Policy = "manager")]
public class SalaryCalculatorController : ApiControllerBase
{

    private readonly IMediator _mediator;

    public SalaryCalculatorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<SalaryCalculatorDto>> Calculator(CreateSalaryCalculatorCommand command)
    {
        return await Mediator.Send(command);
    }

    



}
