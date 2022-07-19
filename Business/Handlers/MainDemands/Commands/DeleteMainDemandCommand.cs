
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
using Business.Handlers.Demands.ValidationRules;
using Core.Aspects.Autofac.Validation;
using System.ComponentModel.DataAnnotations;

namespace Business.Handlers.Demands.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteMainDemandCommand : IRequest<IResult>
    {
         public int MainDemandId { get; set; }

        public class DeleteDemandCommandHandler : IRequestHandler<DeleteMainDemandCommand, IResult>
        {
            private readonly IMainDemandRepository _demandRepository;
            private readonly IMediator _mediator;

            public DeleteDemandCommandHandler(IMainDemandRepository demandRepository, IMediator mediator)
            {
                _demandRepository = demandRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(DeleteMainDemandValidator), Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger),"Genel talep bilgisi silindi",Priority =3)]
            public async Task<IResult> Handle(DeleteMainDemandCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() => {
                    var demandToDelete = _demandRepository.GetAsync(p => p.MainDemandId == request.MainDemandId).GetAwaiter().GetResult();
                    if (demandToDelete == null) return new ErrorResult(Messages.RecordNotFound);
                    if (demandToDelete.IsOpen) return new ErrorResult(Messages.DemandIsOpenCannotDelete);
                    demandToDelete.IsDeleted = true;
                    _demandRepository.Update(demandToDelete);
                    _demandRepository.SaveChangesAsync().GetAwaiter();
                    return new SuccessResult(Messages.Deleted);
                }); 
            }
        }
    }
}

