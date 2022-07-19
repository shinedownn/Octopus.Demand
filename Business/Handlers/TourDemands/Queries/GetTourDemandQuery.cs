using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Entities.TourDemands.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.TourDemands.Queries
{
    public class GetTourDemandQuery:IRequest<IDataResult<TourDemandDto>>
    {
         public int TourDemandId { get; set; }

        public class GetTourDemandQueryHandler : IRequestHandler<GetTourDemandQuery, IDataResult<TourDemandDto>>
        {
            private readonly ITourDemandRepository _tourDemandRepository;
            private readonly ITourDemandActionRepository _tourDemandActionRepository;
            private readonly IActionRepository _actionRepository;
            private readonly ITourDemandOnRequestRepository _tourDemandOnRequestRepository;
            private readonly IOnRequestRepository _onRequestRepository;
            private readonly IMapper _mapper;
            public GetTourDemandQueryHandler(ITourDemandRepository tourDemandRepository, IMapper mapper, ITourDemandActionRepository tourDemandActionRepository, IActionRepository actionRepository, ITourDemandOnRequestRepository tourDemandOnRequestRepository, IOnRequestRepository onRequestRepository)
            {
                _tourDemandRepository = tourDemandRepository;
                _mapper = mapper;
                _tourDemandActionRepository = tourDemandActionRepository;
                _actionRepository = actionRepository;
                _tourDemandOnRequestRepository = tourDemandOnRequestRepository;
                _onRequestRepository = onRequestRepository;
            }

            public async Task<IDataResult<TourDemandDto>> Handle(GetTourDemandQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run<IDataResult<TourDemandDto>>(() => {
                    var tourDemand = _tourDemandRepository.GetAsync(x => x.TourDemandId == request.TourDemandId).GetAwaiter().GetResult();
                    var tourDemandDto = _mapper.Map<TourDemandDto>(tourDemand);

                    if (tourDemandDto == null)
                        return new ErrorDataResult<TourDemandDto>(Messages.TourDemandNotFound);

                    tourDemandDto.Actions = _tourDemandActionRepository.GetListAsync(x => x.TourDemandId == request.TourDemandId).GetAwaiter().GetResult().Select(x => new
                    {
                        TourDemandActionId = x.TourDemandActionId,
                        ActionId = x.ActionId,
                        IsOpen = x.IsOpen,
                        Description = x.Description,
                        Name = _actionRepository.GetAsync(a => a.ActionId == x.ActionId).Result.Name,
                        CreatedUserName = x.CreatedUserName,
                        CreateDate = x.CreateDate

                    }).ToList<object>();

                    tourDemandDto.OnRequests = _tourDemandOnRequestRepository.GetListAsync(x => x.TourDemandId == request.TourDemandId).GetAwaiter().GetResult().Select(x => new
                    {
                        TourDemandOnRequestId = x.TourDemandOnRequestId,
                        OnRequestId = x.OnRequestId,
                        IsOpen = x.IsOpen,
                        Description = x.Description,
                        Name = _onRequestRepository.GetAsync(a => a.OnRequestId == x.OnRequestId).Result.Name,
                        CreatedUserName = x.CreatedUserName,
                        CreateDate = x.CreateDate,
                        AskingForApprovalDepartmentId = x.AskingForApprovalDepartmentId,  //onay isteyen departman id
                        ConfirmationRequested = x.ConfirmationRequested,         //onay istendi mi?
                        ApprovalRequestedDepartmentId = x.ApprovalRequestedDepartmentId,  // onay istenen departman id
                        Approved = x.Approved,                  // onaylandı mı? 
                        ApprovingDepartmentId = x.ApprovingDepartmentId,          // onaylayan departman id
                        WhoApproves = x.WhoApproves,                 // kim onayladı?
                        ApprovedDate = x.ApprovedDate
                    }).ToList<object>(); 

                    return new SuccessDataResult<TourDemandDto>(tourDemandDto);
                }); 
            }
        }
    }
}
