using Business.Constants;
using Business.Handlers.HotelDemands.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
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
    public class UpdateHotelDemandCommand : IRequest<IResult>
    {
         public int HotelDemandId { get; set; }
         public int MainDemandId { get; set; }
         public string Name { get; set; }
         public DateTime CheckIn { get; set; }
         public DateTime CheckOut { get; set; }
         public int AdultCount { get; set; }
         public int ChildCount { get; set; }
         public string Description { get; set; }
         public bool IsOpen { get; set; }

        public class UpdateHotelDemandCommandHandler : IRequestHandler<UpdateHotelDemandCommand, IResult>
        {
            private readonly IHotelDemandRepository _hotelDemandRepository;

            public UpdateHotelDemandCommandHandler(IHotelDemandRepository hotelDemandRepository)
            {
                _hotelDemandRepository = hotelDemandRepository;
            }
            [ValidationAspect(typeof(UpdateHotelDemandValidator),Priority =1)]
            [TransactionScopeAspectAsync]
            [LogAspect(typeof(PostgreSqlLogger),"Hotel talep güncellendi",Priority =3)]
            public async Task<IResult> Handle(UpdateHotelDemandCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run(() => {
                    var isThereDemandRecord = _hotelDemandRepository.GetAsync(x => x.HotelDemandId == request.HotelDemandId).GetAwaiter().GetResult();

                    isThereDemandRecord.MainDemandId = request.MainDemandId;
                    isThereDemandRecord.Name = request.Name;
                    isThereDemandRecord.CheckIn = request.CheckIn.ToUniversalTime();
                    isThereDemandRecord.CheckOut = request.CheckOut.ToUniversalTime();
                    isThereDemandRecord.AdultCount = request.AdultCount;
                    isThereDemandRecord.ChildCount = request.ChildCount;
                    isThereDemandRecord.TotalCount = request.AdultCount + request.ChildCount;
                    isThereDemandRecord.IsOpen = request.IsOpen;
                    isThereDemandRecord.Description = request.Description;

                    _hotelDemandRepository.Update(isThereDemandRecord);
                    _hotelDemandRepository.SaveChangesAsync().GetAwaiter();
                    return new SuccessResult(Messages.Updated);
                });

                
            }
        }
    }
}
