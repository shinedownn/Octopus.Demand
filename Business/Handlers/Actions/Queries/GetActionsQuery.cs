using AutoMapper;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Actions.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Actions.Queries
{
    public class GetActionsQuery : IRequest<IDataResult<IEnumerable<ActionDto>>>
    {
        public class GetActionsQueryHandler : IRequestHandler<GetActionsQuery, IDataResult<IEnumerable<ActionDto>>>
        {
            private readonly IActionRepository actionRepository;
            private readonly IMapper mapper;
            public GetActionsQueryHandler(IActionRepository actionRepository, IMapper mapper)
            {
                this.actionRepository = actionRepository;
                this.mapper = mapper;
            } 
            public async Task<IDataResult<IEnumerable<ActionDto>>> Handle(GetActionsQuery request, CancellationToken cancellationToken)
            {
                var actions = await actionRepository.GetListAsync();
                var actiondtos = actions.Select(x => mapper.Map<ActionDto>(x));
                return new SuccessDataResult<IEnumerable<ActionDto>>(actiondtos);
            }
        }
    }
}
