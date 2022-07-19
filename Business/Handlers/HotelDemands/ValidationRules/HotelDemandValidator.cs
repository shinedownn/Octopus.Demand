using Business.Constants;
using Business.Handlers.HotelDemands.Commands;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.HotelDemands.ValidationRules
{
    public class CreateHotelDemandValidator: AbstractValidator<CreateHotelDemandCommand>
    {
        public CreateHotelDemandValidator()
        {  
            RuleFor(x => x.AdultCount).NotNull().NotEqual(0).NotEmpty();
            RuleFor(x => x.ChildCount).GreaterThanOrEqualTo(0); 
        }        
    }

    public class UpdateHotelDemandValidator : AbstractValidator<UpdateHotelDemandCommand>
    {
        public UpdateHotelDemandValidator()
        {
            RuleFor(x => x.HotelDemandId).NotNull().NotEmpty();  
            RuleFor(x => x.AdultCount).NotNull().NotEqual(0).NotEmpty();
            RuleFor(x => x.ChildCount).GreaterThanOrEqualTo(0);            
        }
    }

    public class DeleteHotelDemandValidator : AbstractValidator<DeleteHotelDemandCommand>
    {
        public DeleteHotelDemandValidator()
        {
            RuleFor(x => x.HotelDemandId).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
