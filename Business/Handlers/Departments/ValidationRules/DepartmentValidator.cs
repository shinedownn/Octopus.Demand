
using Business.Handlers.Departments.Commands;
using FluentValidation;

namespace Business.Handlers.Departments.ValidationRules
{

    public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentValidator()
        {
            RuleFor(x => x.Name).NotEmpty();  
        }
    }
    public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentValidator()
        {
            RuleFor(x => x.Name).NotEmpty();  
        }
    }

    public class DeleteDepartmentValidator : AbstractValidator<DeleteDepartmentCommand>
    {
        public DeleteDepartmentValidator()
        {
            RuleFor(x => x.DepartmentId).GreaterThan(0);
        }
    }
}