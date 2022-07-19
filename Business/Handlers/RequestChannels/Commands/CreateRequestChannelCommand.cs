using AutoMapper;
using Business.Constants;
using Business.Helpers;
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

namespace Business.Handlers.RequestChannels.Commands
{
    public class CreateRequestChannelCommand : IRequest<IResult>
    { 
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public int DepartmentId { get; set; }
        public string CreatedUserName { get; set; }
        public DateTime CreateDate { get; set; }
        public class CreateRequestChannelCommandHandler : IRequestHandler<CreateRequestChannelCommand, IResult>
{
            private readonly IRequestChannelRepository _requestChannelRepository;
            private readonly IMapper _mapper;
            public CreateRequestChannelCommandHandler(IRequestChannelRepository requestChannelRepository, IMapper mapper)
            {
                _requestChannelRepository = requestChannelRepository;
                _mapper = mapper;
            }
            public async Task<IResult> Handle(CreateRequestChannelCommand request, CancellationToken cancellationToken)
            {
                var requestChannel = new RequestChannel() { 
                 Name=request.Name,
                 DepartmentId=request.DepartmentId,
                 IsDeleted=request.IsDeleted,
                 CreatedUserName=JwtHelper.GetValue("name").ToString(),
                 CreateDate=DateTime.Now  
                };

                _requestChannelRepository.Add(requestChannel);
                await _requestChannelRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
