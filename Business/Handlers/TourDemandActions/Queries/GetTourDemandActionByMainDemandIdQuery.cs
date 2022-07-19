using AutoMapper;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;
using Entities.TourDemandActions.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.TourDemandActions.Queries
{
    public class GetTourDemandActionByTourDemandIdQuery : IRequest<IDataResult<IEnumerable<TourDemandActionDto>>>
    {
        public int TourDemandId { get; set; }

        public class GetTourDemandActionByTourDemandIdQueryHandler : IRequestHandler<GetTourDemandActionByTourDemandIdQuery, IDataResult<IEnumerable<TourDemandActionDto>>>
        {
            private readonly ITourDemandActionRepository _TourDemandActionRepository;
            private readonly IMapper _mapper;
            public GetTourDemandActionByTourDemandIdQueryHandler(ITourDemandActionRepository TourDemandActionRepository, IMapper mapper)
            {
                _TourDemandActionRepository = TourDemandActionRepository;
                _mapper = mapper;
            } 
            public async Task<IDataResult<IEnumerable<TourDemandActionDto>>> Handle(GetTourDemandActionByTourDemandIdQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run<IDataResult<IEnumerable<TourDemandActionDto>>>(() => {
                    var TourDemandActions = _TourDemandActionRepository.GetListAsync(x => x.TourDemandId == request.TourDemandId).GetAwaiter().GetResult();
                    var TourDemandActionDtos = TourDemandActions.Select(x => _mapper.Map<TourDemandActionDto>(x));
                    return new SuccessDataResult<IEnumerable<TourDemandActionDto>>(TourDemandActionDtos);
                }); 
            }
        }
    }
}
