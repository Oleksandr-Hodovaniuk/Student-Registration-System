using Application.DTOs;
using FluentValidation;
using System.Xml.Linq;

namespace Application.Validators;

public class UserDTOValidator : AbstractValidator<CreateUserDTO>
{
	public UserDTOValidator()
	{
        RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Name is required.")
			.Matches("^[A-Za-z]+$").WithMessage("Name must contain only Latin letters.")
            .Length(1, 60);

		RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .Matches("^[A-Za-z]+$").WithMessage("Last name must contain only Latin letters.")
            .Length(1, 60);

        RuleFor(x => x.Age)
            .NotEmpty().WithMessage("Age is required.")
            .GreaterThanOrEqualTo(18).WithMessage("Age must be at least 18.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .Matches(@"^[a-zA-Z0-9._%+-]+@gmail\.com$").WithMessage("Email must be a valid Gmail address.");
    }
}
