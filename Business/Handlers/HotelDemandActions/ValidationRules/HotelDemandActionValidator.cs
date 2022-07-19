using Business.Handlers.HotelDemandActions.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.HotelDemandActions.ValidationRules
{
    public class CreateHotelDemandActionValidator : AbstractValidator<CreateHotelDemandActionCommand>
    {
        public CreateHotelDemandActionValidator()
        {
            RuleFor(x => x.HotelDemandId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.ActionId).NotNull().NotEmpty().GreaterThan(0);
        }
    }

    public class UpdateHotelDemandActionValidator : AbstractValidator<UpdateHotelDemandActionCommand>
    {
        public UpdateHotelDemandActionValidator()
        {
            RuleFor(x => x.HotelDemandActionId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.HotelDemandId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.ActionId).NotNull().NotEmpty().GreaterThan(0); 
        }
    }

    public class DeleteHotelDemandActionValidator : AbstractValidator<DeleteHotelDemandActionCommand>
    {
        public DeleteHotelDemandActionValidator()
        {
            RuleFor(x => x.ActionId).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
