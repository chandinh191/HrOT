using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.EmployeeContracts.Commands.Update;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace hrOT.Application.CompanyContracts.Commands.Create;
public record CreateCompanyContractCommand : IRequest<Guid>
{
    public IFormFile File { get; set; }
    public double? Salary { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}


public class CreateCompanyContractCommandHandler : IRequestHandler<CreateCompanyContractCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public CreateCompanyContractCommandHandler(IApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<Guid> Handle(CreateCompanyContractCommand request, CancellationToken cancellationToken)
    {
        var entity = new CompanyContract();
        entity.File = UploadFile(request);
        entity.Salary = request.Salary;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.CreatedBy = "Admin";
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Admin";

        _context.CompanyContracts.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

    private String UploadFile(CreateCompanyContractCommand request)
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
