﻿using FluentValidation;
using EventApp.Application.DTOs.Location.Request;
using EventApp.Application.DTOs.Location.Response;

namespace EventApp.Application.Common.Validation.Validators.Location
{
    public class LocationResponseValidator : AbstractValidator<LocationResponse>
    {
        public LocationResponseValidator()
        {
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .Length(5, 200).WithMessage("Address must be between 5 and 200 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .Length(2, 100).WithMessage("City must be between 2 and 100 characters.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("State is required.")
                .Length(2, 50).WithMessage("State must be between 2 and 50 characters.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.")
                .Length(2, 100).WithMessage("Country must be between 2 and 100 characters.");
        }
    }
}
