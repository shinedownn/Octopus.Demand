using Business.Constants;
using Business.Handlers.HotelDemandActions.ValidationRules;
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

namespace Business.Handlers.HotelDemandActions.Commands
{
    public class UpdateHotelDemandActionCommand : IRequest<IResult>
    { 
        public int HotelDemandActionId { get; set; }
         public int HotelDemandId { get; set; }
         public int MainDemandId { get; set; }
         public int ActionId { get; set; }
         public bool IsOpen { get; set; }

        public class UpdateHotelDemandActionCommandHandler : IRequestHandler<UpdateHotelDemandActionCommand, IResult>
        {
            private readonly IHotelDemandActionRepository _HotelDemandActionRepository;
            public UpdateHotelDemandActionCommandHandler(IHotelDemandActionRepository HotelDemandActionRepository)
            {
                _HotelDemandActionRepository = HotelDemandActionRepository;
            }
            [ValidationAspect(typeof(UpdateHotelDemandActionValidator), Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger),"Hotel talep aksiyon güncellendi",Priority =3)]
            public async Task<IResult> Handle(UpdateHotelDemandActionCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() => {
                    var updateToAction = _HotelDemandActionRepository.GetAsync(x => x.HotelDemandActionId == request.HotelDemandActionId).GetAwaiter().GetResult();
                    updateToAction.HotelDemandId = request.HotelDemandId;
                    updateToAction.ActionId = request.ActionId;
                    updateToAction.IsOpen = request.IsOpen;
                    _HotelDemandActionRepository.Update(updateToAction);
                    _HotelDemandActionRepository.SaveChangesAsync().GetAwaiter().GetResult();
                    return new SuccessResult(Messages.Updated);
                }); 
            }
        }
    }
}
