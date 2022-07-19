using Business.Handlers.MainDemandActions.Commands; 
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.MainDemandActions.ValidationRules
{
    public class CreateMainDemandActionValidator:AbstractValidator<CreateMainDemandActionCommand>
    {
        public CreateMainDemandActionValidator()
        {
            RuleFor(x => x.MainDemandId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.ActionId).NotNull().NotEmpty().GreaterThan(0);
        }
    }

    public class UpdateMainDemandActionValidator : AbstractValidator<UpdateMainDemandActionCommand>
    {
        public UpdateMainDemandActionValidator()
        {
            RuleFor(x => x.MainDemandActionId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.MainDemandId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.ActionId).NotNull().NotEmpty().GreaterThan(0); 
        }
    }

    public class DeleteMainDemandActionValidator : AbstractValidator<DeleteMainDemandActionCommand>
    {
        public DeleteMainDemandActionValidator()
        {
            RuleFor(x => x.ActionId).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
