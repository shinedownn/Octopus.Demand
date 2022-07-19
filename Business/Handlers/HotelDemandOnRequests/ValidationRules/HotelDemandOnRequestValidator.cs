using Business.Handlers.HotelDemandOnRequests.Commands;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.HotelDemandOnRequests.ValidationRules
{
    public class CreateHotelDemandOnRequestValidator : AbstractValidator<CreateHotelDemandOnRequestCommand>
    {
        public CreateHotelDemandOnRequestValidator()
        {
            RuleFor(x => x.OnRequestId).GreaterThan(0);
            RuleFor(x=>x.HotelDemandId).GreaterThan(0);
            RuleFor(x=>x.MainDemandId).GreaterThan(0);
            //RuleFor(x => x.Description).NotEmpty().NotNull();            
        }
    }
    public class UpdateHotelDemandOnRequestValidator : AbstractValidator<UpdateHotelDemandOnRequestCommand>
    {
        public UpdateHotelDemandOnRequestValidator()
        {
            RuleFor(x=>x.OnRequestId).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x=>x.HotelDemandOnRequestId).NotEmpty().NotNull().GreaterThan(0);
            //RuleFor(x=>x.Description).NotEmpty().NotNull(); 
            RuleFor(x=>x.IsOpen).NotNull();

        }
    }
    public class DeleteHotelDemandOnRequestValidator : AbstractValidator<DeleteHotelDemandOnRequestCommand>
    {
        public DeleteHotelDemandOnRequestValidator()
        {
            RuleFor(x => x.HotelDemandOnRequestId).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
