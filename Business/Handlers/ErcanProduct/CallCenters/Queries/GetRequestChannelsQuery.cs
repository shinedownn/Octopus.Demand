using AutoMapper;
using Core.Utilities.Results;
using DataAccess.Abstract.ErcanProduct;
using Entities.ErcanProduct.Concrete.CallCenter;
using Entities.ErcanProduct.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ErcanProduct.CallCenters.Queries
{
    public class GetRequestChannelsQuery : IRequest<IDataResult<IEnumerable<RequestChannelDto>>>
    {
        public class GetRequestChannelsQueryHandler : IRequestHandler<GetRequestChannelsQuery, IDataResult<IEnumerable<RequestChannelDto>>>
        {
            private readonly IRequestChannelRepository _requestChannelRepository;
            private readonly IMapper _mapper;
            public GetRequestChannelsQueryHandler(IRequestChannelRepository requestChannelRepository, IMapper mapper)
            {
                _requestChannelRepository = requestChannelRepository;
                _mapper = mapper;
            }
            public async Task<IDataResult<IEnumerable<RequestChannelDto>>> Handle(GetRequestChannelsQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run(() => {
                    var requestChannels = _requestChannelRepository.GetListAsync(x => x.IsDeleted == false).GetAwaiter().GetResult();
                    var dtos=_mapper.Map<IEnumerable<RequestChannelDto>>(requestChannels);
                    return new SuccessDataResult<IEnumerable<RequestChannelDto>>(dtos);
                });
            }
        }
    }
}
