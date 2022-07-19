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
    public class UpdateMainDemandActionCommand : IRequest<IResult>
    {
         public int MainDemandActionId { get; set; }
         public int MainDemandId { get;  set; }
         public int ActionId { get;  set; }
         public bool IsOpen { get; set; }       

        public class UpdateMainDemandActionCommandHandler : IRequestHandler<UpdateMainDemandActionCommand, IResult>
        {
            private readonly IMainDemandActionRepository _mainDemandActionRepository;
            public UpdateMainDemandActionCommandHandler(IMainDemandActionRepository mainDemandActionRepository)
            {
                _mainDemandActionRepository = mainDemandActionRepository;
            }
            [ValidationAspect(typeof(UpdateMainDemandActionValidator), Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger),"Genel talep aksiyonu güncellendi",Priority =3)]
            public async Task<IResult> Handle(UpdateMainDemandActionCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() => {
                    var updateToAction = _mainDemandActionRepository.GetAsync(x => x.MainDemandActionId == request.MainDemandActionId).GetAwaiter().GetResult();
                    updateToAction.MainDemandId = request.MainDemandId;
                    updateToAction.ActionId = request.ActionId;
                    updateToAction.IsOpen = request.IsOpen;
                    _mainDemandActionRepository.Update(updateToAction);
                    _mainDemandActionRepository.SaveChangesAsync().GetAwaiter();
                    return new SuccessResult(Messages.Updated);
                }); 
            }
        }
    }
}
