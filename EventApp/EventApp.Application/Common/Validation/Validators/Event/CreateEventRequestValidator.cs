using EventApp.Application.Common.Validation.Validators.Location;

using EventApp.Application.DTOs.Event.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventApp.Application.Common.Validation.Validators.Event
{
    public class CreateEventRequestValidator : AbstractValidator<AddEventRequest>
    {
        public CreateEventRequestValidator()
        {
            RuleFor(e => e.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(e => e.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(e => e.Location)
                .NotNull().WithMessage("Location is required.")
                .SetValidator(new LocationRequestValidator());

            RuleFor(e => e.Category)
                 .NotEmpty().WithMessage("Category is required.");
        }
    }
}
