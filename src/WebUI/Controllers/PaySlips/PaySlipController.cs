using hrOT.Application.PaySlips.Commands.CreatePaySlip;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using hrOT.Application.PaySlips;
using hrOT.Application.PaySlips.Queries;

namespace WebUI.Controllers.PaySlips;
public class PaySlipController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<double?>> GetTotalSalary(DateTime FromDate, DateTime ToDate)
    {
        return await Mediator.Send(new GetTotalSalaryPayForEmployeeQuery(FromDate, ToDate));
    }
    [HttpGet("{EmployeeId}")]
    public async Task<ActionResult<List<PaySlipDto>>> Get(Guid EmployeeId)
    {
        return await Mediator.Send(new GetListPaySlipByEmployeeIdQuery(EmployeeId));
    }
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreatePaySlipCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await Mediator.Send(command);
            return Ok("Tạo thành công");
        }
        return Ok("Tạo thất bại");
    }
}
