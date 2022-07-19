using Business.BusinessAspects;
using Business.Constants;
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

namespace Business.Handlers.HotelDemands.Commands
{
    public class DeleteHotelDemandCommand : IRequest<IResult>
    {
         public int HotelDemandId { get; set; }

        public class DeleteHotelDemandCommandHandler : IRequestHandler<DeleteHotelDemandCommand, IResult>
        {
            private readonly IHotelDemandRepository _hotelDemandRepository;

            public DeleteHotelDemandCommandHandler(IHotelDemandRepository hotelDemandRepository)
            {
                _hotelDemandRepository = hotelDemandRepository;
            } 

            [ValidationAspect(typeof(DeleteHotelDemandCommand),Priority =1)]
            [LogAspect(typeof(PostgreSqlLogger),"Hotel talep silindi",Priority =3)]
            public async Task<IResult> Handle(DeleteHotelDemandCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() => {
                    var demandToDelete = _hotelDemandRepository.GetAsync(x => x.HotelDemandId == request.HotelDemandId).GetAwaiter().GetResult();
                    if (demandToDelete == null) return new ErrorResult(Messages.DemandIsOpenCannotDelete);
                    demandToDelete.IsDeleted = true;
                    _hotelDemandRepository.Update(demandToDelete);
                    _hotelDemandRepository.SaveChangesAsync();
                    return new SuccessResult(Messages.Deleted);
                });         
            }
        }
    }
}
