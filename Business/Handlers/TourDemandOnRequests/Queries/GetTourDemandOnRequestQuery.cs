using AutoMapper;
using Business.Constants;
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
    public class GetTourDemandOnRequestQuery : IRequest<IDataResult<TourDemandOnRequestDto>>
    {
        public int TourDemandOnRequestId { get; set; }

        public class GetTourDemandOnRequestQueryHandler : IRequestHandler<GetTourDemandOnRequestQuery, IDataResult<TourDemandOnRequestDto>>
        {
            private readonly ITourDemandOnRequestRepository tourDemandOnRequestRepository;
            private readonly IMapper mapper;
            public GetTourDemandOnRequestQueryHandler(ITourDemandOnRequestRepository tourDemandOnRequestRepository, IMapper mapper)
            {
                this.tourDemandOnRequestRepository = tourDemandOnRequestRepository;
                this.mapper = mapper;
            }

            public async Task<IDataResult<TourDemandOnRequestDto>> Handle(GetTourDemandOnRequestQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run<IDataResult<TourDemandOnRequestDto>>(() => {
                    var onRequest = tourDemandOnRequestRepository.GetAsync(x => x.TourDemandOnRequestId == request.TourDemandOnRequestId).GetAwaiter().GetResult();
                    if (onRequest == null) return new ErrorDataResult<TourDemandOnRequestDto>(Messages.RecordNotFound);
                    var onRequestDto = mapper.Map<TourDemandOnRequestDto>(onRequest);
                    return new SuccessDataResult<TourDemandOnRequestDto>(onRequestDto);
                }); 
            }
        }
    }
}
