using AutoMapper;
using Business.Handlers.Actions.Enums;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Actions.Dtos;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Actions.Queries
{
    public class GetActionsByTypeQuery : IRequest<IDataResult<IEnumerable<ActionDto>>>
    {
         public ActionTypeEnum actionTypeEnum { get; set; }
        public bool IsOpen { get; set; }

        public class GetActionsByTypeQueryHandler : IRequestHandler<GetActionsByTypeQuery, IDataResult<IEnumerable<ActionDto>>>
        {
            private readonly IActionRepository actionRepository;
            private readonly IMapper mapper;
            public GetActionsByTypeQueryHandler(IActionRepository actionRepository, IMapper mapper)
            {
                this.actionRepository = actionRepository;
                this.mapper = mapper;
            } 
            public async Task<IDataResult<IEnumerable<ActionDto>>> Handle(GetActionsByTypeQuery request, CancellationToken cancellationToken)
            {
                var actions = await actionRepository.GetListAsync(x => x.ActionType == request.actionTypeEnum.ToString() && x.IsOpen==request.IsOpen);
                var actiondtos=actions.Select(x=> mapper.Map<ActionDto>(x));
                return new SuccessDataResult<IEnumerable<ActionDto>>(actiondtos);
            }
        }
    }
}
