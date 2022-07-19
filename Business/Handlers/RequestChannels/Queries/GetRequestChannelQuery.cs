using AutoMapper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos.RequestChannel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.RequestChannels.Queries
{
    public class GetRequestChannelQuery : IRequest<IDataResult<IEnumerable<RequestChannelDto>>>
    {
        public class GetRequestChannelQueryHandler : IRequestHandler<GetRequestChannelQuery, IDataResult<IEnumerable<RequestChannelDto>>>
        {
            private readonly IRequestChannelRepository _requestChannelRepository;
            private readonly IMapper _mapper;
            public GetRequestChannelQueryHandler(IRequestChannelRepository requestChannelRepository, IMapper mapper)
            {
                _requestChannelRepository = requestChannelRepository;
                _mapper = mapper;
            }
            public async Task<IDataResult<IEnumerable<RequestChannelDto>>> Handle(GetRequestChannelQuery request, CancellationToken cancellationToken)
            {
                var requestChannels = await _requestChannelRepository.GetListAsync();
                var dtos = _mapper.Map<IEnumerable<RequestChannelDto>>(requestChannels);
                return new SuccessDataResult<IEnumerable<RequestChannelDto>>(dtos);
            }
        }
    }
}
