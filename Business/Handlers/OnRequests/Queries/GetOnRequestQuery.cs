using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.OnRequests.Queries
{
    public class GetOnRequestQuery : IRequest<IDataResult<OnRequest>>
    {
        public int OnRequestId { get; set; }

        public class GetOnRequestQueryHandler : IRequestHandler<GetOnRequestQuery, IDataResult<OnRequest>>
        {
            private readonly IOnRequestRepository onRequestRepository;
            public GetOnRequestQueryHandler(IOnRequestRepository onRequestRepository)
            {
                this.onRequestRepository = onRequestRepository;
            }
            public async Task<IDataResult<OnRequest>> Handle(GetOnRequestQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run<IDataResult<OnRequest>>(() => {
                    var onRequest = onRequestRepository.GetAsync(x => x.OnRequestId == request.OnRequestId).GetAwaiter().GetResult();
                    if (onRequest == null) return new ErrorDataResult<OnRequest>(Messages.RecordNotFound);
                    return new SuccessDataResult<OnRequest>(onRequest);
                }); 
            }
        }
    }
}
