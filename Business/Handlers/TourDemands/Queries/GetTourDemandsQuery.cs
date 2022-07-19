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
using Entities.TourDemands.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.TourDemands.Queries
{
    public class GetTourDemandsQuery : IRequest<IDataResult<IEnumerable<TourDemandDto>>>
    {
         public int MainDemandId { get; set; }
        public class GetTourDemandsQueryHandler : IRequestHandler<GetTourDemandsQuery, IDataResult<IEnumerable<TourDemandDto>>>
        {
            private readonly ITourDemandRepository _tourDemandRepository;
            private readonly IMapper _mapper;
            public GetTourDemandsQueryHandler(ITourDemandRepository tourDemandRepository, IMapper mapper)
            {
                _tourDemandRepository = tourDemandRepository;
                _mapper = mapper;
            }
             
            public async Task<IDataResult<IEnumerable<TourDemandDto>>> Handle(GetTourDemandsQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run<IDataResult<IEnumerable<TourDemandDto>>>(() => {
                    var tours = _tourDemandRepository.GetListAsync(x => x.MainDemandId == request.MainDemandId).GetAwaiter().GetResult();
                    var tourDtoList = tours.Select(tour => _mapper.Map<TourDemandDto>(tour)).ToList();
                    return new SuccessDataResult<IEnumerable<TourDemandDto>>(tourDtoList);
                }); 
            }
        }
    }
}
