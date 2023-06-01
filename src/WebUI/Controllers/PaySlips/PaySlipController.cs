using hrOT.Application.PaySlips.Commands.CreatePaySlip;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using hrOT.Application.PaySlips.Queries;
using hrOT.Application.PaySlips;
using hrOT.Application.Common.Models;
using hrOT.Application.Common.Exceptions;

namespace WebUI.Controllers.PaySlips;
public class PaySlipController : ApiControllerBase
{

    private readonly IMediator _mediator;

    public PaySlipController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("TotalSalary")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<double?>> GetTotalSalary(DateTime FromDate, DateTime ToDate)
    {
        return await Mediator.Send(new GetTotalSalaryPayForEmployeeQuery(FromDate, ToDate));
    }
    [HttpGet("TotalCostOfInsurance")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<double?>> GetTotalCostOfInsurance(DateTime FromDate, DateTime ToDate)
    {
        return await Mediator.Send(new GetTotalCostOfInsurance(FromDate, ToDate));
    }
    [HttpGet("TotalTaxIncome")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<double?>> GetTotalTaxIncome(DateTime FromDate, DateTime ToDate)
    {
        return await Mediator.Send(new GetTotalTaxIncomeQuery(FromDate, ToDate));
    }
    [HttpGet("{EmployeeId}")]
    [Authorize(Policy = "ManagerOrStaff")]
    public async Task<ActionResult<List<PaySlipDto>>> Get(Guid EmployeeId)
    {
        return await Mediator.Send(new GetListPaySlipByEmployeeIdQuery(EmployeeId));
    }
    [HttpPost]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<Guid>> Create(CreatePaySlipCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        return BadRequest("Thêm thất bại");
    }
    [HttpPost("CreatePaySlipForAllEmployee")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<Guid>> CreatePaySlipForAllEmployee(CreateAllPaySlipCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        return BadRequest("Thêm thất bại");
    }

    /* [HttpGet]
     public async Task<ActionResult<HttpResponseMessage>> GetPaySlipById(Guid id)
     {
         var query = new GetPaySlipByIdQuery { Id = id };
         var response = await _mediator.Send(query);

         if (response == null)
         {
             return NotFound();
         }

         return response;
     }

     [HttpGet("{id}/DownloadPdf")]
     public async Task<IActionResult> DownloadPdf(Guid id)
     {
         var query = new GetPaySlipByIdQuery { Id = id };
         var response = await Mediator.Send(query);

         if (response.StatusCode == HttpStatusCode.NotFound)
         {
             return NotFound();
         }

         var pdfBytes = await response.Content.ReadAsByteArrayAsync();
         return File(pdfBytes, "application/pdf", "paySlip.pdf");
     }*/

    [HttpGet("GetPaySlipByDateRange")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<List<PaySlipDto>>> GetByDateRange(DateTime fromDate, DateTime toDate)
    {
        return await _mediator.Send(new GetListPaySlipByDate(fromDate, toDate));
    }


    [HttpGet("TotalSalaryOfCompany")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<double>> GetTotalSalaryOfCompany()
    {
        var query = new GetTotalSalaryOfCompanyQuery();
        var totalSalary = await _mediator.Send(query);

        return totalSalary;
    }

    [HttpGet("TotalSalaryOfDepartment")]
    [Authorize(Policy = "manager")]
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
