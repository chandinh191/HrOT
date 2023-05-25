using FluentValidation;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Employees.Commands.Create;
using Microsoft.EntityFrameworkCore;
using System;
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
            .NotEmpty().WithMessage("Full name is required.")
            .MaximumLength(100).WithMessage("Full name must not exceed 100 characters.")
            .Must(BeValidFullName).WithMessage("Invalid full name format or contains invalid characters.");

        RuleFor(e => e.UserName)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters.")
            .MustAsync(BeUniqueUserName).WithMessage("The specified username is already taken.");

        RuleFor(e => e.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.")
            .EmailAddress().WithMessage("Invalid email address.")
            .MustAsync(BeUniqueEmail).WithMessage("The specified email address is already registered.");

        RuleFor(e => e.BirthDay)
            .NotEmpty().WithMessage("Birth date is required.")
            .Must(BeValidBirthDate).WithMessage("Invalid birth date.");

        RuleFor(e => e.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must have at least 6 characters.");
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
        // For example, ensure the birth date is not in the future
        return birthDate <= DateTime.Today;
    }
    public bool BeValidFullName(string fullName)
    {
        // Validate full name logic here
        // The full name must be in Vietnamese or a different ethnic language of Vietnam,
        // and it should not contain numbers or single characters that are not letters.

        // Regular expression pattern to match Vietnamese or ethnic language characters
        string vietnamesePattern = "^[a-zA-ZÀ-ỹỳỷỵỹáàảạãÁÀẢẠÃéèẻẹẽÉÈẺẸẼíìỉịĩÍÌỈỊĨóòỏọõÓÒỎỌÕúùủụũÚÙỦỤŨýỳỷỵỹÝỲỶỴỸđĐ\\s]+$";

        // Check if the full name matches the pattern and does not contain single characters that are not letters
        return Regex.IsMatch(fullName, vietnamesePattern) &&
            !Regex.IsMatch(fullName, @"\b[a-zA-ZÀ-ỹỳỷỵỹáàảạãÁÀẢẠÃéèẻẹẽÉÈẺẸẼíìỉịĩÍÌỈỊĨóòỏọõÓÒỎỌÕúùủụũÚÙỦỤŨýỳỷỵỹÝỲỶỴỸđĐ\s]\b");
    }
}
