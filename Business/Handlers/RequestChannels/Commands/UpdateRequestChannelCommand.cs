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
    public class UpdateRequestChannelCommand : IRequest<IResult>
    {
        public int RequestChannelId { get; set; }
        public int DepartmentId { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }

        public class UpdateRequestChannelCommandHandler : IRequestHandler<UpdateRequestChannelCommand, IResult>
        {
            private readonly IRequestChannelRepository _requestChannelRepository;
            private readonly IMapper _mapper;
            public UpdateRequestChannelCommandHandler(IRequestChannelRepository requestChannelRepository, IMapper mapper)
            {
                _requestChannelRepository = requestChannelRepository;
                _mapper = mapper;
            }
            public async Task<IResult> Handle(UpdateRequestChannelCommand request, CancellationToken cancellationToken)
            {
                var requestChannel = await _requestChannelRepository.GetAsync(x=>x.RequestChannelId==request.RequestChannelId);
                requestChannel.DepartmentId = request.DepartmentId;
                requestChannel.IsDeleted = request.IsDeleted;
                requestChannel.Name = request.Name;
                _requestChannelRepository.Update(requestChannel);
                await _requestChannelRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
