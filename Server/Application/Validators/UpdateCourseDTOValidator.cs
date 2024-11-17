using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateCourseDTOValidator : AbstractValidator<UpdateCourseDTO>
    {
        public UpdateCourseDTOValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        }
    }
}
