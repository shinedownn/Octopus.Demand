using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.CrossCuttingConcerns.Caching.ErcanProduct;
using Core.Utilities.Results;
using DataAccess.Abstract.ErcanProduct;
using Entities.ErcanProduct.Concrete.Tour;
using Entities.ErcanProduct.Dtos;
using MediatR;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ErcanProduct.Tour.TourPeriods.Queries
{
    public class GetTourPeriods : IRequest<IDataResult<IEnumerable<TourAvailablePeriod>>>
    { 
        public int TourId { get; set; }

        public class GetTourPeriodsHandler : IRequestHandler<GetTourPeriods, IDataResult<IEnumerable<TourAvailablePeriod>>>
        {
            private readonly ITourRepository _tourRepository;
            private readonly ITourPeriodRepository _tourPeriodRepository;
            private readonly IMapper _mapper;
            public GetTourPeriodsHandler(ITourRepository tourRepository,ITourPeriodRepository tourPeriodRepository, IMapper mapper)
            {
                _tourRepository = tourRepository;
                _tourPeriodRepository = tourPeriodRepository;
                _mapper = mapper; 
            }

            public async Task<IDataResult<IEnumerable<TourAvailablePeriod>>> Handle(GetTourPeriods request, CancellationToken cancellationToken)
            { 
                return await Task.Run<IDataResult<IEnumerable<TourAvailablePeriod>>>(() =>
                { 
                    var cache = new ErcanProductCacheManager();
                    var cacheData = cache.Get<TourDetail>("urn:tourdetailcachemodel:" + request.TourId);
                    if (cacheData != null) return new SuccessDataResult<IEnumerable<TourAvailablePeriod>>(cacheData.TourAvailablePeriods);

                    var ss = DateTime.Today.AddDays(1);
                    //var tour = _tourRepository.GetAsync(x => x.ProductId == request.ProductId).GetAwaiter().GetResult();
                    var tourPeriods = _tourPeriodRepository.GetListAsync(x => x.TourId == request.TourId && x.StartDate > ss).GetAwaiter().GetResult();
                    var dtos = _mapper.Map<IEnumerable<TourAvailablePeriod>>(tourPeriods);
                    return new SuccessDataResult<IEnumerable<TourAvailablePeriod>>(dtos);
                }); 
            }
        }
    }
}
