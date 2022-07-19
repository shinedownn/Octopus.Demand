using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;
using Entities.MainDemandActions.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.MainDemandActions.Queries
{
    public class GetMainDemandActionQuery :IRequest<IDataResult<MainDemandActionDto>>
    {
         public int MainDemandActionId { get; set; }

        public class GetMainDemandActionQueryHandler : IRequestHandler<GetMainDemandActionQuery, IDataResult<MainDemandActionDto>>
        {
            private readonly IMainDemandActionRepository _mainDemandActionRepository;
            private readonly IMapper _mapper;
            public GetMainDemandActionQueryHandler(IMainDemandActionRepository mainDemandActionRepository, IMapper mapper)
            {
                _mainDemandActionRepository = mainDemandActionRepository;
                _mapper = mapper;
            } 
            public async Task<IDataResult<MainDemandActionDto>> Handle(GetMainDemandActionQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run<IDataResult<MainDemandActionDto>>(() => {
                    var mainDemandAction = _mainDemandActionRepository.GetAsync(x => x.MainDemandActionId == request.MainDemandActionId).GetAwaiter().GetResult();
                    var mainDemandActionDto = _mapper.Map<MainDemandActionDto>(mainDemandAction);
                    return new SuccessDataResult<MainDemandActionDto>(mainDemandActionDto);
                }); 
            }
        }
    }
}
