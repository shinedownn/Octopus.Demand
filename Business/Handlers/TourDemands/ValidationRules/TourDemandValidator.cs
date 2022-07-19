using Business.Handlers.TourDemands.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.TourDemands.ValidationRules
{
    public class CreateTourDemandValidator: AbstractValidator<CreateTourDemandCommand>
    {
        public CreateTourDemandValidator()
        {
            RuleFor(x => x.MainDemandId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.AdultCount).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.ChildCount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Period).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();
        }
    }

    public class UpdateTourDemandValidator : AbstractValidator<UpdateTourDemandCommand>
    {
        public UpdateTourDemandValidator()
        {
            RuleFor(x => x.TourDemandId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.MainDemandId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.AdultCount).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.ChildCount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.IsOpen).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotEmpty().NotNull();
        }
    }

    public class DeleteTourDemandValidator : AbstractValidator<DeleteTourDemandCommand>
    {
        public DeleteTourDemandValidator()
        {
            RuleFor(x => x.TourDemandId).NotNull().NotEmpty().NotEqual(0);
        }
    }
}
