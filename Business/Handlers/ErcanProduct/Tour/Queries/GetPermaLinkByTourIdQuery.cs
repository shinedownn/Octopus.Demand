using AutoMapper;
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
    public class GetPermaLinkByTourIdQuery : IRequest<IDataResult<string>>
    {
        public int TourId { get; set; }

        public class GetPermaLinkByTourIdQueryHandler : IRequestHandler<GetPermaLinkByTourIdQuery, IDataResult<string>>
        {
            private readonly ITourPermaLinkRepository _tourPermaLinkRepository;
            private readonly IMapper _mapper;
            public GetPermaLinkByTourIdQueryHandler(ITourPermaLinkRepository tourPermaLinkRepository, IMapper mapper)
            {
                _tourPermaLinkRepository = tourPermaLinkRepository;
                _mapper = mapper;
            }
            public async Task<IDataResult<string>> Handle(GetPermaLinkByTourIdQuery request, CancellationToken cancellationToken)
            {
                var tourPermaLink = await _tourPermaLinkRepository.GetAsync(x => x.TourId == request.TourId);
                return new SuccessDataResult<string>(data: tourPermaLink.Link);
            }
        }
    }
}
