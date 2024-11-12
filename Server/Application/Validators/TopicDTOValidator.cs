using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class TopicDTOValidator : AbstractValidator<TopicDTO>
{
    public TopicDTOValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Topic name is required.")
            .Length(1, 25).WithMessage("Topic name must be between 1 and 25 characters.");
    }
}
