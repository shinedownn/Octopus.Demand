using AutoMapper;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.RequestChannels.Commands
{
    public class DeleteRequestChannelCommand : IRequest<IResult>
    {
        public int RequestChannelId { get; set; }

        public class DeleteRequestChannelCommandHandler : IRequestHandler<DeleteRequestChannelCommand, IResult>
        {
            private readonly IRequestChannelRepository _requestChannelRepository;
            private readonly IMapper _mapper;
            public DeleteRequestChannelCommandHandler(IRequestChannelRepository requestChannelRepository, IMapper mapper)
            {
                _requestChannelRepository = requestChannelRepository;
                _mapper = mapper;
            }
            public async Task<IResult> Handle(DeleteRequestChannelCommand request, CancellationToken cancellationToken)
            {
                var requestChannel = await _requestChannelRepository.GetAsync(x => x.RequestChannelId == request.RequestChannelId);
                _requestChannelRepository.Delete(requestChannel);
                await _requestChannelRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
