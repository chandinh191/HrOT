using FluentValidation;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Employees.Commands.Create;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

public class CreateEmployeeValidator : AbstractValidator<CreateEmployee>
{
    private readonly IApplicationDbContext _context;

    public CreateEmployeeValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(e => e.FullName)
            .NotEmpty().WithMessage("Tên không được để trống.")
            .MaximumLength(50).WithMessage("Tên không quá 50 ký tự.")
            .Must(BeValidFullName).WithMessage("Định dạng tên đầy đủ không hợp lệ hoặc chứa các ký tự không hợp lệ.");

        RuleFor(e => e.Address)
            .NotEmpty().WithMessage("Địa chỉ không được để trống.")
            .MaximumLength(50).WithMessage("Địa chỉ không được vượt quá 50 ký tự.");
        RuleFor(e => e.PhoneNumber)

            .NotEmpty().WithMessage("Số điện thoại không được để trống.")
            .Matches(@"^(03|05|07|08|09)\d{8}$").WithMessage("Định dạng số điện thoại không hợp lệ.")
            .MaximumLength(10).WithMessage("Điện thoại không được vượt quá 10 ký tự.");

        RuleFor(e => e.UserName)

            .NotEmpty().WithMessage("Tài khoản không được để trống")
            .MaximumLength(50).WithMessage("Tên người dùng không được vượt quá 50 ký tự.")
            .MustAsync(BeUniqueUserName).WithMessage("Tên người dùng được chỉ định đã được sử dụng.");

        RuleFor(e => e.Email)

            .NotEmpty().WithMessage("Email không được để trống.")
            .MaximumLength(100).WithMessage("Email không được vượt quá 100 ký tự.")
            .EmailAddress().WithMessage("Địa chỉ email không hợp lệ.")
            .MustAsync(BeUniqueEmail).WithMessage("Địa chỉ email được chỉ định đã được đăng ký.");

        RuleFor(e => e.BirthDay)
     .NotEmpty().WithMessage("Ngày sinh không được để trống.")
     .Must(BeValidBirthDate).WithMessage("Ngày sinh không hợp lệ.")
     .Must(BeAtLeast18YearsOld).WithMessage("Nhập ngày sinh nhân viên chưa đủ 18 tuổi không thể tạo được.");

        RuleFor(e => e.Password)
    .NotEmpty().WithMessage("Mật khẩu không được để trống.")
    .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 ký tự.")
    .Matches("[A-Z]").WithMessage("Mật khẩu phải chứa ít nhất một ký tự in hoa.")
    .Matches("[a-z]").WithMessage("Mật khẩu phải chứa ít nhất một ký tự thường.")
    .Matches("[0-9]").WithMessage("Mật khẩu phải chứa ít nhất một chữ số.")
    .Matches("[!@#$%^&*(),.?\":{}|<>]").WithMessage("Mật khẩu phải chứa ít nhất một ký tự đặc biệt.");


        RuleFor(e => e.BankName)

           .NotEmpty().WithMessage("Tên ngân hàng không được để trống.")
           .MaximumLength(20).WithMessage("Tên Ngân hàng không được vượt quá 20 ký tự.");


        RuleFor(e => e.BankAccountName)

          .NotEmpty().WithMessage("Tên tài khoản không được để trống")
          .MaximumLength(50).WithMessage("Tên tài khoản ngân hàng không được vượt quá 50 ký tự.");


        RuleFor(e => e.BankAccountNumber)

          .NotEmpty().WithMessage("Số tài khoản không được để trống.")
          .MaximumLength(20).WithMessage("Số Tài Khoản Ngân Hàng không được vượt quá 20 ký tự.");

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
        RuleFor(e => e.SelectedRole)
            .NotNull().WithMessage("Quyền không được để trống");

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
    private bool BeAtLeast18YearsOld(DateTime birthDate)
    {
        // Tính tuổi hiện tại của ngày sinh
        int age = DateTime.Today.Year - birthDate.Year;

        // Kiểm tra nếu tuổi chưa đủ 18 tuổi
        return age >= 18;
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
