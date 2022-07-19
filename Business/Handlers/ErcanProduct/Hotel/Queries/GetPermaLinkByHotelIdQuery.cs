using AutoMapper;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract.ErcanProduct;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ErcanProduct.Hotel.Queries
{
    public class GetPermaLinkByHotelIdQuery : IRequest<IDataResult<string>>
    {
        public int HotelId { get; set; }

        public class GetPermaLinkByHotelIdQueryHandler : IRequestHandler<GetPermaLinkByHotelIdQuery, IDataResult<string>>
        {
            private readonly IHotelPermaLinkRepository _hotelPermaLinkRepository;
            private readonly IMapper _mapper;
            public GetPermaLinkByHotelIdQueryHandler(IHotelPermaLinkRepository hotelPermaLinkRepository, IMapper mapper)
            {
                _hotelPermaLinkRepository = hotelPermaLinkRepository;
                _mapper = mapper;
            }
            public async Task<IDataResult<string>> Handle(GetPermaLinkByHotelIdQuery request, CancellationToken cancellationToken)
            { 
                var hotelpermaLink = await _hotelPermaLinkRepository.GetAsync(x => x.HotelId == request.HotelId);
                return new SuccessDataResult<string>(data:hotelpermaLink.Link); 
            }
        }
    }
}
