using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.CompanyContracts.Commands.Create;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace hrOT.Application.CompanyContracts.Commands.Update;
public record UpdateCompanyContractCommand : IRequest
{
    public Guid Id { get; set; }
    public IFormFile File { get; set; }
    public double? Salary { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}


public class UpdateCompanyContractCommandHandler : IRequestHandler<UpdateCompanyContractCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public UpdateCompanyContractCommandHandler(IApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<Unit> Handle(UpdateCompanyContractCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CompanyContracts
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(CompanyContract), request.Id);
        }

        entity.File = UploadFile(request);
        entity.Salary = request.Salary;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Admin";

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private String UploadFile(UpdateCompanyContractCommand request)
    {
        var file = request.File;
        if (file != null && file.Length > 0)
        {
            // Generate a unique file name
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // Specify the directory to save the CV files
            string uploadDirectory = _configuration.GetSection("UploadDirectory").Value;

            // Combine the directory and file name to get the full file path
            var filePath = Path.Combine(uploadDirectory, fileName);

            // Save the file to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }

            // Update the CVPath property of the employee
            return filePath;

        }
        return null;
    }
}
