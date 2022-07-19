
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Reminders.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteReminderCommand : IRequest<IResult>
    {
        public int ReminderId { get; set; }

        public class DeleteReminderCommandHandler : IRequestHandler<DeleteReminderCommand, IResult>
        {
            private readonly IReminderRepository _reminderRepository;
            private readonly IMediator _mediator;

            public DeleteReminderCommandHandler(IReminderRepository reminderRepository, IMediator mediator)
            {
                _reminderRepository = reminderRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(PostgreSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteReminderCommand request, CancellationToken cancellationToken)
            {
                var reminderToDelete = _reminderRepository.Get(p => p.ReminderId == request.ReminderId);

                _reminderRepository.Delete(reminderToDelete);
                await _reminderRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

