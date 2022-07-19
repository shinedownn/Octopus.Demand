using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Entities.HotelDemands.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.HotelDemands.Queries
{
    public class GetHotelDemandQuery : IRequest<IDataResult<HotelDemandDto>>
    {
        public int HotelDemandId { get; set; }

        public class GetHotelDemandQueryHandler : IRequestHandler<GetHotelDemandQuery, IDataResult<HotelDemandDto>>
        {
            private readonly IHotelDemandRepository _hotelDemandRepository;
            private readonly IHotelDemandActionRepository _hotelDemandActionRepository;
            private readonly IActionRepository _actionRepository;
            private readonly IHotelDemandOnRequestRepository _hotelDemandOnRequestRepository;
            private readonly IOnRequestRepository _onRequestRepository;
            private readonly IMapper _mapper;

            public GetHotelDemandQueryHandler(IHotelDemandRepository hotelDemandRepository, IMapper mapper, IHotelDemandActionRepository hotelDemandActionRepository, IActionRepository actionRepository, IHotelDemandOnRequestRepository hotelDemandOnRequestRepository, IOnRequestRepository onRequestRepository)
            {
                _hotelDemandRepository = hotelDemandRepository;
                _mapper = mapper;
                _hotelDemandActionRepository = hotelDemandActionRepository;
                _actionRepository = actionRepository;
                _hotelDemandOnRequestRepository = hotelDemandOnRequestRepository;
                _onRequestRepository = onRequestRepository;
            }

            public async Task<IDataResult<HotelDemandDto>> Handle(GetHotelDemandQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run<IDataResult<HotelDemandDto>>(async () =>
                {
                    var hotelDemand = _hotelDemandRepository.GetAsync(x => x.HotelDemandId == request.HotelDemandId).GetAwaiter().GetResult();
                    if (hotelDemand == null)
                        return new ErrorDataResult<HotelDemandDto>(Messages.HotelDemandNotFound);

                    var dto = _mapper.Map<HotelDemandDto>(hotelDemand);

                    dto.Actions = _hotelDemandActionRepository.GetListAsync(x => x.HotelDemandId == request.HotelDemandId).GetAwaiter().GetResult().Select(x => new
                    {
                        HotelDemandActionId = x.HotelDemandActionId,
                        ActionId = x.ActionId,
                        IsOpen = x.IsOpen,
                        Description = x.Description,
                        Name = _actionRepository.GetAsync(a => a.ActionId == x.ActionId).Result.Name,
                        CreatedUserName = x.CreatedUserName,
                        CreateDate = x.CreateDate

                    }).ToList<object>();

                    dto.OnRequests = _hotelDemandOnRequestRepository.GetListAsync(x => x.HotelDemandId == request.HotelDemandId).GetAwaiter().GetResult().Select(x => new
                    {
                        HotelDemandOnRequestId = x.HotelDemandOnRequestId,
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

                    return new SuccessDataResult<HotelDemandDto>(dto);
                });
            }
        }
    }
}
