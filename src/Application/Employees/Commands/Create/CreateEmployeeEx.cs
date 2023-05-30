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

public record CreateEmployeeEx : IRequest<string>
{
    public string FilePath { get; set; }
}

public class CreateEmployeeExHandler : IRequestHandler<CreateEmployeeEx, string>
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

    public async Task<string> Handle(CreateEmployeeEx request, CancellationToken cancellationToken)
    {
        var filePath = request.FilePath;
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

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
                var birthDayString = worksheet.Cells[row, 3].Value?.ToString();
                if (DateTime.TryParseExact(birthDayString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime birthDay))
                {
                    var userName = worksheet.Cells[row, 12].Value?.ToString();

                    // Kiểm tra xem nhân viên đã tồn tại hay chưa
                    var isEmployeeExist = await _context.Employees.AnyAsync(e => e.ApplicationUser.UserName == userName);

                    if (!isEmployeeExist)
                    {
                        // Thêm nhân viên vào danh sách employees
                        var user = new ApplicationUser
                        {
                            UserName = userName,
                            
                            Image = worksheet.Cells[row, 8].Value?.ToString(),
                            Email = worksheet.Cells[row, 5].Value?.ToString(),
                            Fullname = worksheet.Cells[row, 1].Value?.ToString(),
                            PhoneNumber = worksheet.Cells[row, 4].Value?.ToString(),
                            BirthDay = birthDay,
                        };

                        var result = await userManager.CreateAsync(user, worksheet.Cells[row, 13].Value?.ToString());

                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, worksheet.Cells[row, 14].Value?.ToString());
                            await _signInManager.SignInAsync(user, isPersistent: false);

                            var employee = new Employee
                            {
                                PositionId= Guid.Parse(worksheet.Cells[row, 18].Value?.ToString()),
                                Province = worksheet.Cells[row, 16].Value?.ToString(),
                                District = worksheet.Cells[row, 17].Value?.ToString(),
                                CitizenIdentificationNumber = worksheet.Cells[row, 15].Value?.ToString(),
                                ApplicationUserId = user.Id,
                                Address = worksheet.Cells[row, 2].Value?.ToString(),
                                CreatedDateCIN = DateTime.Parse(worksheet.Cells[row, 7].Value?.ToString()),
                                PlaceForCIN = worksheet.Cells[row, 6].Value?.ToString(),
                                BankName = worksheet.Cells[row, 9].Value?.ToString(),
                                BankAccountNumber = worksheet.Cells[row, 10].Value?.ToString(),
                                BankAccountName = worksheet.Cells[row, 11].Value?.ToString(),
                            };

                            employees.Add(employee);
                        }
                        else
                        {
                            // Xử lý khi không thể tạo người dùng mới
                        }
                    }
                }
                else
                {
                    return "Lỗi thêm nhân viên";
                }
            }

            // Thêm danh sách nhân viên vào DbContext
            await _context.Employees.AddRangeAsync(employees);
            await _context.SaveChangesAsync(cancellationToken);

        }

        return "Thêm thành công";
    }

}

