
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

namespace Business.Handlers.OnRequestApprovements.Queries
{

    public class GetOnRequestApprovementsQuery : IRequest<IDataResult<IEnumerable<OnRequestApprovement>>>
    {
        public class GetOnRequestApprovementsQueryHandler : IRequestHandler<GetOnRequestApprovementsQuery, IDataResult<IEnumerable<OnRequestApprovement>>>
        {
            private readonly IOnRequestApprovementRepository _onRequestApprovementRepository;
            private readonly IMediator _mediator;

            public GetOnRequestApprovementsQueryHandler(IOnRequestApprovementRepository onRequestApprovementRepository, IMediator mediator)
            {
                _onRequestApprovementRepository = onRequestApprovementRepository;
                _mediator = mediator;
            }
             
            [LogAspect(typeof(PostgreSqlLogger))] 
            public async Task<IDataResult<IEnumerable<OnRequestApprovement>>> Handle(GetOnRequestApprovementsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OnRequestApprovement>>(await _onRequestApprovementRepository.GetListAsync());
            }
        }
    }
}