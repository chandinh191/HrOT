using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Enums;
using MediatR;
using iTextSharp.text;
using iTextSharp.text.pdf;

using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Headers;

namespace hrOT.Application.PaySlips.Commands.Queries;
public class GetPaySlipByIdQuery : IRequest<HttpResponseMessage>
{
    public Guid Id { get; set; }
}
public class GetPaySlipByIdQueryHandler : IRequestHandler<GetPaySlipByIdQuery, HttpResponseMessage>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPaySlipByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<HttpResponseMessage> Handle(GetPaySlipByIdQuery request, CancellationToken cancellationToken)
    {
        var paySlip = await _context.PaySlips
            .Where(x => x.Id == request.Id)
            .ProjectTo<PaySlipDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (paySlip == null)
        {
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        // Generate PDF using iTextSharp
        byte[] pdfBytes = GeneratePdfFromPaySlip(paySlip);

        // Create a response with the PDF content
        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
        response.Content = new ByteArrayContent(pdfBytes);
        response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        {
            FileName = "paySlip.pdf"
        };
        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

        return response;
    }

    private byte[] GeneratePdfFromPaySlip(PaySlipDto paySlipDto)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, ms);
            document.Open();

            // Add content to the PDF document based on the paySlipDto data
            document.Add(new Paragraph($"Standard Work Hours: {paySlipDto.Standard_Work_Hours}"));
            document.Add(new Paragraph($"Actual Work Hours: {paySlipDto.Actual_Work_Hours}"));
            document.Add(new Paragraph($"Ot Hours: {paySlipDto.Ot_Hours}"));
            document.Add(new Paragraph($"Leave Hours: {paySlipDto.Leave_Hours}"));
            document.Add(new Paragraph($"Salary: {paySlipDto.Salary}"));
            document.Add(new Paragraph($"BHXH_Emp: {paySlipDto.BHXH_Emp}"));
            document.Add(new Paragraph($"BHYT_Emp: {paySlipDto.BHYT_Emp}"));
            document.Add(new Paragraph($"BHTN_Emp: {paySlipDto.BHTN_Emp}"));
            document.Add(new Paragraph($"BHXH_Comp: {paySlipDto.BHXH_Comp}"));
            document.Add(new Paragraph($"BHYT_Comp: {paySlipDto.BHYT_Comp}"));
            document.Add(new Paragraph($"BHTN_Comp: {paySlipDto.BHTN_Comp}"));
            document.Add(new Paragraph($"Tax In Come: {paySlipDto.Tax_In_Come}"));
            document.Add(new Paragraph($"Bonus: {paySlipDto.Bonus}"));
            document.Add(new Paragraph($"Deduction: {paySlipDto.Deduction}"));
            document.Add(new Paragraph($"Final Salary: {paySlipDto.Final_Salary}"));
            document.Add(new Paragraph($"Paid date: {paySlipDto.Paid_date}"));
            document.Add(new Paragraph($"Detail Tax Incomes: {paySlipDto.DetailTaxIncomes}"));
            //paySlipDto.ToString();

            // Add more elements and formatting as needed

            document.Close();

            return ms.ToArray();
        }
    }
}
