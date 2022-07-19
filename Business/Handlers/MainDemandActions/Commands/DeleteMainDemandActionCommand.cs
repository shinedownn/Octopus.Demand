using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.MainDemandActions.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.MainDemandActions.Commands
{
    public class DeleteMainDemandActionCommand:IRequest<IResult>
    {
         public int ActionId { get; set; }

        public class DeleteMainDemandActionCommandHandler : IRequestHandler<DeleteMainDemandActionCommand, IResult>
        {
            private readonly IMainDemandActionRepository _mainDemandActionRepository;
            public DeleteMainDemandActionCommandHandler(IMainDemandActionRepository mainDemandActionRepository)
            {
                _mainDemandActionRepository = mainDemandActionRepository;
            }
            [ValidationAspect(typeof(DeleteMainDemandActionValidator),Priority =1)]
            [LogAspect(typeof(PostgreSqlLogger),"Genel talep aksiyonu silindi",Priority =3)]
            public async Task<IResult> Handle(DeleteMainDemandActionCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() => {
                    var deleteToAction = _mainDemandActionRepository.GetAsync(x => x.ActionId == request.ActionId).GetAwaiter().GetResult();
                    if (deleteToAction == null) return new ErrorResult(Messages.RecordNotFound);
                    if (deleteToAction.IsOpen) return new ErrorResult(Messages.ActionIsOpenCannotDelete);
                    deleteToAction.IsDeleted = true;
                    _mainDemandActionRepository.Update(deleteToAction);
                    _mainDemandActionRepository.SaveChangesAsync().GetAwaiter();
                    return new SuccessResult(Messages.Deleted);
                }); 
            }
        }
    }
}
