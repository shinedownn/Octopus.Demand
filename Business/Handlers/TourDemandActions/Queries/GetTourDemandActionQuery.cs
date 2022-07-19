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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.TourDemandActions.Queries
{
    public class GetTourDemandActionQuery : IRequest<IDataResult<TourDemandActionDto>>
    {
         public int TourDemandActionId { get; set; }

        public class GetTourDemandActionQueryHandler : IRequestHandler<GetTourDemandActionQuery, IDataResult<TourDemandActionDto>>
        {
            private readonly ITourDemandActionRepository _TourDemandActionRepository;
            private readonly IMapper _mapper;
            public GetTourDemandActionQueryHandler(ITourDemandActionRepository TourDemandActionRepository, IMapper mapper)
            {
                _TourDemandActionRepository = TourDemandActionRepository;
                _mapper = mapper;
            }
             
            public async Task<IDataResult<TourDemandActionDto>> Handle(GetTourDemandActionQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run<IDataResult<TourDemandActionDto>>(() => {
                    var TourDemandAction = _TourDemandActionRepository.GetAsync(x => x.TourDemandActionId == request.TourDemandActionId).GetAwaiter().GetResult();
                    var TourDemandActionDto = _mapper.Map<TourDemandActionDto>(TourDemandAction);
                    return new SuccessDataResult<TourDemandActionDto>(TourDemandActionDto);
                }); 
            }
        }
    }
}
