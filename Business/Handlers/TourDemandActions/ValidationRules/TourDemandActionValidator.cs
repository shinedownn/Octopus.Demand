using Business.Handlers.TourDemandActions.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.TourDemandActions.ValidationRules
{
    public class CreateTourDemandActionValidator : AbstractValidator<CreateTourDemandActionCommand>
    {
        public CreateTourDemandActionValidator()
        {
            RuleFor(x => x.TourDemandId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.ActionId).NotNull().NotEmpty().GreaterThan(0);
        }
    }

    public class UpdateTourDemandActionValidator : AbstractValidator<UpdateTourDemandActionCommand>
    {
        public UpdateTourDemandActionValidator()
        {
            RuleFor(x => x.TourDemandActionId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.TourDemandId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.ActionId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.IsOpen).NotNull().NotEmpty();
        }
    }

    public class DeleteTourDemandActionValidator : AbstractValidator<DeleteTourDemandActionCommand>
    {
        public DeleteTourDemandActionValidator()
        {
            RuleFor(x => x.ActionId).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
