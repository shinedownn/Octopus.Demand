
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
using Business.Handlers.OnRequestApprovements.ValidationRules;

namespace Business.Handlers.OnRequestApprovements.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOnRequestApprovementCommand : IRequest<IResult>
    {

        public int OnRequestId { get; set; }
        public int DepartmentId { get; set; }
        public string CreatedUserName { get; set; }
        public System.DateTime CreateDate { get; set; }


        public class CreateOnRequestApprovementCommandHandler : IRequestHandler<CreateOnRequestApprovementCommand, IResult>
        {
            private readonly IOnRequestApprovementRepository _onRequestApprovementRepository;
            private readonly IMediator _mediator;
            public CreateOnRequestApprovementCommandHandler(IOnRequestApprovementRepository onRequestApprovementRepository, IMediator mediator)
            {
                _onRequestApprovementRepository = onRequestApprovementRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOnRequestApprovementValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(PostgreSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOnRequestApprovementCommand request, CancellationToken cancellationToken)
            {
                var isThereOnRequestApprovementRecord = _onRequestApprovementRepository.Query().Any(u => u.OnRequestId == request.OnRequestId);

                if (isThereOnRequestApprovementRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOnRequestApprovement = new OnRequestApprovement
                {
                    OnRequestId = request.OnRequestId,
                    DepartmentId = request.DepartmentId,
                    CreatedUserName = request.CreatedUserName,
                    CreateDate = request.CreateDate,

                };

                _onRequestApprovementRepository.Add(addedOnRequestApprovement);
                await _onRequestApprovementRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}