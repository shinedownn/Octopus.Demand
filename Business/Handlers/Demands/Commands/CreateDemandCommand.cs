using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.Demands.ValidationRules;
using Business.Handlers.ErcanProduct.NumberRange.Queries;
using Business.Helpers;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.ErcanProduct;
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
    public class CreateDemandCommand : IRequest<IDataResult<int>>
    { 
        public MainDemandInsertDto MainDemandDto { get; set; }
        public List<HotelDemandInsertDto> HotelDemandDtos { get; set; }
        public List<TourDemandInsertDto> TourDemandDtos { get; set; } 

        public class CreateDemandCommandHandler : IRequestHandler<CreateDemandCommand, IDataResult<int>>
        {
            private readonly IMainDemandRepository _mainDemandRepository;
            private readonly IMainDemandActionRepository _mainDemandActionRepository;
            private readonly IHotelDemandRepository _hotelDemandRepository;
            private readonly ITourDemandRepository _tourDemandRepository;
            private readonly IHotelDemandActionRepository _hotelDemandActionRepository;
            private readonly ITourDemandActionRepository _tourDemandActionRepository;
            private readonly IHotelDemandOnRequestRepository _hotelDemandOnRequestRepository;
            private readonly ITourDemandOnRequestRepository _tourDemandOnRequestRepository;
            private readonly INumberRangeRepository _numberRangeRepository;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public CreateDemandCommandHandler(IMainDemandRepository mainDemandRepository, IHotelDemandRepository hotelDemandRepository, ITourDemandRepository tourDemandRepository, IHotelDemandActionRepository hotelDemandActionRepository, ITourDemandActionRepository tourDemandActionRepository, IMapper mapper, IHotelDemandOnRequestRepository hotelDemandOnRequestRepository, ITourDemandOnRequestRepository tourDemandOnRequestRepository, IMainDemandActionRepository mainDemandActionRepository, INumberRangeRepository numberRangeRepository, IMediator mediator)
            {
                _mainDemandRepository = mainDemandRepository;
                _hotelDemandRepository = hotelDemandRepository;
                _tourDemandRepository = tourDemandRepository;
                _mapper = mapper;
                _hotelDemandActionRepository = hotelDemandActionRepository;
                _tourDemandActionRepository = tourDemandActionRepository;
                _hotelDemandOnRequestRepository = hotelDemandOnRequestRepository;
                _tourDemandOnRequestRepository = tourDemandOnRequestRepository;
                _mainDemandActionRepository = mainDemandActionRepository;
                _numberRangeRepository = numberRangeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateDemandValidator), Priority = 1)]
            [TransactionScopeAspectAsync(Priority = 2)]
            [LogAspect(typeof(PostgreSqlLogger),"Talep oluşturuldu", Priority = 3)]
            public async Task<IDataResult<int>> Handle(CreateDemandCommand request, CancellationToken cancellationToken)
            {  
                return await Task.Run<IDataResult<int>>(() =>
                {
                    var numberRange = _numberRangeRepository.GetAsync(x => x.Prefix == "REQ").GetAwaiter().GetResult();
                    numberRange.Value++;
                    _numberRangeRepository.Update(numberRange);
                    _numberRangeRepository.SaveChangesAsync().GetAwaiter().GetResult();
                    string newNumberRange = "TLP" + Convert.ToInt32(numberRange.Value + 1).ToString().PadLeft(6, '0');
                    //return new SuccessDataResult<int>("TLP" + Convert.ToInt32(numberRange.Value + 1).ToString().PadLeft(6, '0'));

                    if (request.MainDemandDto.IsOpen == false)
                    {
                        if (request.HotelDemandDtos.Any(x => x.OnRequests.Any(o => o.ConfirmationRequested)))
                            return new ErrorDataResult<int>(Messages.OnRequestWaitingApproves);

                        if (request.TourDemandDtos.Any(x => x.OnRequests.Any(o => o.ConfirmationRequested)))
                            return new ErrorDataResult<int>(Messages.OnRequestWaitingApproves);

                        if (request.HotelDemandDtos.Any(x => x.Actions.Count == 0))
                            return new ErrorDataResult<int>(Messages.ActionCountIsZero);

                        if (request.TourDemandDtos.Any(x => x.Actions.Count == 0))
                            return new ErrorDataResult<int>(Messages.ActionCountIsZero);
                    }

                    var FullName = JwtHelper.GetValue("name").ToString();

                    var departmentId = Convert.ToInt32(JwtHelper.GetValue("departmentId").ToString());

                    var maindemand = new MainDemand()
                    {
                        CreateDate = DateTime.Now,
                        DemandChannel = request.MainDemandDto.DemandChannel,
                        CreatedUserName = FullName,
                        Description = request.MainDemandDto.Description,
                        Email = request.MainDemandDto.Email,
                        IsDeleted = false,
                        IsOpen = request.MainDemandDto.IsOpen,
                        Name = request.MainDemandDto.Name,
                        PhoneNumber = request.MainDemandDto.PhoneNumber,
                        ReservationNumber = request.MainDemandDto.ReservationNumber,
                        Surname = request.MainDemandDto.Surname,
                        IsFirm = request.MainDemandDto.IsFirm,
                        FirmName = request.MainDemandDto.FirmName,
                        FirmTitle = request.MainDemandDto.FirmTitle,
                        AreaCode = request.MainDemandDto.AreaCode,
                        CountryCode = request.MainDemandDto.CountryCode,
                        FullPhoneNumber = (request.MainDemandDto.CountryCode + request.MainDemandDto.AreaCode + request.MainDemandDto.PhoneNumber).Replace(" ", ""),
                        ContactId=request.MainDemandDto.ContactId,
                        RequestCode= newNumberRange
                    };

                    var insertedMainDemand = _mainDemandRepository.Add(maindemand);
                    var resulttttt = _mainDemandRepository.SaveChangesAsync().GetAwaiter().GetResult();

                    request.MainDemandDto.Actions.ForEach(actiondto =>
                    {
                        var action = new MainDemandAction()
                        {
                            CreateDate = DateTime.Now,
                            CreatedUserName = FullName,
                            IsDeleted = false,
                            IsOpen = actiondto.IsOpen,
                            MainDemandId = insertedMainDemand.MainDemandId,
                            ActionId = actiondto.ActionId,
                            Description = actiondto.Description,
                        };
                        _mainDemandActionRepository.Add(action);
                    });
                    var mainActionAnyClosed = request.MainDemandDto.Actions.Any(x => x.IsOpen == false);
                    request.HotelDemandDtos.ForEach(x =>
                    {
                        var hotelDemand = new HotelDemand()
                        {
                            HotelId = x.HotelId,
                            IsDeleted = false,
                            IsOpen = true,
                            AdultCount = x.AdultCount,
                            ChildCount = x.ChildCount,
                            TotalCount = x.AdultCount + x.ChildCount,
                            MainDemandId = insertedMainDemand.MainDemandId,
                            CreateDate = DateTime.Now,
                            CreatedUserName = FullName,
                            Description = x.Description,
                            CheckIn= x.CheckIn.ToUniversalTime(),
                            CheckOut= x.CheckOut.ToUniversalTime(),
                            Name = x.Name, 
                            
                        };

                        _hotelDemandRepository.Add(hotelDemand);
                        _hotelDemandRepository.SaveChangesAsync().GetAwaiter().GetResult();

                        x.Actions.ForEach(actiondto =>
                        {
                            var action = new HotelDemandAction()
                            {
                                HotelDemandId = hotelDemand.HotelDemandId,
                                IsDeleted = false,
                                IsOpen = mainActionAnyClosed?false:true,
                                CreateDate = DateTime.Now,
                                CreatedUserName = FullName,
                                ActionId= actiondto.ActionId,
                                MainDemandId=insertedMainDemand.MainDemandId,
                                Description=actiondto.Description,
                                
                            };
                            _hotelDemandActionRepository.Add(action);
                        });

                        x.OnRequests.ForEach(requestDto =>
                        {
                            var request = new HotelDemandOnRequest()
                            {
                                OnRequestId = requestDto.OnRequestId,
                                CreateDate = DateTime.Now,
                                CreatedUserName = FullName,
                                Description = requestDto.Description,
                                HotelDemandId = hotelDemand.HotelDemandId,
                                IsDeleted = false,
                                IsOpen = mainActionAnyClosed ? false : requestDto.IsOpen,
                                MainDemandId = hotelDemand.MainDemandId,  
                                AskingForApprovalDepartmentId=requestDto.ConfirmationRequested==true ? departmentId : null,
                                ConfirmationRequested=requestDto.ConfirmationRequested,
                                ApprovalRequestedDepartmentId= requestDto.ApprovalRequestedDepartmentId,
                                Note=requestDto.Note
                            };
                            _hotelDemandOnRequestRepository.Add(request);
                        });
                    });

                    request.TourDemandDtos.ForEach(x =>
                    {
                        var tourDemand = new TourDemand()
                        {
                            TourId = x.TourId,
                            MainDemandId = insertedMainDemand.MainDemandId,
                            AdultCount = x.AdultCount,
                            ChildCount = x.ChildCount,
                            TotalCount = x.AdultCount + x.ChildCount,
                            IsDeleted = false,
                            IsOpen = mainActionAnyClosed ? false : true,
                            CreateDate = DateTime.Now,
                            CreatedUserName = FullName,
                            Name = x.Name,
                            Period = x.Period,
                            Description = x.Description, 
                        };

                        _tourDemandRepository.Add(tourDemand);
                        _tourDemandRepository.SaveChangesAsync().GetAwaiter().GetResult();

                        x.Actions.ForEach(actionddto =>
                        {
                            var action = new TourDemandAction()
                            {
                                TourDemandId = tourDemand.TourDemandId,
                                IsDeleted = false,
                                IsOpen = mainActionAnyClosed ? false : true,
                                CreateDate = DateTime.Now,
                                CreatedUserName = FullName,
                                ActionId = actionddto.ActionId,
                                Description= actionddto.Description,
                                MainDemandId=insertedMainDemand.MainDemandId, 
                                
                            };
                            _tourDemandActionRepository.Add(action); 
                        });

                        x.OnRequests.ForEach(requestDto =>
                        {
                            var request = new TourDemandOnRequest()
                            {
                                OnRequestId = requestDto.OnRequestId,
                                CreateDate = DateTime.Now,
                                CreatedUserName = FullName,
                                Description = requestDto.Description,
                                IsDeleted = false,
                                IsOpen = mainActionAnyClosed ? false : requestDto.IsOpen,
                                MainDemandId = maindemand.MainDemandId,
                                TourDemandId = tourDemand.TourDemandId, 
                                AskingForApprovalDepartmentId = requestDto.ConfirmationRequested == true ? departmentId : null,
                                ConfirmationRequested = requestDto.ConfirmationRequested,
                                ApprovalRequestedDepartmentId = requestDto.ApprovalRequestedDepartmentId
                            };
                            _tourDemandOnRequestRepository.Add(request);
                        });
                    });
                    _mainDemandActionRepository.SaveChangesAsync().GetAwaiter().GetResult();
                    _tourDemandActionRepository.SaveChangesAsync().GetAwaiter().GetResult();
                    _tourDemandOnRequestRepository.SaveChangesAsync().GetAwaiter().GetResult();

                    _hotelDemandActionRepository.SaveChangesAsync().GetAwaiter().GetResult();
                    _hotelDemandOnRequestRepository.SaveChangesAsync().GetAwaiter().GetResult();

                    return new SuccessDataResult<int>(maindemand.MainDemandId, Messages.Added);
                });
            }
        }
    }
}
