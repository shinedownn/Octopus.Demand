using AutoMapper;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Demands.Dtos;
using Entities.HotelDemands.Dtos;
using Entities.MainDemands.Dtos;
using Entities.TourDemands.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Demands.Queries
{
    public class GetDemandQuery : IRequest<IDataResult<DemandsDto>>
    {
        public int MainDemandId { get; set; }

        public class GetDemandQueryHandler : IRequestHandler<GetDemandQuery, IDataResult<DemandsDto>>
        {
            private readonly IMainDemandRepository _mainDemandRepository;
            private readonly IMainDemandActionRepository _mainDemandActionRepository;
            private readonly IHotelDemandRepository _hotelDemandRepository;
            private readonly ITourDemandRepository _tourDemandRepository;
            private readonly IHotelDemandActionRepository _hotelDemandActionRepository;
            private readonly IHotelDemandOnRequestRepository _hotelDemandOnRequestRepository;
            private readonly ITourDemandActionRepository _tourDemandActionRepository;
            private readonly ITourDemandOnRequestRepository _tourDemandOnRequestRepository;
            private readonly IActionRepository _actionRepository;
            private readonly IOnRequestRepository _onRequestRepository;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public GetDemandQueryHandler(IMainDemandRepository mainDemandRepository, IHotelDemandRepository hotelDemandRepository, ITourDemandRepository tourDemandRepository, IMapper mapper, IMediator mediator, IHotelDemandActionRepository hotelDemandActionRepository, ITourDemandActionRepository tourDemandActionRepository, IMainDemandActionRepository mainDemandActionRepository, IHotelDemandOnRequestRepository hotelDemandOnRequestRepository, ITourDemandOnRequestRepository tourDemandOnRequestRepository, IActionRepository actionRepository, IOnRequestRepository onRequestRepository)
            {
                _mainDemandRepository = mainDemandRepository;
                _hotelDemandRepository = hotelDemandRepository;
                _tourDemandRepository = tourDemandRepository;
                _mapper = mapper;
                _mediator = mediator;
                _hotelDemandActionRepository = hotelDemandActionRepository;
                _tourDemandActionRepository = tourDemandActionRepository;
                _mainDemandActionRepository = mainDemandActionRepository;
                _hotelDemandOnRequestRepository = hotelDemandOnRequestRepository;
                _tourDemandOnRequestRepository = tourDemandOnRequestRepository;
                _actionRepository = actionRepository;
                _onRequestRepository = onRequestRepository;
            }
             
            //[LogAspect(typeof(PostgreSqlLogger),"GetDemands çekildi")]
            public async Task<IDataResult<DemandsDto>> Handle(GetDemandQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run<IDataResult<DemandsDto>>(() => {
                    var demandDto = new DemandsDto();

                    var mainDemand = _mainDemandRepository.GetAsync(x => x.MainDemandId == request.MainDemandId && !x.IsDeleted).GetAwaiter().GetResult();

                    if (mainDemand == null)
                        return new ErrorDataResult<DemandsDto>(Messages.MainDemandNotFound);

                    demandDto.MainDemandDto = _mapper.Map<MainDemandDto>(mainDemand);

                    demandDto.MainDemandDto.Actions = _mainDemandActionRepository.GetListAsync(x => x.MainDemandId == request.MainDemandId).GetAwaiter().GetResult().Select(x => new {
                        MainDemandActionId = x.MainDemandActionId,
                        ActionId = x.ActionId,
                        Name = _actionRepository.GetAsync(a => a.ActionId == x.ActionId).Result.Name,
                        CreateDate = x.CreateDate,
                        CreatedUserName = x.CreatedUserName,
                        IsOpen = x.IsOpen,
                        Description = x.Description

                    }).ToList<object>();

                    demandDto.HotelDemandDtos = _mapper.Map<List<HotelDemandDto>>(_hotelDemandRepository.GetListAsync(x => x.MainDemandId == request.MainDemandId && !x.IsDeleted).GetAwaiter().GetResult());
                    demandDto.TourDemandDtos = _mapper.Map<List<TourDemandDto>>(_tourDemandRepository.GetListAsync(x => x.MainDemandId == request.MainDemandId && !x.IsDeleted).GetAwaiter().GetResult());

                    var hotelDemandActions = _hotelDemandActionRepository.GetListAsync(x => x.MainDemandId == request.MainDemandId && !x.IsDeleted).GetAwaiter().GetResult();
                    var hotelDemandOnRequests = _hotelDemandOnRequestRepository.GetListAsync(x => x.MainDemandId == request.MainDemandId && !x.IsDeleted).GetAwaiter().GetResult();
                    demandDto.HotelDemandDtos.ForEach(hoteldemanddto =>
                    {
                        hoteldemanddto.Actions = hotelDemandActions.Where(x => x.HotelDemandId == hoteldemanddto.HotelDemandId).Select(x => new {
                            HotelDemandActionId = x.HotelDemandActionId,
                            ActionId = x.ActionId,
                            IsOpen = x.IsOpen,
                            Description = x.Description,
                            Name = _actionRepository.GetAsync(a => a.ActionId == x.ActionId).Result.Name,
                            CreatedUserName = x.CreatedUserName,
                            CreateDate = x.CreateDate
                        }).ToList<object>();
                        hoteldemanddto.OnRequests = hotelDemandOnRequests.Where(x => x.HotelDemandId == hoteldemanddto.HotelDemandId).Select(x => new {
                            HotelDemandOnRequestId = x.HotelDemandOnRequestId,
                            OnRequestId = x.OnRequestId,
                            IsOpen = x.IsOpen,
                            Description = x.Description,
                            Name = _onRequestRepository.GetAsync(a => a.OnRequestId == x.OnRequestId).Result.Name,
                            CreatedUserName = x.CreatedUserName,
                            CreateDate = x.CreateDate,
                            AskingForApprovalDepartmentId= x.AskingForApprovalDepartmentId,  //onay isteyen departman id
                            ConfirmationRequested = x.ConfirmationRequested,         //onay istendi mi?
                            ApprovalRequestedDepartmentId=x.ApprovalRequestedDepartmentId,  // onay istenen departman id
                            Approved=x.Approved,                  // onaylandı mı? 
                            ApprovingDepartmentId=x.ApprovingDepartmentId,          // onaylayan departman id
                            WhoApproves=x.WhoApproves,                 // kim onayladı?
                            ApprovedDate=x.ApprovedDate               // onaylanma tarihi

                        }).ToList<object>();
                        //hoteldemanddto.Actions = _mapper.Map<List<HotelDemandActionDto>>(hotelDemandActions.Where(x => x.HotelDemandId == hoteldemanddto.HotelDemandId).ToList());
                        //hoteldemanddto.OnRequests = _mapper.Map<List<HotelDemandOnRequestDto>>(hotelDemandOnRequests.Where(x => x.HotelDemandId == hoteldemanddto.HotelDemandId)).ToList();
                    });

                    var tourDemandActions = _tourDemandActionRepository.GetListAsync(x => x.MainDemandId == request.MainDemandId && !x.IsDeleted).GetAwaiter().GetResult();
                    var tourDemandOnRequests = _tourDemandOnRequestRepository.GetListAsync(x => x.MainDemandId == request.MainDemandId && !x.IsDeleted).GetAwaiter().GetResult();
                    demandDto.TourDemandDtos.ForEach(tourdemanddto =>
                    {
                        tourdemanddto.Actions = tourDemandActions.Where(x => x.TourDemandId == tourdemanddto.TourDemandId).Select(x => new {
                            TourDemandActionId = x.TourDemandActionId,
                            ActionId = x.ActionId,
                            IsOpen = x.IsOpen,
                            Description = x.Description,
                            Name = _actionRepository.GetAsync(a => a.ActionId == x.ActionId).Result.Name,
                            CreatedUserName = x.CreatedUserName,
                            CreateDate = x.CreateDate
                        }).ToList<object>();
                        tourdemanddto.OnRequests = tourDemandOnRequests.Where(x => x.TourDemandId == tourdemanddto.TourDemandId).Select(x => new {
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
                            ApprovedDate = x.ApprovedDate               // onaylanma tarihi
                        }).ToList<object>();
                    });

                    return new SuccessDataResult<DemandsDto>(demandDto);
                }); 
                
            }
        }
    }
}
