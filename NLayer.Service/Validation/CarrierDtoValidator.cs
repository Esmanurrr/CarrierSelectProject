using FluentValidation;
using Microsoft.Identity.Client;
using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Validation
{
    public class CarrierDtoValidator : AbstractValidator<CarrierDto>
    {
        public CarrierDtoValidator()
        {
            RuleFor(x => x.CarrierName).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.CarrierPlusDesiCost).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} is must be greater than 0");
            RuleFor(x => x.CarrierIsActive).NotNull().WithMessage("{PropertyName} must be true or false");
        }
    }
}
