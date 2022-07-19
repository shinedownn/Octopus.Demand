using Business.Handlers.TourDemandOnRequests.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.TourDemandOnRequests.ValidationRules
{
    public class CreateTourDemandOnRequestValidator : AbstractValidator<CreateTourDemandOnRequestCommand>
    {
        public CreateTourDemandOnRequestValidator()
        {
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x=>x.OnRequestId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x=>x.MainDemandId).NotNull().GreaterThan(0);
            RuleFor(x=>x.TourDemandId).NotNull().GreaterThan(0);
            
        }
    }

    public class UpdateTourDemandOnRequestValidator : AbstractValidator<UpdateTourDemandOnRequestCommand>
    {
        public UpdateTourDemandOnRequestValidator()
        {
            RuleFor(x => x.OnRequestId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x=>x.TourDemandOnRequestId).NotNull().GreaterThan(0);
            RuleFor(x => x.IsOpen).NotEmpty().NotNull(); 
        }
    }

    public class DeleteTourDemandOnRequestValidator : AbstractValidator<DeleteTourDemandOnRequestCommand>
    {
        public DeleteTourDemandOnRequestValidator()
        {
            RuleFor(x => x.TourDemandOnRequestId).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
