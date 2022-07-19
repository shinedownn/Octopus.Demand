using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.Demands.ValidationRules;
using Business.Helpers;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Entities.HotelDemands.Dtos;
using Entities.MainDemands.Dtos;
using Entities.TourDemands.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Demands.Commands
{
    public class UpdateDemandCommand : IRequest<IResult>
    {
        public MainDemandUpdateDto MainDemandDto { get; set; }
        public List<HotelDemandUpdateDto> HotelDemandDtos { get; set; }
        public List<TourDemandUpdateDto> TourDemandDtos { get; set; }
        //public bool IsFirm { get; set; }
        //public string FirmName { get; set; }
        //public string FirmTitle { get; set; }
        //public string AreaCode { get; set; }
        //public string CountryCode { get; set; }

        public class UpdateDemandCommandHandler : IRequestHandler<UpdateDemandCommand, IResult>
        {
            private readonly IMainDemandRepository _mainDemandRepository;
            private readonly IHotelDemandRepository _hotelDemandRepository;
            private readonly ITourDemandRepository _tourDemandRepository;
            private readonly IHotelDemandOnRequestRepository _hotelOnRequestRepository;
            private readonly ITourDemandOnRequestRepository _tourDemandOnRequestRepository;
            private readonly IMapper _mapper;
            public UpdateDemandCommandHandler(IMainDemandRepository mainDemandRepository, IHotelDemandRepository hotelDemandRepository, ITourDemandRepository tourDemandRepository, IMapper mapper, IHotelDemandOnRequestRepository hotelOnRequestRepository, ITourDemandOnRequestRepository tourDemandOnRequestRepository)
            {
                _mainDemandRepository = mainDemandRepository;
                _hotelDemandRepository = hotelDemandRepository;
                _tourDemandRepository = tourDemandRepository;
                _mapper = mapper;
                _hotelOnRequestRepository = hotelOnRequestRepository;
                _tourDemandOnRequestRepository = tourDemandOnRequestRepository;
            }

            [ValidationAspect(typeof(UpdateDemandValidator), Priority = 1)]
            [TransactionScopeAspectAsync(Priority = 2)]
            [LogAspect(typeof(PostgreSqlLogger), "Talep düzenlendi", Priority = 3)]
            public async Task<IResult> Handle(UpdateDemandCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() =>
                {
                    var mainDemand = _mainDemandRepository.GetAsync(x => x.MainDemandId == request.MainDemandDto.MainDemandId).GetAwaiter().GetResult();
                    if (mainDemand == null) return new ErrorResult(Messages.RecordNotFound);

                    mainDemand.Surname = request.MainDemandDto?.Surname;
                    mainDemand.Description = request.MainDemandDto?.Description;
                    mainDemand.Email = request.MainDemandDto?.Email;
                    mainDemand.IsOpen = request.MainDemandDto.IsOpen;
                    mainDemand.PhoneNumber = request.MainDemandDto?.PhoneNumber;
                    mainDemand.DemandChannel = request.MainDemandDto?.DemandChannel;
                    mainDemand.IsFirm = request.MainDemandDto.IsFirm;
                    mainDemand.FirmName = request.MainDemandDto?.FirmName;
                    mainDemand.FirmTitle = request.MainDemandDto?.FirmTitle;
                    mainDemand.AreaCode = request.MainDemandDto?.AreaCode;
                    mainDemand.CountryCode = request.MainDemandDto?.CountryCode;
                    mainDemand.ReservationNumber = request.MainDemandDto?.ReservationNumber;
                    mainDemand.FullPhoneNumber = (request.MainDemandDto.CountryCode + request.MainDemandDto.AreaCode + request.MainDemandDto.PhoneNumber).Replace(" ", "");
                    _mainDemandRepository.Update(mainDemand);
                    request.HotelDemandDtos.ForEach(hotelDemandDto =>
                    {
                        var hoteldemand = _hotelDemandRepository.GetAsync(hoteldemand => hoteldemand.HotelDemandId == hotelDemandDto.HotelDemandId).GetAwaiter().GetResult();
                        hoteldemand.AdultCount = hotelDemandDto.AdultCount;
                        hoteldemand.Description = hotelDemandDto.Description;
                        hoteldemand.CheckIn =   hotelDemandDto.CheckIn.Date.ToUniversalTime();
                        hoteldemand.CheckOut = hotelDemandDto.CheckOut.ToUniversalTime();
                        hoteldemand.ChildCount = hotelDemandDto.ChildCount;
                        hoteldemand.TotalCount = hotelDemandDto.AdultCount + hotelDemandDto.ChildCount;
                        hoteldemand.IsOpen = hotelDemandDto.IsOpen;
                        hoteldemand.Name = hotelDemandDto.Name;
                        _hotelDemandRepository.Update(hoteldemand);

                        var hotelrequests = _hotelOnRequestRepository.GetListAsync(request => request.HotelDemandId == hotelDemandDto.HotelDemandId).GetAwaiter().GetResult(); ;
                        hotelrequests.ToList().ForEach(hotelrequest =>
                        {
                            hotelrequest.OnRequestId = hotelDemandDto.Requests.Where(x => x.HotelDemandOnRequestId == hotelrequest.HotelDemandOnRequestId).FirstOrDefault().OnRequestId;
                            hotelrequest.Description = hotelDemandDto.Requests.Where(x => x.HotelDemandOnRequestId == hotelrequest.HotelDemandOnRequestId).FirstOrDefault().Description;
                            hotelrequest.IsOpen = hotelDemandDto.Requests.Where(x => x.HotelDemandOnRequestId == hotelrequest.HotelDemandOnRequestId).FirstOrDefault().IsOpen;
                            hotelrequest.Approved = hotelDemandDto.Requests.Where(x => x.HotelDemandOnRequestId == hotelrequest.HotelDemandOnRequestId).FirstOrDefault().Approved;
                            hotelrequest.ConfirmationRequested = hotelDemandDto.Requests.Where(x => x.HotelDemandOnRequestId == hotelrequest.HotelDemandOnRequestId).FirstOrDefault().ConfirmationRequested;
                            hotelrequest.AskingForApprovalDepartmentId = hotelrequest.ConfirmationRequested == true ? Convert.ToInt32(JwtHelper.GetValue("departmentId").ToString()) : null;
                            hotelrequest.ApprovalRequestedDepartmentId = hotelrequest.ApprovalRequestedDepartmentId;
                            _hotelOnRequestRepository.Update(hotelrequest);
                        });
                    });

                    request.TourDemandDtos.ForEach(tourDemandDto =>
                    {
                        var tourdemand = _tourDemandRepository.GetAsync(tourdemand => tourdemand.TourDemandId == tourDemandDto.TourDemandId).GetAwaiter().GetResult(); ;
                        tourdemand.AdultCount = tourDemandDto.AdultCount;
                        tourdemand.Description = tourDemandDto.Description;
                        tourdemand.ChildCount = tourDemandDto.ChildCount;
                        tourdemand.TotalCount = tourDemandDto.AdultCount + tourDemandDto.ChildCount;
                        tourdemand.IsOpen = tourDemandDto.IsOpen;
                        tourdemand.Name = tourDemandDto.Name;

                        _tourDemandRepository.Update(tourdemand);

                        var tourrequests = _tourDemandOnRequestRepository.GetListAsync(request => request.TourDemandId == tourDemandDto.TourDemandId).GetAwaiter().GetResult(); ;
                        tourrequests.ToList().ForEach(tourrequest =>
                        {
                            tourrequest.OnRequestId = tourDemandDto.Requests.FirstOrDefault(x => x.TourDemandOnRequestId == tourrequest.TourDemandOnRequestId).OnRequestId;
                            tourrequest.Description = tourDemandDto.Requests.FirstOrDefault(x => x.TourDemandOnRequestId == tourrequest.TourDemandOnRequestId).Description;
                            tourrequest.IsOpen = tourDemandDto.Requests.FirstOrDefault(x => x.TourDemandOnRequestId == tourrequest.TourDemandOnRequestId).IsOpen;
                            tourrequest.Approved = tourDemandDto.Requests.Where(x => x.TourDemandOnRequestId == tourrequest.TourDemandOnRequestId).FirstOrDefault().Approved;
                            tourrequest.ConfirmationRequested = tourDemandDto.Requests.Where(x => x.TourDemandOnRequestId == tourrequest.TourDemandOnRequestId).FirstOrDefault().ConfirmationRequested;
                            tourrequest.AskingForApprovalDepartmentId = tourrequest.ConfirmationRequested == true ? Convert.ToInt32(JwtHelper.GetValue("departmentId").ToString()) : null;
                            tourrequest.ApprovalRequestedDepartmentId = tourrequest.ApprovalRequestedDepartmentId;
                            tourrequest.ApprovingDepartmentId = tourrequest.ApprovingDepartmentId;
                            tourrequest.WhoApproves = tourrequest.WhoApproves;
                            _tourDemandOnRequestRepository.Update(tourrequest);
                        });
                    });

                    _mainDemandRepository.SaveChangesAsync().GetAwaiter().GetResult(); ;
                    _hotelDemandRepository.SaveChangesAsync().GetAwaiter().GetResult(); ;
                    _hotelOnRequestRepository.SaveChangesAsync().GetAwaiter().GetResult(); ;
                    _tourDemandRepository.SaveChangesAsync().GetAwaiter().GetResult(); ;
                    _tourDemandOnRequestRepository.SaveChangesAsync().GetAwaiter().GetResult(); ;
                    return new SuccessResult(Messages.Updated);
                });
            }
        }
    }
}
