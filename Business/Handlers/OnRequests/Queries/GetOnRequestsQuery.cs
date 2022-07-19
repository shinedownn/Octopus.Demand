using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.OnRequests.Queries
{
    public class GetOnRequestsQuery : IRequest<IDataResult<IEnumerable<OnRequest>>>
    {
        public class GetOnRequestsQueryHandler : IRequestHandler<GetOnRequestsQuery, IDataResult<IEnumerable<OnRequest>>>
        {
            private readonly IOnRequestRepository onRequestRepository;
            public GetOnRequestsQueryHandler(IOnRequestRepository onRequestRepository)
            {
                this.onRequestRepository = onRequestRepository;
            }
            public async Task<IDataResult<IEnumerable<OnRequest>>> Handle(GetOnRequestsQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run<IDataResult<IEnumerable<OnRequest>>>(() => {
                    var onRequests = onRequestRepository.GetListAsync().GetAwaiter().GetResult();
                    return new SuccessDataResult<IEnumerable<OnRequest>>(onRequests);
                }); 
            }
        }
    }
}
