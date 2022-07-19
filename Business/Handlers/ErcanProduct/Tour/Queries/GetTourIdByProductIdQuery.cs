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

namespace Business.Handlers.ErcanProduct.Tour.Queries
{
    public class GetTourIdByProductIdQuery : IRequest<IDataResult<Tuple<int, string>>>
    {
        public int ProductId { get; set; }

        public class GetTourIdByProductIdQueryHandler : IRequestHandler<GetTourIdByProductIdQuery, IDataResult<Tuple<int, string>>>
        {
            private readonly ITourRepository _tourRepository;
            private readonly ITourPermaLinkRepository _tourPermaLinkRepository;
            private readonly IMediator _mediator;
            public GetTourIdByProductIdQueryHandler(ITourRepository tourRepository, ITourPermaLinkRepository tourPermaLinkRepository, IMediator mediator)
            {
                _tourRepository = tourRepository;
                _mediator = mediator;
                _tourPermaLinkRepository = tourPermaLinkRepository;
            }
            public async Task<IDataResult<Tuple<int, string>>> Handle(GetTourIdByProductIdQuery request, CancellationToken cancellationToken)
            {
                var tour = _tourRepository.GetAsync(x => x.ProductId == request.ProductId).GetAwaiter().GetResult();
                if (tour != null)
                {
                    var tourpermaLink = _tourPermaLinkRepository.GetAsync(x => x.TourId == tour.TourId).GetAwaiter().GetResult();
                    return new SuccessDataResult<Tuple<int, string>>(new Tuple<int, string>(tour.TourId, tourpermaLink.Link));
                }

                return new ErrorDataResult<Tuple<int, string>>(Messages.HotelNotFound);
            }
        }
    }
}
