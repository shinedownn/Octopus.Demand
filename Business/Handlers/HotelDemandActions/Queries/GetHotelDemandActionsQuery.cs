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
    public class GetHotelDemandActionsQuery : IRequest<IDataResult<IEnumerable<HotelDemandActionDto>>>
    {
        public int HotelDemandId { get; set; }
        public class GetHotelDemandActionsQueryHandler : IRequestHandler<GetHotelDemandActionsQuery, IDataResult<IEnumerable<HotelDemandActionDto>>>
        {
            private readonly IHotelDemandActionRepository _HotelDemandActionRepository;
            private readonly IMapper _mapper;
            public GetHotelDemandActionsQueryHandler(IHotelDemandActionRepository HotelDemandActionRepository, IMapper mapper)
            {
                _HotelDemandActionRepository = HotelDemandActionRepository;
                _mapper = mapper;
            } 
            public async Task<IDataResult<IEnumerable<HotelDemandActionDto>>> Handle(GetHotelDemandActionsQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run<IDataResult<IEnumerable<HotelDemandActionDto>>>(() => {
                    var HotelDemandActions = _HotelDemandActionRepository.GetListAsync(x => x.HotelDemandId == request.HotelDemandId).GetAwaiter().GetResult();
                    var HotelDemandActionDtos = HotelDemandActions.Select(x => _mapper.Map<HotelDemandActionDto>(x));
                    return new SuccessDataResult<IEnumerable<HotelDemandActionDto>>(HotelDemandActionDtos);
                }); 
            }
        }
    }
}
