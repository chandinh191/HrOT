using hrOT.Application.Common.Security;
using hrOT.Application.SalaryCalculators;
using hrOT.Application.SalaryCalculators.Commands;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.SalaryCalculators;
[Authorize(Policy = "manager")]
public class SalaryCalculatorController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<SalaryCalculatorDto>> Calculator(CreateSalaryCalculatorCommand command)
    {
        return await Mediator.Send(command);
    }
}
