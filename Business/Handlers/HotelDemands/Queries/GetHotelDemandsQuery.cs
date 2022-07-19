using AutoMapper;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Entities.HotelDemands.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.HotelDemands.Queries
{
    public class GetHotelDemandsQuery : IRequest<IDataResult<IEnumerable<HotelDemandDto>>>
    {
         public int MainDemandId { get; set; }

        public class GetHotelDemandsQueryHandler : IRequestHandler<GetHotelDemandsQuery, IDataResult<IEnumerable<HotelDemandDto>>>
        {
            private readonly IHotelDemandRepository _hotelDemandRepository;
            private readonly IMapper _mapper;

            public GetHotelDemandsQueryHandler(IHotelDemandRepository hotelDemandRepository, IMapper mapper)
            {
                _mapper = mapper;
                _hotelDemandRepository = hotelDemandRepository;
            } 
            [PerformanceAspect(5)]
             
            public async Task<IDataResult<IEnumerable<HotelDemandDto>>> Handle(GetHotelDemandsQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run(() => {
                    var hotelDemands = _hotelDemandRepository.GetListAsync(x => x.MainDemandId == request.MainDemandId).GetAwaiter().GetResult();
                    var hotelDemandsDtos = hotelDemands.Select(x => _mapper.Map<HotelDemandDto>(x));
                    return new SuccessDataResult<IEnumerable<HotelDemandDto>>(hotelDemandsDtos);
                }); 
            } 
        }
    }
}
