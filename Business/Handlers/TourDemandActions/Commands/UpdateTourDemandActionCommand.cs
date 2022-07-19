using Business.Constants;
using Business.Handlers.TourDemandActions.ValidationRules;
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

namespace Business.Handlers.TourDemandActions.Commands
{
    public class UpdateTourDemandActionCommand : IRequest<IResult>
    {
         public int TourDemandActionId { get; set; } 
         public int TourDemandId { get; set; }
         public int ActionId { get; set; } 
         public bool IsOpen { get; set; }

        public class UpdateTourDemandActionCommandHandler : IRequestHandler<UpdateTourDemandActionCommand, IResult>
        {
            private readonly ITourDemandActionRepository _TourDemandActionRepository;
            public UpdateTourDemandActionCommandHandler(ITourDemandActionRepository TourDemandActionRepository)
            {
                _TourDemandActionRepository = TourDemandActionRepository;
            }
            [ValidationAspect(typeof(UpdateTourDemandActionValidator), Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger),"Tur talebi güncellendi",Priority =3)]
            public async Task<IResult> Handle(UpdateTourDemandActionCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() => {
                    var updateToAction = _TourDemandActionRepository.GetAsync(x => x.TourDemandActionId == request.TourDemandActionId).GetAwaiter().GetResult();
                    if (updateToAction == null) return new ErrorResult(Messages.RecordNotFound);
                    updateToAction.TourDemandId = request.TourDemandId;
                    updateToAction.ActionId = request.ActionId;
                    updateToAction.IsOpen = request.IsOpen;

                    _TourDemandActionRepository.Update(updateToAction);
                    _TourDemandActionRepository.SaveChangesAsync().GetAwaiter();
                    return new SuccessResult(Messages.Updated);
                }); 
            }
        }
    }
}
