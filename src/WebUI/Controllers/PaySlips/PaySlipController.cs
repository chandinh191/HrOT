using hrOT.Application.PaySlips.Commands.CreatePaySlip;
using hrOT.Application.PaySlips.Commands.Queries;
using hrOT.Application.PaySlips.Commands;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net;

namespace WebUI.Controllers.PaySlips;
public class PaySlipController : ApiControllerBase
{

    private readonly IMediator _mediator;

    public PaySlipController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
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

}
