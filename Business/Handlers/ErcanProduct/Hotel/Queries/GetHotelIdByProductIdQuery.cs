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
    public class GetHotelIdByProductIdQuery : IRequest<IDataResult<Tuple<int,string>>>
    {
        public int? ProductId { get; set; }

        public class GetHotelIdByProductIdQueryHandler : IRequestHandler<GetHotelIdByProductIdQuery, IDataResult<Tuple<int, string>>>
        {
            private readonly IHotelRepository _hotelRepository;
            private readonly IHotelPermaLinkRepository _hotelPermaLinkRepository;
            private readonly IMediator _mediator;
            public GetHotelIdByProductIdQueryHandler(IHotelRepository hotelRepository, IHotelPermaLinkRepository hotelPermaLinkRepository, IMediator mediator)
            {
                _hotelRepository = hotelRepository;
                _mediator = mediator;
                _hotelPermaLinkRepository = hotelPermaLinkRepository;
            }
            public async Task<IDataResult<Tuple<int, string>>> Handle(GetHotelIdByProductIdQuery request, CancellationToken cancellationToken)
            { 
                var hotel = await _hotelRepository.GetAsync(x => x.ProductId == request.ProductId);
                    if (hotel != null)
                {
                    var hotelpermaLink = await _hotelPermaLinkRepository.GetAsync(x => x.HotelId == hotel.HotelId);
                    return new SuccessDataResult<Tuple<int, string>>(new Tuple<int, string>(hotel.HotelId, hotelpermaLink.Link));
                } 

                return new ErrorDataResult<Tuple<int, string>>(Messages.HotelNotFound);
            }
        }
    }
}
