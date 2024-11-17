using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
{
	public CreateUserDTOValidator()
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

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Matches(@"^[a-zA-Z0-9._%+-]+$").WithMessage("The password can only contain letters, numbers, and the following characters: . _ % + -.")
            .Length(8,60).WithMessage("Password must be between 8 and 60 characters.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm Password is required.")
            .Equal(x => x.Password).WithMessage("Passwords do not match.");
    }
}
