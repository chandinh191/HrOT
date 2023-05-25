using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace LogOT.Application.Employees.Commands.Create;

public record CreateEmployeeEx : IRequest
{
    public string FilePath { get; set; }
}

public class CreateEmployeeExHandler : IRequestHandler<CreateEmployeeEx>
{
    private readonly IIdentityService _identityService;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IApplicationDbContext _context;


    public CreateEmployeeExHandler(IApplicationDbContext context, IIdentityService identityService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
    {
        _context = context;
        _identityService = identityService;
        this.userManager = userManager;
        this.roleManager = roleManager;
        _signInManager = signInManager;
    }

    public async Task<Unit> Handle(CreateEmployeeEx request, CancellationToken cancellationToken)
    {
        var filePath = request.FilePath;

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("The specified file does not exist.", filePath);
        }

        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            var worksheet = package.Workbook.Worksheets[0];
            var rowCount = worksheet.Dimension.Rows;

            var employees = new List<Employee>();

            for (int row = 2; row <= rowCount; row++)
            {
                string dateString = worksheet.Cells[row, 7].Value?.ToString();
                DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime birthDay);

                var user = new ApplicationUser
                {
                    UserName = worksheet.Cells[row, 1].Value?.ToString(),

                    Address = worksheet.Cells[row, 2].Value?.ToString(),
                    Image = worksheet.Cells[row, 3].Value?.ToString(),
                    Email = worksheet.Cells[row, 4].Value?.ToString(),
                    Fullname = worksheet.Cells[row, 5].Value?.ToString(),
                    PhoneNumber = worksheet.Cells[row, 6].Value?.ToString(),

                    BirthDay = birthDay,
                };
                var result = await userManager.CreateAsync(user, worksheet.Cells[row, 8].Value?.ToString());

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, worksheet.Cells[row, 9].Value?.ToString());
                    await _signInManager.SignInAsync(user, isPersistent: false);

                }
                else
                {

                }
                var employee = new Employee
                {
                    ApplicationUserId = user.Id,
                    // Đọc dữ liệu từ các ô trong tệp Excel và gán vào các thuộc tính của đối tượng Employee
                    IdentityImage = worksheet.Cells[row, 8].Value?.ToString(),
                    Diploma = worksheet.Cells[row, 9].Value?.ToString(),
                    BankName = worksheet.Cells[row, 10].Value?.ToString(),
                    BankAccountNumber = worksheet.Cells[row, 11].Value?.ToString(),
                    BankAccountName = worksheet.Cells[row, 12].Value?.ToString(),
                    // Các thuộc tính khác
                };

                employees.Add(employee);
            }

            // Thêm danh sách nhân viên vào DbContext
            await _context.Employees.AddRangeAsync(employees);
            await _context.SaveChangesAsync(cancellationToken);

            // Tạo tài khoản cho nhân viên và gán vào vai trò tương ứng
            foreach (var employee in employees)
            {
                var user = new ApplicationUser { UserName = employee.ApplicationUser.UserName, Email = $"{employee.ApplicationUser.UserName}@example.com" };
                await userManager.CreateAsync(user);

                // Gán vai trò cho tài khoản nhân viên
                await userManager.AddToRoleAsync(user, "Employee");

                employee.ApplicationUserId = user.Id;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
        

        return Unit.Value;
    }
}

