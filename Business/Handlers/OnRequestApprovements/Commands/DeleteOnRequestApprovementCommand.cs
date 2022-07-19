
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


namespace Business.Handlers.OnRequestApprovements.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOnRequestApprovementCommand : IRequest<IResult>
    {
        public int OnRequestApprovementId { get; set; }

        public class DeleteOnRequestApprovementCommandHandler : IRequestHandler<DeleteOnRequestApprovementCommand, IResult>
        {
            private readonly IOnRequestApprovementRepository _onRequestApprovementRepository;
            private readonly IMediator _mediator;

            public DeleteOnRequestApprovementCommandHandler(IOnRequestApprovementRepository onRequestApprovementRepository, IMediator mediator)
            {
                _onRequestApprovementRepository = onRequestApprovementRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(PostgreSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOnRequestApprovementCommand request, CancellationToken cancellationToken)
            {
                var onRequestApprovementToDelete = _onRequestApprovementRepository.Get(p => p.OnRequestApprovementId == request.OnRequestApprovementId);

                _onRequestApprovementRepository.Delete(onRequestApprovementToDelete);
                await _onRequestApprovementRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

