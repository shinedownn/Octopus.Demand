using AutoMapper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos.TourDemandOnRequests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.TourDemandOnRequests.Queries
{
    public class GetTourDemandOnRequestsQuery : IRequest<IDataResult<IEnumerable<TourDemandOnRequestDto>>>
    {
        public int TourDemandId { get; set; }

        public class GetTourDemandOnRequestsQueryHandler : IRequestHandler<GetTourDemandOnRequestsQuery, IDataResult<IEnumerable<TourDemandOnRequestDto>>>
        {
            private readonly ITourDemandOnRequestRepository tourDemandOnRequestRepository;
            private readonly IMapper mapper;
            public GetTourDemandOnRequestsQueryHandler(ITourDemandOnRequestRepository tourDemandOnRequestRepository, IMapper mapper)
            {
                this.tourDemandOnRequestRepository = tourDemandOnRequestRepository;
                this.mapper = mapper;
            }
            public async Task<IDataResult<IEnumerable<TourDemandOnRequestDto>>> Handle(GetTourDemandOnRequestsQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run<IDataResult<IEnumerable<TourDemandOnRequestDto>>>(() => {
                    var onRequests = tourDemandOnRequestRepository.GetListAsync(x => x.TourDemandId == request.TourDemandId).GetAwaiter().GetResult();
                    var Dtos = mapper.Map<IEnumerable<TourDemandOnRequestDto>>(onRequests);
                    return new SuccessDataResult<IEnumerable<TourDemandOnRequestDto>>(Dtos);
                }); 
            }
        }
    }
}
