using hrOT.Application.Common.Exceptions;
using hrOT.Application.SalaryCalculators;
using hrOT.Application.SalaryCalculators.Commands;
using hrOT.Application.SalaryCalculators.Queries;
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

    [HttpGet("TotalSalaryOfCompany")]
    public async Task<ActionResult<double>> GetTotalSalaryOfCompany()
    {
        var query = new GetTotalSalaryOfCompanyQueries();
        var totalSalary = await _mediator.Send(query);

        return totalSalary;
    }

    [HttpGet("TotalSalaryOfDepartment")]
    public async Task<ActionResult<double>> GetTotalSalaryOfDepartment(Guid id)
    {
        try
        {
            var query = new GetTotalSalaryOfDepartmentQuery(id);
            var totalSalary = await _mediator.Send(query);

            return totalSalary;
        }
        catch (NotFoundException ex)
        {
            // Handle the NotFoundException
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            return StatusCode(500, "An error occurred.");
        }
    }



}
