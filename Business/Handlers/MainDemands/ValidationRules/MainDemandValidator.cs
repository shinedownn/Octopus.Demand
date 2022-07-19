
using Business.Handlers.Demands.Commands;
using FluentValidation;

namespace Business.Handlers.Demands.ValidationRules
{
    public class CreateMainDemandValidator : AbstractValidator<CreateMainDemandCommand>
    {
        public CreateMainDemandValidator()
        {
            RuleFor(x => x.ContactId).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Description).NotEmpty(); 

        }
    }
    public class UpdateMainDemandValidator : AbstractValidator<UpdateMainDemandCommand>
    {
        public UpdateMainDemandValidator()
        {
            RuleFor(x => x.ContactId).GreaterThan(0);
            RuleFor(x => x.MainDemandId).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Description).NotEmpty(); 
        }
    } 

    public class DeleteMainDemandValidator : AbstractValidator<DeleteMainDemandCommand>
    {
        public DeleteMainDemandValidator()
        {
            RuleFor(x => x.MainDemandId).NotEqual(0).NotNull().NotEmpty();
        }
    }
}