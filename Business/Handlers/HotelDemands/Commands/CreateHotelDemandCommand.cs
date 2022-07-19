using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.HotelDemands.ValidationRules;
using Business.Helpers;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
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
    public class CreateHotelDemandCommand : IRequest<IResult>
    {
         public int MainDemandId { get; set; }
         public int HotelId { get; set; }
         public string Name { get; set; }
         public DateTime CheckIn { get; set; }
         public DateTime CheckOut { get; set; }
         public int AdultCount { get; set; }
         public int ChildCount { get; set; }
         public string Description { get; set; } 

        public class CreateHotelDemandCommandHandler : IRequestHandler<CreateHotelDemandCommand, IResult>
        {
            private readonly IHotelDemandRepository _hotelDemandRepository; 
            public CreateHotelDemandCommandHandler(IHotelDemandRepository hotelDemandRepository)
            {
                this._hotelDemandRepository = hotelDemandRepository;
            }
            [ValidationAspect(typeof(CreateHotelDemandValidator),Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger),"Hotel talebi oluşturuldu",Priority =3)]
            public async Task<IResult> Handle(CreateHotelDemandCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run(() => {
                    var addedHotelDemand = new HotelDemand
                    {
                        MainDemandId = request.MainDemandId,
                        HotelId = request.HotelId,
                        Name = request.Name,
                        CheckIn = request.CheckIn.ToUniversalTime(),
                        CheckOut = request.CheckOut.ToUniversalTime(),
                        AdultCount = request.AdultCount,
                        ChildCount = request.ChildCount,
                        TotalCount = request.AdultCount + request.ChildCount,
                        Description = request.Description,
                        IsOpen = true,
                        IsDeleted = false,
                        CreatedUserName = JwtHelper.GetValue("name").ToString(),
                        CreateDate = DateTime.Now,
                    };

                    _hotelDemandRepository.Add(addedHotelDemand);
                    _hotelDemandRepository.SaveChangesAsync().GetAwaiter().GetResult();
                    return new SuccessResult(Messages.Added);
                }); 
            }
        }
    }
}
