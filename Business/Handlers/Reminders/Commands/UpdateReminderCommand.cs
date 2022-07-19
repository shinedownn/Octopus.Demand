
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Reminders.ValidationRules;
using System;
using Business.Helpers;

namespace Business.Handlers.Reminders.Commands
{


    public class UpdateReminderCommand : IRequest<IResult>
    {
        public int ReminderId { get; set; }
        public int? HotelDemandActionId { get; set; }
        public int? TourDemandActionId { get; set; }
        public bool IsActive { get; set; }
        public DateTime ReminderDate { get; set; }  
        public string Description { get; set; }

        public class UpdateReminderCommandHandler : IRequestHandler<UpdateReminderCommand, IResult>
        {
            private readonly IReminderRepository _reminderRepository;
            private readonly IMediator _mediator;

            public UpdateReminderCommandHandler(IReminderRepository reminderRepository, IMediator mediator)
            {
                _reminderRepository = reminderRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateReminderValidator), Priority = 1)] 
            [LogAspect(typeof(PostgreSqlLogger))]
             
            public async Task<IResult> Handle(UpdateReminderCommand request, CancellationToken cancellationToken)
            {
                var isThereReminderRecord = await _reminderRepository.GetAsync(u => u.ReminderId == request.ReminderId);
                if (isThereReminderRecord == null) return new ErrorResult(Messages.RecordNotFound); 

                isThereReminderRecord.HotelDemandActionId = request.HotelDemandActionId;
                isThereReminderRecord.TourDemandActionId = request.TourDemandActionId;
                isThereReminderRecord.IsActive = request.IsActive;
                isThereReminderRecord.ReminderDate = request.ReminderDate;
                isThereReminderRecord.Description = request.Description;
                _reminderRepository.Update(isThereReminderRecord);
                await _reminderRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

