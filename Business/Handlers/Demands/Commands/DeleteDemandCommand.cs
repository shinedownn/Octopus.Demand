using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.Demands.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
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

namespace Business.Handlers.Demands.Commands
{
    public class DeleteDemandCommand : IRequest<IResult>
    {
         public int MainDemandId { get; set; }

        public class DeleteDemandCommandHandler : IRequestHandler<DeleteDemandCommand, IResult>
        {
            private readonly IMainDemandRepository _mainDemandRepository;
            private readonly IHotelDemandRepository _hotelDemandRepository;
            private readonly ITourDemandRepository _tourDemandRepository;
            private readonly IHotelDemandOnRequestRepository _hotelDemandOnRequestRepository;
            private readonly ITourDemandOnRequestRepository _tourDemandOnRequestRepository;
            public DeleteDemandCommandHandler(IMainDemandRepository mainDemandRepository, IHotelDemandRepository hotelDemandRepository, ITourDemandRepository tourDemandRepository, IHotelDemandOnRequestRepository hotelDemandOnRequestRepository, ITourDemandOnRequestRepository tourDemandOnRequestRepository)
            {
                _mainDemandRepository = mainDemandRepository;
                _hotelDemandRepository = hotelDemandRepository;
                _tourDemandRepository = tourDemandRepository;
                _hotelDemandOnRequestRepository = hotelDemandOnRequestRepository;
                _tourDemandOnRequestRepository = tourDemandOnRequestRepository;
            }
            [ValidationAspect(typeof(DeleteDemandValidator),Priority =1)]
            [TransactionScopeAspectAsync(Priority =2)]
            [LogAspect(typeof(PostgreSqlLogger),"Talep Silindi",Priority =3)]
            public async Task<IResult> Handle(DeleteDemandCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() =>
                {
                    var mainDemand = _mainDemandRepository.GetAsync(x => x.MainDemandId == request.MainDemandId).GetAwaiter().GetResult();
                    if (mainDemand == null) return new ErrorResult(Messages.RecordNotFound);
                    if (mainDemand.IsOpen) return new ErrorResult(Messages.DemandIsOpenCannotDelete);

                    var hotelDemands = _hotelDemandRepository.GetListAsync(x => x.MainDemandId == request.MainDemandId).GetAwaiter().GetResult();
                    if (hotelDemands.Any(x => x.IsOpen)) return new ErrorResult(Messages.DemandIsOpenCannotDelete);

                    var tourDemands = _tourDemandRepository.GetListAsync(x => x.MainDemandId == request.MainDemandId).GetAwaiter().GetResult();
                    if (tourDemands.Any(x => x.IsOpen)) return new ErrorResult(Messages.DemandIsOpenCannotDelete);


                    mainDemand.IsDeleted = true;
                    _mainDemandRepository.Update(mainDemand);

                    hotelDemands.ToList().ForEach(async hoteldemand => {
                        hoteldemand.IsDeleted = true;
                        _hotelDemandRepository.Update(hoteldemand);
                        var hoteldemandsRequests = await _hotelDemandOnRequestRepository.GetListAsync(request => request.HotelDemandId == hoteldemand.HotelDemandId);
                        hoteldemandsRequests.ToList().ForEach(request => {
                            request.IsDeleted = true;
                            _hotelDemandOnRequestRepository.Update(request);
                        });
                        await _hotelDemandOnRequestRepository.SaveChangesAsync();
                    });

                    tourDemands.ToList().ForEach(async tourdemand => {
                        tourdemand.IsDeleted = true;
                        _tourDemandRepository.Update(tourdemand);
                        var tourdemandsrequests = await _tourDemandOnRequestRepository.GetListAsync(request => request.TourDemandId == tourdemand.TourDemandId);
                        tourdemandsrequests.ToList().ForEach(request => {
                            request.IsDeleted = true;
                            _tourDemandOnRequestRepository.Update(request);
                        });
                        await _tourDemandOnRequestRepository.SaveChangesAsync();
                    });

                    _mainDemandRepository.SaveChangesAsync().GetAwaiter().GetResult();
                    _hotelDemandRepository.SaveChangesAsync().GetAwaiter().GetResult();
                    _tourDemandRepository.SaveChangesAsync().GetAwaiter().GetResult();

                    return new SuccessResult(Messages.Deleted);
                });
                         
            }
        }
    }
}
