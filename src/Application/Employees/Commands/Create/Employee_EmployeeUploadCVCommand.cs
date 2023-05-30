using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.Ocsp;

namespace hrOT.Application.Employees.Commands.Create;
public class Employee_EmployeeUploadCVCommand : IRequest
{
    //public Guid Id { get; set; }
    public IFormFile CVFile { get; set; }
    
}

public class Employee_EmployeeUploadCVHandler : IRequestHandler<Employee_EmployeeUploadCVCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Employee_EmployeeUploadCVHandler(IApplicationDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(Employee_EmployeeUploadCVCommand request, CancellationToken cancellationToken)
    {
        var employeeIdCookie = _httpContextAccessor.HttpContext.Request.Cookies["EmployeeId"];
        var employeeId = Guid.Parse(employeeIdCookie);
        var employee = await _context.Employees.FindAsync(employeeId);

        if (employee == null || employee.IsDeleted)
        {
            throw new NotFoundException("Employee not found");
        }

        var file = request.CVFile;
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
                await file.CopyToAsync(stream);
            }

            // Update the CVPath property of the employee
            employee.CVPath = filePath;

            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}
