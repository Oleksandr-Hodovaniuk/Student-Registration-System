using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
{
    public UpdateUserDTOValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.Name)
            .Matches("^[A-Za-z]+$").WithMessage("Name must contain only Latin letters.")
            .Length(1, 60)
            .When(x => !string.IsNullOrWhiteSpace(x.Name));

        RuleFor(x => x.LastName)
            .Matches("^[A-Za-z]+$").WithMessage("Last name must contain only Latin letters.")
            .Length(1, 60)
            .When(x => !string.IsNullOrWhiteSpace(x.LastName));

        RuleFor(x => x.Age)
            .GreaterThanOrEqualTo(18).WithMessage("Age must be at least 18.")
            .When(x => x.Age != default);

        RuleFor(x => x.Email)
            .Matches(@"^[a-zA-Z0-9._%+-]+@gmail\.com$").WithMessage("Email must be a valid Gmail address.")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));
    }
}
