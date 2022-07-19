
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OnRequestApprovements.Queries
{
    public class GetOnRequestApprovementQuery : IRequest<IDataResult<OnRequestApprovement>>
    {
        public int OnRequestApprovementId { get; set; }

        public class GetOnRequestApprovementQueryHandler : IRequestHandler<GetOnRequestApprovementQuery, IDataResult<OnRequestApprovement>>
        {
            private readonly IOnRequestApprovementRepository _onRequestApprovementRepository;
            private readonly IMediator _mediator;

            public GetOnRequestApprovementQueryHandler(IOnRequestApprovementRepository onRequestApprovementRepository, IMediator mediator)
            {
                _onRequestApprovementRepository = onRequestApprovementRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(PostgreSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OnRequestApprovement>> Handle(GetOnRequestApprovementQuery request, CancellationToken cancellationToken)
            {
                var onRequestApprovement = await _onRequestApprovementRepository.GetAsync(p => p.OnRequestApprovementId == request.OnRequestApprovementId);
                return new SuccessDataResult<OnRequestApprovement>(onRequestApprovement);
            }
        }
    }
}
