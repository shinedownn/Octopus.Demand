using AutoMapper;
using Business.Helpers;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Actions.Dtos;
using Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Actions.Queries
{
    public class GetActionQuery : IRequest<IDataResult<ActionDto>>
    { 
        public int ActionId { get; set; }
        public class GetActionQueryHandler : IRequestHandler<GetActionQuery, IDataResult<ActionDto>>
        {
            private readonly IActionRepository actionRepository;
            private readonly IMapper mapper;

            public GetActionQueryHandler(IActionRepository actionRepository, IMapper mapper)
            {
                this.actionRepository = actionRepository;
                this.mapper= mapper;
            }
             
            public async Task<IDataResult<ActionDto>> Handle(GetActionQuery request, CancellationToken cancellationToken)
            {
                var action = await actionRepository.GetAsync(x => x.ActionId == request.ActionId);
                var dto = mapper.Map<ActionDto>(action);
                return new SuccessDataResult<ActionDto>(dto);
            }
        }
    }
}
