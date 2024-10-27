using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class CourseDTOValidator : AbstractValidator<CourseDTO>
{
    public CourseDTOValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(1, 60).WithMessage("Name must be between 1 and 60 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(50, 3000).WithMessage("Description must be between 50 and 3000 characters.");

        RuleFor(x => x.Author)
           .NotEmpty().WithMessage("Author is required.")
           .Length(1, 60).WithMessage("Author must be between 1 and 60 characters.");

        RuleFor(x => x.Beginning)
            .NotEmpty().WithMessage("Beginning date is required.")
            .GreaterThan(DateTime.Now.Date.AddDays(1))
            .WithMessage("Beginning date must be at least one day in the future.");

        RuleFor(x => x.Duration)
            .GreaterThanOrEqualTo((short) 1)
            .WithMessage("Duration must be at least 1 day");
    }
}
