using Business.Handlers.Actions.Commands;
using Business.Handlers.Actions.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.Actions.ValidationRules
{
    public class CreateActionValidator:AbstractValidator<CreateActionCommand>
    {
        public CreateActionValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().NotNull();
            RuleFor(x => x.ActionType).IsInEnum<CreateActionCommand, ActionTypeEnum>();
        }
    }

    public class UpdateActionValidator : AbstractValidator<UpdateActionCommand>
    {
        public UpdateActionValidator()
        {
            RuleFor(x => x.ActionId).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.IsOpen).NotNull().NotEmpty();        
        }
    }

    public class DeleteActionValidator : AbstractValidator<DeleteActionCommand>
    {
        public DeleteActionValidator()
        {
            RuleFor(x => x.ActionId).GreaterThan(0);
        }
    }
}
