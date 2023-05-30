using hrOT.Application.PaySlips.Commands.CreatePaySlip;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using hrOT.Application.PaySlips.Queries;
using hrOT.Application.PaySlips;

namespace WebUI.Controllers.PaySlips;
public class PaySlipController : ApiControllerBase
{

    private readonly IMediator _mediator;

    public PaySlipController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<double?>> GetTotalSalary(DateTime FromDate, DateTime ToDate)
    {
        return await Mediator.Send(new GetTotalSalaryPayForEmployeeQuery(FromDate, ToDate));
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
            await Mediator.Send(command);
            return Ok("Tạo thành công");
        }
        return BadRequest("Tạo thất bại");
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



}
