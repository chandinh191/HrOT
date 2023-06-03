﻿using FluentValidation;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Employees.Commands.Create;
using hrOT.Application.Employees.Commands.Update;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

public class UpdateEmployeeRoleEmployeeValidator : AbstractValidator<UpdateEmployeeRoleEmployee>
{
    private readonly IApplicationDbContext _context;

    public UpdateEmployeeRoleEmployeeValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(e => e.Fullname)

            .NotEmpty().WithMessage("Tên không được để trống.")
            .MaximumLength(50).WithMessage("Tên không quá 50 ký tự.")
            .Must(BeValidFullName).WithMessage("Định dạng tên đầy đủ không hợp lệ hoặc chứa các ký tự không hợp lệ.");

        RuleFor(e => e.Address)

            .NotEmpty().WithMessage("Địa chỉ không được để trống.")
            .MaximumLength(50).WithMessage("Address must not exceed 50 characters.");



        RuleFor(e => e.PhoneNumber)

            .NotEmpty().WithMessage("Số điện thoại không được để trống.")
            .Matches(@"^(03|05|07|08|09)\d{8}$").WithMessage("Invalid phone number format.")
            .MaximumLength(10).WithMessage("Phone must not exceed 10 characters.");


        RuleFor(e => e.BirthDay)
     .NotEmpty().WithMessage("Ngày sinh không được để trống.")
     .Must(BeValidBirthDate).WithMessage("Ngày sinh không hợp lệ.")
     .Must(BeAtLeast18YearsOld).WithMessage("Nhập ngày sinh nhân viên chưa đủ 18 tuổi không thể tạo được.");


        RuleFor(e => e.BankName)

           .NotEmpty().WithMessage("Tên ngân hàng không được để trống.")
           .MaximumLength(20).WithMessage("Bank Name must not exceed 20 characters.");


        RuleFor(e => e.BankAccountName)

          .NotEmpty().WithMessage("Tên tài khoản không được để trống")
          .MaximumLength(50).WithMessage("Bank Account Name must not exceed 50 characters.");


        RuleFor(e => e.BankAccountNumber)

          .NotEmpty().WithMessage("Số tài khoản không được để trống.")
          .MaximumLength(20).WithMessage("Bank Account Number must not exceed 20 characters.");
        RuleFor(e => e.District)
           .NotEmpty().WithMessage("Quận/Huyện không được để trống.")
           .MaximumLength(50).WithMessage("Quận/Huyện không quá 50 ký tự.");

        RuleFor(e => e.Province)
            .NotEmpty().WithMessage("Tỉnh/Thành phố không được để trống.")
            .MaximumLength(50).WithMessage("Tỉnh/Thành phố không quá 50 ký tự.");

        RuleFor(e => e.PositionId)
           .NotEmpty().WithMessage("PositionId không được để trống.");

        RuleFor(e => e.CitizenIdentificationNumber)
            .MaximumLength(20).WithMessage("CitizenIdentificationNumber không quá 20 ký tự.");

        RuleFor(e => e.CreatedDateCIN)
            .NotEmpty().WithMessage("CreatedDateCIN không được để trống.");

        RuleFor(e => e.PlaceForCIN)
            .MaximumLength(50).WithMessage("PlaceForCIN không quá 50 ký tự.");

        

       
    }

    private bool BeAtLeast18YearsOld(DateTime birthDate)
    {
        // Tính tuổi hiện tại của ngày sinh.
        int age = DateTime.Today.Year - birthDate.Year;

        // Kiểm tra nếu tuổi chưa đủ 18 tuổi
        return age >= 18;
    }
    private async Task<bool> ExistAsync(Guid employeeId, CancellationToken cancellationToken)
    {
        var employeeExists = await _context.Employees.AnyAsync(e => e.Id == employeeId, cancellationToken);
        return employeeExists;
    }

    public async Task<bool> BeUniqueUserName(string username, CancellationToken cancellationToken)
    {
        return await _context.Employees
            .AllAsync(e => e.ApplicationUser.UserName != username, cancellationToken);
    }

    public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _context.Employees
            .AllAsync(e => e.ApplicationUser.Email != email, cancellationToken);
    }

    public bool BeValidBirthDate(DateTime birthDate)
    {
        // Validate birth date logic here
        // Ensure the birth date is not in the future
        if (birthDate > DateTime.Today)
        {
            return false;
        }

        // Ensure the birth date is in the format of "month/day/year"
        // Use custom DateTime parsing with the desired format
        string birthDateStr = birthDate.ToString("M/d/yyyy");
        bool isValidFormat = DateTime.TryParseExact(birthDateStr, "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);

        return isValidFormat;
    }

    public bool BeValidFullName(string fullName)
    {
        // Validate full name logic here
        // The full name must be in Vietnamese or a different ethnic language of Vietnam,
        // and it should not contain numbers or single characters that are not letters.

        // Regular expression pattern to match Vietnamese or ethnic language characters
        string vietnamesePattern = @"^[\p{L}\s]+$";

        // Check if the full name matches the pattern and does not contain single characters that are not letters
        return Regex.IsMatch(fullName, vietnamesePattern) &&
            !Regex.IsMatch(fullName, @"\b\p{L}{1}\b");
    }


}
