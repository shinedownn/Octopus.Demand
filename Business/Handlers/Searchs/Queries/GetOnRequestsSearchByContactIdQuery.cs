using AutoMapper;
using Business.Handlers.Searchs.ValidationRules;
using Business.Helpers;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Searchs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Searchs.Queries
{
    public class GetOnRequestsSearchByContactIdQuery : IRequest<IDataResult<SearchOnRequestsDto>>
    {
        public int ContactId { get; set; }
        public int? MainDemandId { get; set; }
        public string OnRequestName { get; set; } = "";
        public bool IsOpen { get; set; } = true;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool Asc { get; set; } = false;
        public string DemandChannel { get; set; }
        public string ProductName { get; set; }
        public class GetOnRequestsSearchByContactIdHandler : IRequestHandler<GetOnRequestsSearchByContactIdQuery, IDataResult<SearchOnRequestsDto>>
        {
            private readonly IMainDemandRepository _mainDemandRepository;
            private readonly IHotelDemandRepository _hotelDemandRepository;
            private readonly ITourDemandRepository _tourDemandRepository;

            private readonly IHotelDemandOnRequestRepository _hotelDemandOnRequestRepository;
            private readonly ITourDemandOnRequestRepository _tourDemandOnRequestRepository;
            private readonly IOnRequestRepository _onRequestRepository;
            private readonly IMapper _mapper;
            public GetOnRequestsSearchByContactIdHandler(IMainDemandRepository mainDemandRepository, IHotelDemandRepository hotelDemandRepository, ITourDemandRepository tourDemandRepository, IHotelDemandOnRequestRepository hotelDemandOnRequestRepository, ITourDemandOnRequestRepository tourDemandOnRequestRepository, IOnRequestRepository onRequestRepository, IMapper mapper)
            {
                _mainDemandRepository = mainDemandRepository;
                _hotelDemandRepository = hotelDemandRepository;
                _tourDemandRepository = tourDemandRepository;
                _hotelDemandOnRequestRepository = hotelDemandOnRequestRepository;
                _tourDemandOnRequestRepository = tourDemandOnRequestRepository;
                _onRequestRepository = onRequestRepository;
                _mapper = mapper;
            }
            [ValidationAspect(typeof(GetOnRequestsSearchByContactIdQueryValidator))]
            public async Task<IDataResult<SearchOnRequestsDto>> Handle(GetOnRequestsSearchByContactIdQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run(() =>
                {

                    var FullName = JwtHelper.GetValue("name").ToString();
                    var hotelDemandOnRequests = _hotelDemandOnRequestRepository.GetListForPaging(request.CurrentPage, request.PageSize, "CreateDate", request.Asc, x => x.IsOpen == request.IsOpen, new Expression<Func<HotelDemandOnRequest, object>>[0]);
                    var tourDemandOnRequests = _tourDemandOnRequestRepository.GetListForPaging(request.CurrentPage, request.PageSize, "CreateDate", request.Asc, x => x.IsOpen == request.IsOpen, new Expression<Func<TourDemandOnRequest, object>>[0]);
             
                    var onRequests = _onRequestRepository.GetList();

                    if (!string.IsNullOrEmpty(request.OnRequestName))
                    {
                        onRequests = onRequests.Where(x => x.Name.Contains(request.OnRequestName)).ToList();
                    }

                    if (request.MainDemandId != null)
                    {
                        hotelDemandOnRequests.Data = hotelDemandOnRequests.Data.Where(x => x.MainDemandId == request.MainDemandId).ToList();
                        tourDemandOnRequests.Data = tourDemandOnRequests.Data.Where(x => x.MainDemandId == request.MainDemandId).ToList();
                    }
                    if (request.StartDate != null)
                    {
                        hotelDemandOnRequests.Data = hotelDemandOnRequests.Data.Where(x => x.CreateDate >= request.StartDate).ToList();
                        tourDemandOnRequests.Data = tourDemandOnRequests.Data.Where(x => x.CreateDate >= request.StartDate).ToList();
                    }
                    if (request.EndDate != null)
                    {
                        hotelDemandOnRequests.Data = hotelDemandOnRequests.Data.Where(x => x.CreateDate <= request.EndDate).ToList();
                        tourDemandOnRequests.Data = tourDemandOnRequests.Data.Where(x => x.CreateDate <= request.EndDate).ToList();
                    }

                    var hotel = (from hotelonrequest in hotelDemandOnRequests.Data.ToList()
                                 join hoteldemand in _hotelDemandRepository.GetList(x => x.IsOpen) on hotelonrequest.HotelDemandId equals hoteldemand.HotelDemandId
                                 join onrequest in onRequests on hotelonrequest.OnRequestId equals onrequest.OnRequestId
                                 join maindemand in _mainDemandRepository.GetList() on hoteldemand.MainDemandId equals maindemand.MainDemandId
                                 where hotelonrequest.CreatedUserName == FullName 
                                 && maindemand.ContactId==request.ContactId 
                                 && (!string.IsNullOrEmpty(request.DemandChannel) ? maindemand.DemandChannel == request.DemandChannel : maindemand.DemandChannel != string.Empty)
                                 && (!string.IsNullOrEmpty(request.ProductName)? hoteldemand.Name.ToLowerInvariant().Contains(request.ProductName) : hoteldemand.Name!=string.Empty)
                                 select new HotelDemandOnRequestSearchDto
                                 {
                                     MainDemandId = hotelonrequest.MainDemandId,
                                     HotelDemandId = hotelonrequest.HotelDemandId,
                                     HotelOnRequestDescription = hotelonrequest.Description,
                                     Approved = hotelonrequest.Approved,
                                     ConfirmationRequested = hotelonrequest.ConfirmationRequested,
                                     CreateDate = hotelonrequest.CreateDate,
                                     IsOpen = hotelonrequest.IsOpen,
                                     IsDeleted = hotelonrequest.IsDeleted,
                                     AdultCount = hoteldemand.AdultCount,
                                     ChildCount = hoteldemand.ChildCount,
                                     HotelId = hoteldemand.HotelId,
                                     Name = hoteldemand.Name,
                                     Description = hoteldemand.Description,
                                     OnRequestName = onrequest.Name,
                                     CreatedUserName = hotelonrequest.CreatedUserName,
                                     DemandChannel = maindemand.DemandChannel,
                                     HotelDemandOnRequestId = hotelonrequest.HotelDemandOnRequestId,
                                     OnRequestId = hotelonrequest.OnRequestId,
                                     WhoApproves = hotelonrequest.WhoApproves
                                 }).ToList();

                    var tour = (from touronrequest in tourDemandOnRequests.Data.ToList()
                                join tourdemand in _tourDemandRepository.GetList(x => x.IsOpen) on touronrequest.TourDemandId equals tourdemand.TourDemandId
                                join onrequest in onRequests on touronrequest.OnRequestId equals onrequest.OnRequestId
                                join maindemand in _mainDemandRepository.GetList() on tourdemand.MainDemandId equals maindemand.MainDemandId
                                where touronrequest.CreatedUserName == FullName 
                                && maindemand.ContactId==request.ContactId && (!string.IsNullOrEmpty(request.DemandChannel) ? maindemand.DemandChannel == request.DemandChannel : maindemand.DemandChannel != string.Empty)
                                && (!string.IsNullOrEmpty(request.ProductName) ? tourdemand.Name.ToLowerInvariant().Contains(request.ProductName) : tourdemand.Name != string.Empty)
                                select new TourDemandOnRequestSearchDto
                                {
                                    MainDemandId = touronrequest.MainDemandId,
                                    TourDemandId = tourdemand.TourDemandId,
                                    TourOnRequestDescription = touronrequest.Description,
                                    Description = tourdemand.Description,
                                    Approved = touronrequest.Approved,
                                    ConfirmationRequested = touronrequest.ConfirmationRequested,
                                    CreateDate = touronrequest.CreateDate,
                                    IsOpen = touronrequest.IsOpen,
                                    IsDeleted = touronrequest.IsDeleted,
                                    Name = tourdemand.Name,
                                    AdultCount = tourdemand.AdultCount,
                                    ChildCount = tourdemand.ChildCount,
                                    TourId = tourdemand.TourId,
                                    OnRequestName = onrequest.Name,
                                    CreatedUserName = touronrequest.CreatedUserName,
                                    DemandChannel = maindemand.DemandChannel,
                                    TourDemandOnRequestId = touronrequest.TourDemandOnRequestId,
                                    OnRequestId = touronrequest.OnRequestId,
                                    WhoApproves = touronrequest.WhoApproves
                                }).ToList();

                    var search = new SearchOnRequestsDto()
                    {
                        hotelDemandOnRequestSearchDto = new PagingResult<HotelDemandOnRequestSearchDto>(hotel.ToList(), hotel.Count, true, $"{hotel.Count} records listed."),
                        tourDemandOnRequestSearchDto = new PagingResult<TourDemandOnRequestSearchDto>(tour.ToList(), tour.Count, true, $"{tour.Count} records listed."),
                        AllTotalItemCount = hotel.Count + tour.Count
                    };

                    return new SuccessDataResult<SearchOnRequestsDto>(search);
                });
            }
        }
    }
}
