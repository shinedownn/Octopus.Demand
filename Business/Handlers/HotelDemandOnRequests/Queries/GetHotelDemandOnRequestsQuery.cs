using AutoMapper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.HotelDemandOnRequests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.HotelDemandOnRequests.Queries
{
    public class GetHotelDemandOnRequestsQuery : IRequest<IDataResult<IEnumerable<HotelDemandOnRequestDto>>>
    {
        public int HotelDemandId { get; set; }

        public class GetHotelDemandOnRequestsQueryHandler : IRequestHandler<GetHotelDemandOnRequestsQuery, IDataResult<IEnumerable<HotelDemandOnRequestDto>>>
        {
            private readonly IHotelDemandOnRequestRepository hotelDemandOnRequestRepository;
            private readonly IMapper mapper;
            public GetHotelDemandOnRequestsQueryHandler(IHotelDemandOnRequestRepository hotelDemandOnRequestRepository, IMapper mapper)
            {
                this.hotelDemandOnRequestRepository = hotelDemandOnRequestRepository;
                this.mapper = mapper;
            }
            public async Task<IDataResult<IEnumerable<HotelDemandOnRequestDto>>> Handle(GetHotelDemandOnRequestsQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run(() => {
                    var requests = hotelDemandOnRequestRepository.GetListAsync(x => x.HotelDemandId == request.HotelDemandId).GetAwaiter().GetResult();
                    var requestsDtos = mapper.Map<List<HotelDemandOnRequestDto>>(requests);
                    return new SuccessDataResult<IEnumerable<HotelDemandOnRequestDto>>(requestsDtos);
                }); 
            }
        }
    }
}
