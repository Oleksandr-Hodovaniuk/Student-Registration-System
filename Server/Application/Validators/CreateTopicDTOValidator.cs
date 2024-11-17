using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class CreateTopicDTOValidator : AbstractValidator<CreateTopicDTO>
{
    public CreateTopicDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Topic name is required.")
            .Matches(@"^[a-zA-Z0-9._%+#-]+$").WithMessage("Name can only contain letters, numbers, and the following characters: . _ % + - #.")
            .Length(1, 25).WithMessage("Topic name must be between 1 and 25 characters.");
    }
}
