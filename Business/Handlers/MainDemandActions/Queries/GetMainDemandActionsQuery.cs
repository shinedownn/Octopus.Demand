using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
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
    public class GetMainDemandActionsQuery : IRequest<IDataResult<IEnumerable<MainDemandActionDto>>>
    {
         public int MainDemandId { get; set; }

        public class GetMainDemandActionByMainDemandIdQueryHandler : IRequestHandler<GetMainDemandActionsQuery, IDataResult<IEnumerable<MainDemandActionDto>>>
        {
            private readonly IMainDemandActionRepository _mainDemandActionRepository;
            private readonly IMapper _mapper;
            public GetMainDemandActionByMainDemandIdQueryHandler(IMainDemandActionRepository mainDemandActionRepository, IMapper mapper)
            {
                _mainDemandActionRepository = mainDemandActionRepository;
                _mapper = mapper;
            }
             
            public async Task<IDataResult<IEnumerable<MainDemandActionDto>>> Handle(GetMainDemandActionsQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run<IDataResult<IEnumerable<MainDemandActionDto>>>(() => {
                    var mainDemandActions = _mainDemandActionRepository.GetListAsync(x => x.MainDemandId == request.MainDemandId).GetAwaiter().GetResult();
                    var mainDemandActionDtos = mainDemandActions.Select(x => _mapper.Map<MainDemandActionDto>(x));
                    return new SuccessDataResult<IEnumerable<MainDemandActionDto>>(mainDemandActionDtos);
                }); 
            }
        }
    }
}
