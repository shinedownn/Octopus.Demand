using Business.Handlers.OnRequests.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.OnRequests.ValidationRules
{
    public class CreateOnRequestValidator : AbstractValidator<CreateOnRequestCommand>
    {
        public CreateOnRequestValidator()
        {
            RuleFor(x=>x.Name).NotNull().NotEmpty(); 
        }
    }
    public class UpdateOnRequestValidator : AbstractValidator<UpdateOnRequestCommand>
    {
        public UpdateOnRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.OnRequestId).GreaterThan(0);
        }
    }
    public class DeleteOnRequestValidator : AbstractValidator<DeleteOnRequestCommand>
    {
        public DeleteOnRequestValidator()
        {
            RuleFor(x => x.OnRequestId).GreaterThan(0);            
        }
    }
}
