
using Business.Handlers.OnRequestApprovements.Commands;
using FluentValidation;

namespace Business.Handlers.OnRequestApprovements.ValidationRules
{

    public class CreateOnRequestApprovementValidator : AbstractValidator<CreateOnRequestApprovementCommand>
    {
        public CreateOnRequestApprovementValidator()
        {
            RuleFor(x => x.OnRequestId).NotEmpty();
            RuleFor(x => x.DepartmentId).NotEmpty();
            RuleFor(x => x.CreatedUserName).NotEmpty();
            RuleFor(x => x.CreateDate).NotEmpty();

        }
    }
    public class UpdateOnRequestApprovementValidator : AbstractValidator<UpdateOnRequestApprovementCommand>
    {
        public UpdateOnRequestApprovementValidator()
        {
            RuleFor(x => x.OnRequestId).NotEmpty();
            RuleFor(x => x.DepartmentId).NotEmpty();
            RuleFor(x => x.CreatedUserName).NotEmpty();
            RuleFor(x => x.CreateDate).NotEmpty();

        }
    }
}