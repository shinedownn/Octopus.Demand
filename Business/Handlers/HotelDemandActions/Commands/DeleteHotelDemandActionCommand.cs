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
    public class DeleteHotelDemandActionCommand : IRequest<IResult>
    {
         public int ActionId { get; set; }

        public class DeleteHotelDemandActionCommandHandler : IRequestHandler<DeleteHotelDemandActionCommand, IResult>
        {
            private readonly IHotelDemandActionRepository _HotelDemandActionRepository;
            public DeleteHotelDemandActionCommandHandler(IHotelDemandActionRepository HotelDemandActionRepository)
            {
                _HotelDemandActionRepository = HotelDemandActionRepository;
            }
            [ValidationAspect(typeof(DeleteHotelDemandActionValidator), Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger),"Hotel talep aksiyon silindi",Priority =2)]
            public async Task<IResult> Handle(DeleteHotelDemandActionCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() => {
                    var deleteToAction = _HotelDemandActionRepository.GetAsync(x => x.ActionId == request.ActionId).GetAwaiter().GetResult();
                    if (deleteToAction == null) return new ErrorResult(Messages.RecordNotFound);
                    if (deleteToAction.IsOpen) return new ErrorResult(Messages.ActionIsOpenCannotDelete);
                    deleteToAction.IsDeleted = true;
                    _HotelDemandActionRepository.Update(deleteToAction);
                    _HotelDemandActionRepository.SaveChangesAsync().GetAwaiter().GetResult();
                    return new SuccessResult(Messages.Deleted);
                });
                
            }
        }
    }
}
