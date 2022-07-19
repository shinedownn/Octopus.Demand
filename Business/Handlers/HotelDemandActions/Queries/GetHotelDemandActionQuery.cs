using AutoMapper;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;
using Entities.HotelDemandActions.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.HotelDemandActions.Queries
{
    public class GetHotelDemandActionQuery : IRequest<IDataResult<HotelDemandActionDto>>
    {
         public int HotelDemandActionId { get; set; }

        public class GetHotelDemandActionQueryHandler : IRequestHandler<GetHotelDemandActionQuery, IDataResult<HotelDemandActionDto>>
        {
            private readonly IHotelDemandActionRepository _HotelDemandActionRepository;
            private readonly IMapper _mapper;
            public GetHotelDemandActionQueryHandler(IHotelDemandActionRepository HotelDemandActionRepository, IMapper mapper)
            {
                _HotelDemandActionRepository = HotelDemandActionRepository;
                _mapper = mapper;
            }  
            public async Task<IDataResult<HotelDemandActionDto>> Handle(GetHotelDemandActionQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run<IDataResult<HotelDemandActionDto>>(() => {
                    var HotelDemandAction = _HotelDemandActionRepository.GetAsync(x => x.HotelDemandActionId == request.HotelDemandActionId).GetAwaiter().GetResult();
                    var HotelDemandActionDto = _mapper.Map<HotelDemandActionDto>(HotelDemandAction);
                    return new SuccessDataResult<HotelDemandActionDto>(HotelDemandActionDto);
                }); 
            }
        }
    }
}
