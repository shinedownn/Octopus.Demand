
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Reminders.ValidationRules;
using System;
using Business.Helpers;

namespace Business.Handlers.Reminders.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateReminderCommand : IRequest<IResult>
    {

        public int? HotelDemandActionId { get; set; }
        public int? TourDemandActionId { get; set; }
        public DateTime ReminderDate { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }  

        public class CreateReminderCommandHandler : IRequestHandler<CreateReminderCommand, IResult>
        {
            private readonly IReminderRepository _reminderRepository;
            private readonly IMediator _mediator;
            public CreateReminderCommandHandler(IReminderRepository reminderRepository, IMediator mediator)
            {
                _reminderRepository = reminderRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateReminderValidator), Priority = 1)] 
            [LogAspect(typeof(PostgreSqlLogger))] 
            public async Task<IResult> Handle(CreateReminderCommand request, CancellationToken cancellationToken)
            { 
                var FullName = JwtHelper.GetValue("name").ToString();

                var departmentId = Convert.ToInt32(JwtHelper.GetValue("departmentId").ToString());

                var addedReminder = new Reminder
                {
                    HotelDemandActionId = request.HotelDemandActionId,
                    TourDemandActionId = request.TourDemandActionId,
                    IsActive = request.IsActive,
                    CreatedUserName = FullName,
                    CreateDate = DateTime.Now, 
                    ReminderDate=request.ReminderDate,
                    Description=request.Description
                };

                _reminderRepository.Add(addedReminder);
                await _reminderRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}