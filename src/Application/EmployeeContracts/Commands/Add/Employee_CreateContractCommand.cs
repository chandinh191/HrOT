using System.Threading;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Employees;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace hrOT.Application.EmployeeContracts.Commands.Add;

public class Employee_CreateContractCommand : IRequest<bool>
{
    public Guid EmployeeId { get; set; }
    public EmployeeContractCommandDTO EmployeeContractDTO { get; set; }

    public Employee_CreateContractCommand(Guid employeeId, EmployeeContractCommandDTO employeeContractDTO)
    {
        EmployeeId = employeeId;
        EmployeeContractDTO = employeeContractDTO;
    }
}

public class Employee_CreateContractCommandHandler : IRequestHandler<Employee_CreateContractCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public Employee_CreateContractCommandHandler(IApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<bool> Handle(Employee_CreateContractCommand request, CancellationToken cancellationToken)
    {
        var employee =  _context.Employees
            .Where(e => e.Id == request.EmployeeId)
            .FirstOrDefault();

        if (employee != null)
        {
            var contract = new EmployeeContract
            {
                Id = new Guid(),
                EmployeeId = request.EmployeeId,
                File = UploadFile(request),
                StartDate = request.EmployeeContractDTO.StartDate,
                EndDate = request.EmployeeContractDTO.EndDate,
                Job = request.EmployeeContractDTO.Job,
                Salary = request.EmployeeContractDTO.Salary,
                Number_Of_Dependents = request.EmployeeContractDTO.Number_Of_Dependents,
                InsuranceType = request.EmployeeContractDTO.InsuranceType,
                CustomSalary = request.EmployeeContractDTO.CustomSalary,
                Status = EmployeeContractStatus.Effective,
                SalaryType = request.EmployeeContractDTO.SalaryType,
                ContractType = request.EmployeeContractDTO.ContractType,
            };
            await _context.EmployeeContracts.AddAsync(contract);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        return false;
    }

    private String UploadFile(Employee_CreateContractCommand request)
    {
        var file = request.EmployeeContractDTO.File;
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