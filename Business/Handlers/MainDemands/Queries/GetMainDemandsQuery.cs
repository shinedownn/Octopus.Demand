
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;
using AutoMapper;
using Entities.Dtos;
using System.Linq;
using Entities.MainDemands.Dtos;

namespace Business.Handlers.Demands.Queries
{

    public class GetMainDemandsQuery : IRequest<IDataResult<IEnumerable<MainDemandDto>>>
    {
        public bool IsOpen { get; set; }
        public class GetDemandsQueryHandler : IRequestHandler<GetMainDemandsQuery, IDataResult<IEnumerable<MainDemandDto>>>
        {
            private readonly IMainDemandRepository _demandRepository;
            private readonly IMapper _mapper;

            public GetDemandsQueryHandler(IMainDemandRepository demandRepository, IMapper mapper)
            {
                _demandRepository = demandRepository;
                _mapper = mapper;
            }
             
            public async Task<IDataResult<IEnumerable<MainDemandDto>>> Handle(GetMainDemandsQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run<IDataResult<IEnumerable<MainDemandDto>>>(() => {
                    var maindemands = _demandRepository.GetListAsync(x => x.IsOpen == request.IsOpen).GetAwaiter().GetResult();
                    var mainDemandsDtos = maindemands.Select(x => _mapper.Map<MainDemand, MainDemandDto>(x));

                    return new SuccessDataResult<IEnumerable<MainDemandDto>>(mainDemandsDtos);
                });                
            }
        }
    }
}