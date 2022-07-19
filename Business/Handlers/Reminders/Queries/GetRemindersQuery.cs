
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;
using Entities.Dtos.Reminder;
using AutoMapper;

namespace Business.Handlers.Reminders.Queries
{

    public class GetRemindersQuery : IRequest<IDataResult<IEnumerable<ReminderDto>>>
    {
        public class GetRemindersQueryHandler : IRequestHandler<GetRemindersQuery, IDataResult<IEnumerable<ReminderDto>>>
        {
            private readonly IReminderRepository _reminderRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public GetRemindersQueryHandler(IReminderRepository reminderRepository, IMediator mediator, IMapper mapper)
            {
                _reminderRepository = reminderRepository;
                _mediator = mediator;
                _mapper = mapper;
            }

            [LogAspect(typeof(PostgreSqlLogger))] 
            public async Task<IDataResult<IEnumerable<ReminderDto>>> Handle(GetRemindersQuery request, CancellationToken cancellationToken)
            {
                var reminders = await _reminderRepository.GetListAsync();
                var dtos = _mapper.Map<IEnumerable<ReminderDto>>(reminders);
                return new SuccessDataResult<IEnumerable<ReminderDto>>(dtos);
            }
        }
    }
}