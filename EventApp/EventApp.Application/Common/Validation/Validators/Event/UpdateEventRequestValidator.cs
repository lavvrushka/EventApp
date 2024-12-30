using FluentValidation;
using EventApp.Application.DTOs.Event.Requests;
using EventApp.Application.Common.Validation.Validators.Location;

namespace EventApp.Application.Common.Validation.Validators.Event
{
    public class UpdateEventRequestValidator : AbstractValidator<UpdateEventRequest>
    {
        public UpdateEventRequestValidator()
        {
            RuleFor(e => e.Title)
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(e => e.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(e => e.DateTime)
                .GreaterThan(DateTime.Now).WithMessage("Event date must be in the future.")
                .When(e => e.DateTime != null);

            RuleFor(e => e.Location)
                .SetValidator(new LocationRequestValidator())
                .When(e => e.Location != null);
            RuleFor(e => e.Category)
               .NotEmpty().WithMessage("Category is required.");
        }
    }
}
