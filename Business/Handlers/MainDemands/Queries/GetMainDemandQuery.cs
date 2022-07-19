
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using AutoMapper;
using Entities.Dtos;
using Core.Aspects.Autofac.Caching;
using System.ComponentModel.DataAnnotations;
using Business.Constants;
using Entities.MainDemands.Dtos;

namespace Business.Handlers.Demands.Queries
{
    public class GetMainDemandQuery : IRequest<IDataResult<MainDemandDto>>
    {
         public int MainDemandId { get; set; }

        public class GetMainDemandQueryHandler : IRequestHandler<GetMainDemandQuery, IDataResult<MainDemandDto>>
        {
            private readonly IMainDemandRepository _demandRepository;
            private readonly IMapper _mapper;

            public GetMainDemandQueryHandler(IMainDemandRepository demandRepository, IMapper mapper)
            {
                _demandRepository = demandRepository;
                _mapper = mapper;
            } 
            public async Task<IDataResult<MainDemandDto>> Handle(GetMainDemandQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run<IDataResult<MainDemandDto>>(() => {
                    var demand = _demandRepository.GetAsync(p => p.MainDemandId == request.MainDemandId).GetAwaiter().GetResult();
                    if (demand == null) return new ErrorDataResult<MainDemandDto>(Messages.RecordNotFound);
                    var demandDto = _mapper.Map<MainDemandDto>(demand);
                    return new SuccessDataResult<MainDemandDto>(demandDto);
                }); 
            }
        }
    }
}
