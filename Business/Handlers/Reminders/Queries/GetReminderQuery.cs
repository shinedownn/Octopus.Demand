
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Entities.Dtos.Reminder;
using AutoMapper;

namespace Business.Handlers.Reminders.Queries
{
    public class GetReminderQuery : IRequest<IDataResult<ReminderDto>>
    {
        public int ReminderId { get; set; }

        public class GetReminderQueryHandler : IRequestHandler<GetReminderQuery, IDataResult<ReminderDto>>
        {
            private readonly IReminderRepository _reminderRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            public GetReminderQueryHandler(IReminderRepository reminderRepository, IMediator mediator, IMapper mapper)
            {
                _reminderRepository = reminderRepository;
                _mediator = mediator;
                _mapper = mapper;
            }
            [LogAspect(typeof(PostgreSqlLogger))]
             
            public async Task<IDataResult<ReminderDto>> Handle(GetReminderQuery request, CancellationToken cancellationToken)
            {
                var reminder = await _reminderRepository.GetAsync(p => p.ReminderId == request.ReminderId);
                var dto= _mapper.Map<ReminderDto>(reminder);
                return new SuccessDataResult<ReminderDto>(dto);
            }
        }
    }
}
