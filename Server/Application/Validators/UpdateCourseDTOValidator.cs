﻿using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateCourseDTOValidator : AbstractValidator<UpdateCourseDTO>
    {
        public UpdateCourseDTOValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");

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
                .GreaterThan(DateTime.Now.AddDays(1).Date)
                .WithMessage("Beginning date must be at least one day in the future.");

            RuleFor(x => x.Duration)
                .GreaterThanOrEqualTo((short)1)
                .LessThanOrEqualTo((short)1000)
                .WithMessage("Duration must be at least 1 day and less than 1000.");
        }
    }
}
