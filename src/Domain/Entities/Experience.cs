using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrOT.Domain.Entities;

public class StartDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        DateTime date = (DateTime)value;
        DateTime today = DateTime.Now;

        if (date < today)
        {
            return new ValidationResult("Start date must be as least from today!");
        }
        return ValidationResult.Success;
    }
}

public class EndDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var experience = value as Experience;
        var startDate = (DateTime)validationContext.ObjectType.GetProperty("StartDate").GetValue(validationContext.ObjectInstance, null);
        var endDate = (DateTime)value;

        if (endDate <= startDate)
        {
            return new ValidationResult("End date must be after start date!");
        }
        return ValidationResult.Success;
    }
}

public class Experience : BaseAuditableEntity
{
    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }

    [RegularExpression(@"^[A-Z][a-zA-Z0-9]+$", ErrorMessage = "Project name must be uppercase at first letter and not start with a number!")]
    [StringLength(100, ErrorMessage = "Project name must be less than 100 characters")]
    public string NameProject { get; set; }

    [Range(1, 10, ErrorMessage = "Team size must be greater than 0 and must lower than 10!")]
    public int TeamSize { get; set; }

    [StartDate(ErrorMessage = "Start date must be as least from today!")]
    public DateTime StartDate { get; set; }

    [EndDate(ErrorMessage = "End date must be after start date!")]
    public DateTime EndDate { get; set; }

    [RegularExpression(@"^.{10,255}$", ErrorMessage = "Description must be between 10 and 255 characters")]
    public string Description { get; set; }


    [StringLength(100, ErrorMessage = "Project name must be less than 100 characters")]
    public string TechStack { get; set; }

    public virtual Employee? Employee { get; set; }

    public bool Status { get; set; }
    //public bool IsDeleted { get; set; }
}