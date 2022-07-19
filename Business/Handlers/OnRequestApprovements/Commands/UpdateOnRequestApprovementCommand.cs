
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
using Business.Handlers.OnRequestApprovements.ValidationRules;


namespace Business.Handlers.OnRequestApprovements.Commands
{


    public class UpdateOnRequestApprovementCommand : IRequest<IResult>
    {
        public int OnRequestApprovementId { get; set; }
        public int OnRequestId { get; set; }
        public int DepartmentId { get; set; }
        public string CreatedUserName { get; set; }
        public System.DateTime CreateDate { get; set; }

        public class UpdateOnRequestApprovementCommandHandler : IRequestHandler<UpdateOnRequestApprovementCommand, IResult>
        {
            private readonly IOnRequestApprovementRepository _onRequestApprovementRepository;
            private readonly IMediator _mediator;

            public UpdateOnRequestApprovementCommandHandler(IOnRequestApprovementRepository onRequestApprovementRepository, IMediator mediator)
            {
                _onRequestApprovementRepository = onRequestApprovementRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOnRequestApprovementValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(PostgreSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOnRequestApprovementCommand request, CancellationToken cancellationToken)
            {
                var isThereOnRequestApprovementRecord = await _onRequestApprovementRepository.GetAsync(u => u.OnRequestApprovementId == request.OnRequestApprovementId);


                isThereOnRequestApprovementRecord.OnRequestId = request.OnRequestId;
                isThereOnRequestApprovementRecord.DepartmentId = request.DepartmentId;
                isThereOnRequestApprovementRecord.CreatedUserName = request.CreatedUserName;
                isThereOnRequestApprovementRecord.CreateDate = request.CreateDate;


                _onRequestApprovementRepository.Update(isThereOnRequestApprovementRecord);
                await _onRequestApprovementRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

