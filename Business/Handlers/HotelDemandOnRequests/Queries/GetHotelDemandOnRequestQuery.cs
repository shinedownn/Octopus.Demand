using AutoMapper;
using Business.Constants;
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
    public class GetHotelDemandOnRequestQuery : IRequest<IDataResult<HotelDemandOnRequestDto>>
    {
        public int HotelDemandOnRequestId { get; set; }

        public class GetHotelDemandOnRequestQueryHandler : IRequestHandler<GetHotelDemandOnRequestQuery, IDataResult<HotelDemandOnRequestDto>>
        {
            private readonly IHotelDemandOnRequestRepository hotelDemandOnRequestRepository;
            private readonly IMapper mapper;
            public GetHotelDemandOnRequestQueryHandler(IHotelDemandOnRequestRepository hotelDemandOnRequestRepository, IMapper mapper)
            {
                this.hotelDemandOnRequestRepository = hotelDemandOnRequestRepository;
                this.mapper = mapper;
            }
            public async Task<IDataResult<HotelDemandOnRequestDto>> Handle(GetHotelDemandOnRequestQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run<IDataResult<HotelDemandOnRequestDto>>(() => {
                    var onRequest = hotelDemandOnRequestRepository.GetAsync(x => x.HotelDemandOnRequestId == request.HotelDemandOnRequestId).GetAwaiter().GetResult();
                    if (onRequest == null) return new ErrorDataResult<HotelDemandOnRequestDto>(Messages.RecordNotFound);
                    var onRequestDto = mapper.Map<HotelDemandOnRequestDto>(onRequest);
                    return new SuccessDataResult<HotelDemandOnRequestDto>(onRequestDto);
                }); 
            }
        }
    }
}
