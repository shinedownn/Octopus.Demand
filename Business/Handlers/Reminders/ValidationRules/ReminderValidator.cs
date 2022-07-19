
using Business.Handlers.Reminders.Commands;
using FluentValidation;
using System;

namespace Business.Handlers.Reminders.ValidationRules
{

    public class CreateReminderValidator : AbstractValidator<CreateReminderCommand>
    {
        public CreateReminderValidator()
        { 
            RuleFor(x => x.ReminderDate).NotNull().GreaterThan(DateTime.Now); 
        }
    }
    public class UpdateReminderValidator : AbstractValidator<UpdateReminderCommand>
    {
        public UpdateReminderValidator()
        { 
            RuleFor(x => x.ReminderDate).NotNull().GreaterThan(DateTime.Now); 
             
        }
    }
}