using AutoMapper;
using Business.Constants;
using Business.Handlers.Demands.ValidationRules;
using Business.Helpers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.ErcanProduct;
using Entities.Concrete;
using Entities.Dtos.HotelDemands;
using Entities.Dtos.MainDemands;
using Entities.Dtos.TourDemands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Demands.Commands
{
    public class UpsertDemandCommand : IRequest<IDataResult<int>>
    {
        public MainDemandUpsertDto MainDemandUpsertDto { get; set; }

        public class UpsertDemandCommandHandler : IRequestHandler<UpsertDemandCommand, IDataResult<int>>
        {
            private readonly IMainDemandRepository _mainDemandRepository;
            private readonly IMainDemandActionRepository _mainDemandActionRepository;
            private readonly IHotelDemandRepository _hotelDemandRepository;
            private readonly IHotelDemandActionRepository _hotelDemandActionRepository;
            private readonly IHotelDemandOnRequestRepository _hotelDemandOnRequestRepository;
            private readonly ITourDemandRepository _tourDemandRepository;
            private readonly ITourDemandActionRepository _tourDemandActionRepository;
            private readonly ITourDemandOnRequestRepository _tourDemandOnRequestRepository;
            private readonly INumberRangeRepository _numberRangeRepository;
            private readonly IMapper _mapper;
            public UpsertDemandCommandHandler(IMainDemandRepository mainDemandRepository, IMainDemandActionRepository mainDemandActionRepository, IHotelDemandRepository hotelDemandRepository, IHotelDemandActionRepository hotelDemandActionRepository, IHotelDemandOnRequestRepository hotelDemandOnRequestRepository, ITourDemandRepository tourDemandRepository, ITourDemandActionRepository tourDemandActionRepository, ITourDemandOnRequestRepository tourDemandOnRequestRepository, IMapper mapper, INumberRangeRepository numberRangeRepository)
            {
                _mainDemandRepository = mainDemandRepository;
                _mainDemandActionRepository = mainDemandActionRepository;
                _hotelDemandRepository = hotelDemandRepository;
                _hotelDemandActionRepository = hotelDemandActionRepository;
                _hotelDemandOnRequestRepository = hotelDemandOnRequestRepository;
                _tourDemandRepository = tourDemandRepository;
                _tourDemandActionRepository = tourDemandActionRepository;
                _tourDemandOnRequestRepository = tourDemandOnRequestRepository;
                _mapper = mapper;
                _numberRangeRepository = numberRangeRepository;
            }
            [LogAspect(typeof(PostgreSqlLogger), "Demand upsert yapıldı", Priority = 3)]
            [TransactionScopeAspectAsync(Priority = 2)]
            [ValidationAspect(typeof(UpsertDemandValidator), Priority = 1)]
            public async Task<IDataResult<int>> Handle(UpsertDemandCommand request, CancellationToken cancellationToken)
            {
                var userName = JwtHelper.GetValue("name").ToString();
                var departmentId = Convert.ToInt32(JwtHelper.GetValue("departmentId").ToString());
                return await Task.Run<IDataResult<int>>(() =>
                {
                    if (request.MainDemandUpsertDto.IsOpen == false)
                    {
                        if (request.MainDemandUpsertDto.HotelDemandUpsertDtos.Any(x => x.OnRequests.Any(o => o.ConfirmationRequested)))
                            return new ErrorDataResult<int>(Messages.OnRequestWaitingApproves);

                        if (request.MainDemandUpsertDto.TourDemandUpsertDtos.Any(x => x.OnRequests.Any(o => o.ConfirmationRequested)))
                            return new ErrorDataResult<int>(Messages.OnRequestWaitingApproves);

                        if (request.MainDemandUpsertDto.HotelDemandUpsertDtos.Any(x => x.Actions.Count == 0))
                            return new ErrorDataResult<int>(Messages.HotelActionCountIsZero);

                        if (request.MainDemandUpsertDto.TourDemandUpsertDtos.Any(x => x.Actions.Count == 0))
                            return new ErrorDataResult<int>(Messages.TourActionCountIsZero);
                    }

                    var oldMainDemand = _mainDemandRepository.GetAsync(x => x.MainDemandId == request.MainDemandUpsertDto.MainDemandId).GetAwaiter().GetResult();
                    if (oldMainDemand == null)
                    {
                        var numberRange = _numberRangeRepository.GetAsync(x => x.Prefix == "REQ").GetAwaiter().GetResult();
                        numberRange.Value++;
                        _numberRangeRepository.Update(numberRange);
                        _numberRangeRepository.SaveChangesAsync().GetAwaiter().GetResult();
                        string newNumberRange = "TLP" + Convert.ToInt32(numberRange.Value + 1).ToString().PadLeft(6, '0');
                        var newMainDemand = new MainDemand()
                        {
                            AreaCode = request.MainDemandUpsertDto.AreaCode,
                            CountryCode = request.MainDemandUpsertDto.CountryCode,
                            CreatedUserName = userName,
                            DemandChannel = request.MainDemandUpsertDto.DemandChannel,
                            Description = request.MainDemandUpsertDto.Description,
                            Email = request.MainDemandUpsertDto.Email,
                            FirmName = request.MainDemandUpsertDto.IsFirm ? request.MainDemandUpsertDto?.FirmName : "",
                            FirmTitle = request.MainDemandUpsertDto.IsFirm ? request.MainDemandUpsertDto?.FirmTitle : "",
                            IsDeleted = false,
                            IsFirm = request.MainDemandUpsertDto.IsFirm,
                            IsOpen = request.MainDemandUpsertDto.IsOpen,
                            Name = request.MainDemandUpsertDto.Name,
                            PhoneNumber = request.MainDemandUpsertDto.PhoneNumber,
                            Surname = request.MainDemandUpsertDto.Surname,
                            CreateDate = DateTime.Now,
                            ReservationNumber = request.MainDemandUpsertDto.ReservationNumber,
                            FullPhoneNumber = (request.MainDemandUpsertDto.CountryCode + request.MainDemandUpsertDto.AreaCode + request.MainDemandUpsertDto.PhoneNumber).Replace(" ", ""),
                            ContactId = request.MainDemandUpsertDto.ContactId,
                            RequestCode = newNumberRange
                        };
                        _mainDemandRepository.Add(newMainDemand);
                        _mainDemandRepository.SaveChangesAsync().GetAwaiter().GetResult();

                        request.MainDemandUpsertDto.Actions.ForEach(action =>
                        {
                            var newAction = new MainDemandAction()
                            {
                                CreateDate = DateTime.Now,
                                CreatedUserName = userName,
                                Description = action.Description,
                                IsDeleted = false,
                                IsOpen = true,
                                ActionId = action.ActionId,
                                MainDemandId = newMainDemand.MainDemandId
                            };
                            _mainDemandActionRepository.Add(newAction);
                        });
                        _mainDemandActionRepository.SaveChangesAsync().GetAwaiter().GetResult();

                        request.MainDemandUpsertDto.HotelDemandUpsertDtos.ForEach(hoteldemand =>
                        {
                            var newHotelDemand = new HotelDemand()
                            {
                                AdultCount = hoteldemand.AdultCount,
                                CheckIn = hoteldemand.CheckIn,
                                CheckOut = hoteldemand.CheckOut,
                                ChildCount = hoteldemand.ChildCount,
                                CreateDate = DateTime.Now,
                                CreatedUserName = userName,
                                Description = hoteldemand.Description,
                                HotelId = hoteldemand.HotelId,
                                Name = hoteldemand.Name,
                                MainDemandId = newMainDemand.MainDemandId,
                                IsDeleted = false,
                                IsOpen = true,
                                TotalCount = hoteldemand.ChildCount + hoteldemand.AdultCount
                            };

                            _hotelDemandRepository.Add(newHotelDemand);
                            _hotelDemandRepository.SaveChangesAsync().GetAwaiter();

                            hoteldemand.Actions.ForEach(action =>
                            {
                                var newAction = new HotelDemandAction()
                                {
                                    ActionId = action.ActionId,
                                    CreateDate = DateTime.Now,
                                    CreatedUserName = userName,
                                    Description = action.Description,
                                    HotelDemandId = action.HotelDemandId,
                                    IsDeleted = false,
                                    IsOpen = true,
                                    MainDemandId = newMainDemand.MainDemandId
                                };
                                _hotelDemandActionRepository.Add(newAction);
                            });
                            hoteldemand.OnRequests.ForEach(onrequest =>
                            {
                                var newOnRequest = new HotelDemandOnRequest()
                                {
                                    CreateDate = DateTime.Now,
                                    CreatedUserName = userName,
                                    Description = onrequest.Description,
                                    HotelDemandId = onrequest.HotelDemandId,
                                    IsDeleted = false,
                                    IsOpen = true,
                                    MainDemandId = newMainDemand.MainDemandId,
                                    OnRequestId = onrequest.OnRequestId, 
                                    AskingForApprovalDepartmentId = onrequest.ConfirmationRequested == true ? departmentId : null, 
                                    ConfirmationRequested = onrequest.ConfirmationRequested,
                                    ApprovalRequestedDepartmentId = onrequest.ApprovalRequestedDepartmentId
                                };
                                _hotelDemandOnRequestRepository.Add(newOnRequest);
                            });
                        });
                        _hotelDemandActionRepository.SaveChangesAsync().GetAwaiter().GetResult();
                        _hotelDemandOnRequestRepository.SaveChangesAsync().GetAwaiter().GetResult();

                        request.MainDemandUpsertDto.TourDemandUpsertDtos.ForEach(tourdemand =>
                        {
                            var newTourDemand = new TourDemand()
                            {
                                AdultCount = tourdemand.AdultCount,
                                ChildCount = tourdemand.ChildCount,
                                CreateDate = DateTime.Now,
                                CreatedUserName = userName,
                                Description = tourdemand.Description,
                                Period = tourdemand.Period,
                                TourId = tourdemand.TourId,
                                Name = tourdemand.Name,
                                MainDemandId = newMainDemand.MainDemandId,
                                IsDeleted = false,
                                IsOpen = true,
                                TotalCount = tourdemand.ChildCount + tourdemand.AdultCount
                            };
                            _tourDemandRepository.Add(newTourDemand);

                            _tourDemandRepository.SaveChangesAsync().GetAwaiter().GetResult();
                            tourdemand.Actions.ForEach(action =>
                            {
                                var newAction = new TourDemandAction()
                                {
                                    ActionId = action.ActionId,
                                    CreateDate = DateTime.Now,
                                    CreatedUserName = userName,
                                    Description = action.Description,
                                    TourDemandId = action.TourDemandId ?? newTourDemand.TourDemandId,
                                    IsDeleted = false,
                                    IsOpen = true,
                                    MainDemandId = newMainDemand.MainDemandId,
                                };
                                _tourDemandActionRepository.Add(newAction);
                            });
                            tourdemand.OnRequests.ForEach(onrequest =>
                            {
                                var newOnRequest = new TourDemandOnRequest()
                                {
                                    CreateDate = DateTime.Now,
                                    CreatedUserName = userName,
                                    Description = onrequest.Description,
                                    TourDemandId = newTourDemand.TourDemandId,
                                    IsDeleted = false,
                                    IsOpen = true,
                                    MainDemandId = newMainDemand.MainDemandId,
                                    OnRequestId = onrequest.OnRequestId, 
                                    AskingForApprovalDepartmentId = onrequest.ConfirmationRequested == true ? departmentId : null, 
                                    ConfirmationRequested = onrequest.ConfirmationRequested,
                                    ApprovalRequestedDepartmentId = onrequest.ApprovalRequestedDepartmentId
                                };
                                _tourDemandOnRequestRepository.Add(newOnRequest);
                            });

                        });
                        _tourDemandActionRepository.SaveChangesAsync().GetAwaiter();
                        _tourDemandOnRequestRepository.SaveChangesAsync().GetAwaiter();
                        return new SuccessDataResult<int>(newMainDemand.MainDemandId, Messages.Upserted);
                    }
                    else
                    {
                        oldMainDemand.Surname = request.MainDemandUpsertDto.Surname;
                        oldMainDemand.Description = request.MainDemandUpsertDto.Description;
                        oldMainDemand.IsFirm = request.MainDemandUpsertDto.IsFirm;
                        oldMainDemand.FirmName = request.MainDemandUpsertDto.IsFirm ? request.MainDemandUpsertDto.FirmName : "";
                        oldMainDemand.FirmTitle = request.MainDemandUpsertDto.IsFirm ? request.MainDemandUpsertDto.FirmTitle : "";
                        oldMainDemand.AreaCode = request.MainDemandUpsertDto.AreaCode;
                        oldMainDemand.IsDeleted = false;
                        oldMainDemand.IsOpen = request.MainDemandUpsertDto.IsOpen;
                        oldMainDemand.CountryCode = request.MainDemandUpsertDto.CountryCode;
                        oldMainDemand.DemandChannel = request.MainDemandUpsertDto.DemandChannel;
                        oldMainDemand.Email = request.MainDemandUpsertDto.Email;
                        oldMainDemand.Name = request.MainDemandUpsertDto.Name;
                        oldMainDemand.PhoneNumber = request.MainDemandUpsertDto.PhoneNumber;
                        oldMainDemand.ReservationNumber = request.MainDemandUpsertDto.ReservationNumber;
                        oldMainDemand.FullPhoneNumber = request.MainDemandUpsertDto.CountryCode + request.MainDemandUpsertDto.AreaCode + request.MainDemandUpsertDto.PhoneNumber;
                        oldMainDemand.ContactId=request.MainDemandUpsertDto.ContactId;
                        _mainDemandRepository.Update(oldMainDemand);
                        _mainDemandRepository.SaveChangesAsync().GetAwaiter().GetResult();  

                        var oldmainActions = _mainDemandActionRepository.GetListAsync(m => m.MainDemandId == request.MainDemandUpsertDto.MainDemandId).GetAwaiter().GetResult();
                        request.MainDemandUpsertDto.Actions.ForEach(mainAction =>
                        {
                            var oldmainAction = oldmainActions.Where(x => x.MainDemandActionId == mainAction.MainDemandActionId).FirstOrDefault();
                            if (oldmainAction != null)
                            {
                                oldmainAction.Description = mainAction.Description;
                                oldmainAction.ActionId = mainAction.ActionId;
                                _mainDemandActionRepository.Update(oldmainAction);
                            }
                            else
                            {
                                _mainDemandActionRepository.Add(new MainDemandAction()
                                {
                                    MainDemandId = oldMainDemand.MainDemandId,
                                    ActionId = mainAction.ActionId,
                                    CreateDate = DateTime.Now,
                                    CreatedUserName = userName,
                                    Description = mainAction.Description,
                                    IsDeleted = false,
                                    IsOpen = false,
                                });
                            }

                        });
                        _mainDemandActionRepository.SaveChangesAsync().GetAwaiter().GetResult();

                        var oldhotelDemands = _hotelDemandRepository.GetListAsync(x => x.MainDemandId == request.MainDemandUpsertDto.MainDemandId).GetAwaiter().GetResult();
                        request.MainDemandUpsertDto.HotelDemandUpsertDtos.ForEach(hoteldemand =>
                        {
                            var oldhotelDemand = oldhotelDemands.Where(x => x.HotelDemandId == hoteldemand.HotelDemandId).FirstOrDefault();
                            if (oldhotelDemand != null)
                            {
                                oldhotelDemand.AdultCount = hoteldemand.AdultCount;
                                oldhotelDemand.ChildCount = hoteldemand.ChildCount;
                                oldhotelDemand.IsDeleted = false;
                                oldhotelDemand.HotelId = hoteldemand.HotelId;
                                oldhotelDemand.IsOpen = request.MainDemandUpsertDto.IsOpen==false ? false: hoteldemand.IsOpen;
                                oldhotelDemand.Name = hoteldemand.Name;
                                oldhotelDemand.TotalCount = hoteldemand.ChildCount + hoteldemand.AdultCount;
                                oldhotelDemand.CheckIn = hoteldemand.CheckIn;
                                oldhotelDemand.CheckOut = hoteldemand.CheckOut;
                                oldhotelDemand.Description = hoteldemand.Description;
                                _hotelDemandRepository.Update(oldhotelDemand);
                                _hotelDemandRepository.SaveChangesAsync().GetAwaiter().GetResult();

                                var oldHotelDemandActions = _hotelDemandActionRepository.GetListAsync(x => x.MainDemandId == hoteldemand.MainDemandId && x.HotelDemandId == hoteldemand.HotelDemandId).GetAwaiter().GetResult();
                                hoteldemand.Actions.ForEach(hotelaction =>
                                {
                                    var oldaction = oldHotelDemandActions.Where(x => x.HotelDemandActionId == hotelaction.HotelDemandActionId).FirstOrDefault();
                                    if (oldaction != null)
                                    {
                                        oldaction.IsDeleted = false;
                                        oldaction.HotelDemandId = hoteldemand.HotelDemandId;
                                        oldaction.MainDemandId = hoteldemand.MainDemandId;
                                        oldaction.Description = hoteldemand.Description;
                                        oldaction.IsOpen = hoteldemand.IsOpen;
                                        _hotelDemandActionRepository.Update(oldaction);
                                    }
                                    else
                                    {
                                        var newAction = new HotelDemandAction()
                                        {
                                            ActionId = hotelaction.ActionId,
                                            IsOpen = request.MainDemandUpsertDto.IsOpen == false ? false: hotelaction.IsOpen,
                                            CreateDate = DateTime.Now,
                                            CreatedUserName = userName,
                                            Description = hoteldemand.Description,
                                            IsDeleted = false,
                                            HotelDemandId = oldhotelDemand.HotelDemandId,
                                            MainDemandId = oldMainDemand.MainDemandId
                                        };
                                        _hotelDemandActionRepository.Add(newAction);
                                    }
                                });
                                _hotelDemandActionRepository.SaveChangesAsync().GetAwaiter().GetResult();
                                var oldHotelDemandOnRequests = _hotelDemandOnRequestRepository.GetListAsync(x => x.MainDemandId == hoteldemand.MainDemandId && x.HotelDemandId == hoteldemand.HotelDemandId).GetAwaiter().GetResult();
                                hoteldemand.OnRequests.ForEach(hotelonrequest =>
                                {
                                    var oldOnRequest = oldHotelDemandOnRequests.Where(x => x.HotelDemandOnRequestId == hotelonrequest.HotelDemandOnRequestId).FirstOrDefault();
                                    if (oldOnRequest != null)
                                    { 
                                        oldOnRequest.ApprovalRequestedDepartmentId = hotelonrequest.ApprovalRequestedDepartmentId;
                                        oldOnRequest.AskingForApprovalDepartmentId = hotelonrequest.AskingForApprovalDepartmentId;
                                        oldOnRequest.ConfirmationRequested = hotelonrequest.ConfirmationRequested;
                                        oldOnRequest.Description = hotelonrequest.Description;
                                        oldOnRequest.IsOpen = request.MainDemandUpsertDto.IsOpen == false ? false: hotelonrequest.IsOpen;
                                        oldOnRequest.MainDemandId = oldOnRequest.MainDemandId;
                                        oldOnRequest.OnRequestId = hotelonrequest.OnRequestId;
                                        _hotelDemandOnRequestRepository.Update(oldOnRequest);
                                    }
                                    else
                                    {
                                        var newOnRequest = new HotelDemandOnRequest()
                                        {
                                            ApprovalRequestedDepartmentId = hotelonrequest.ApprovalRequestedDepartmentId, 
                                            AskingForApprovalDepartmentId = departmentId,
                                            ConfirmationRequested = hotelonrequest.ConfirmationRequested,
                                            Description = hotelonrequest.Description,
                                            IsOpen = request.MainDemandUpsertDto.IsOpen == false ? false:hotelonrequest.IsOpen,
                                            HotelDemandId = hotelonrequest.HotelDemandId,
                                            CreateDate = DateTime.Now,
                                            CreatedUserName = userName,
                                            IsDeleted = false,
                                            MainDemandId = oldMainDemand.MainDemandId,
                                            OnRequestId = hotelonrequest.OnRequestId,
                                        };
                                        _hotelDemandOnRequestRepository.Add(newOnRequest);
                                    }
                                });
                                _hotelDemandOnRequestRepository.SaveChangesAsync().GetAwaiter().GetResult();
                            }
                            else
                            {
                                var newHotelDemand = new HotelDemand()
                                {
                                    AdultCount = hoteldemand.AdultCount,
                                    CheckIn = hoteldemand.CheckIn,
                                    CheckOut = hoteldemand.CheckOut,
                                    ChildCount = hoteldemand.ChildCount,
                                    CreateDate = hoteldemand.CreateDate,
                                    CreatedUserName = userName,
                                    Description = hoteldemand.Description,
                                    HotelId = hoteldemand.HotelId,
                                    IsDeleted = false,
                                    IsOpen = request.MainDemandUpsertDto.IsOpen == false ? false:true,
                                    MainDemandId = oldMainDemand.MainDemandId,
                                    Name = hoteldemand.Name,
                                    TotalCount = hoteldemand.AdultCount + hoteldemand.ChildCount
                                };

                                _hotelDemandRepository.Add(newHotelDemand);
                                _hotelDemandRepository.SaveChangesAsync().GetAwaiter().GetResult();
                                var oldHotelDemandActions = _hotelDemandActionRepository.GetListAsync(x => x.MainDemandId == oldMainDemand.MainDemandId && x.HotelDemandId == hoteldemand.HotelDemandId).GetAwaiter().GetResult().ToList();
                                hoteldemand.Actions.ForEach(action =>
                                {
                                    var oldaction = oldHotelDemandActions.Where(x => x.HotelDemandActionId == action.HotelDemandActionId).FirstOrDefault();
                                    if (oldaction != null)
                                    {
                                        oldaction.ActionId = action.ActionId;
                                        oldaction.Description = action.Description;
                                        oldaction.HotelDemandActionId = action.HotelDemandActionId;
                                        oldaction.IsOpen = action.IsOpen;
                                        oldaction.HotelDemandId = newHotelDemand.HotelDemandId;
                                        oldaction.MainDemandId = oldMainDemand.MainDemandId;
                                        oldaction.IsDeleted = action.IsDeleted;
                                        _hotelDemandActionRepository.Update(oldaction);
                                        _hotelDemandActionRepository.SaveChangesAsync().GetAwaiter().GetResult();
                                    }
                                    else
                                    {
                                        _hotelDemandActionRepository.Add(new HotelDemandAction()
                                        {
                                            ActionId = action.ActionId,
                                            CreateDate = action.CreateDate,
                                            CreatedUserName = userName,
                                            Description = action.Description,
                                            HotelDemandId = newHotelDemand.MainDemandId,
                                            IsDeleted = false,
                                            IsOpen = request.MainDemandUpsertDto.IsOpen == false ? false: action.IsOpen,
                                            MainDemandId = oldMainDemand.MainDemandId
                                        });
                                    }
                                });
                                _hotelDemandActionRepository.SaveChangesAsync().GetAwaiter().GetResult();
                                var oldonRequests = _hotelDemandOnRequestRepository.GetListAsync(x => x.MainDemandId == oldMainDemand.MainDemandId && x.HotelDemandId == hoteldemand.HotelDemandId).GetAwaiter().GetResult();
                                hoteldemand.OnRequests.ForEach(onrequest =>
                                {
                                    var oldonrequest = oldonRequests.Where(x => x.HotelDemandOnRequestId == onrequest.HotelDemandOnRequestId).FirstOrDefault();
                                    if (oldonrequest != null)
                                    {
                                        oldonrequest.Description = onrequest.Description;
                                        oldonrequest.HotelDemandId = hoteldemand.HotelDemandId;
                                        oldonrequest.OnRequestId = onrequest.OnRequestId;
                                        oldonrequest.MainDemandId = onrequest.MainDemandId; 
                                        oldonrequest.ConfirmationRequested = onrequest.ConfirmationRequested; 
                                        oldonrequest.ApprovalRequestedDepartmentId = onrequest.ApprovalRequestedDepartmentId;
                                        oldonrequest.IsOpen= onrequest.IsOpen;
                                        _hotelDemandOnRequestRepository.Update(oldonrequest); 
                                    }
                                    else
                                    {
                                        _hotelDemandOnRequestRepository.Add(new HotelDemandOnRequest()
                                        {
                                            CreateDate = DateTime.Now,
                                            CreatedUserName = userName,
                                            Description = onrequest.Description,
                                            HotelDemandId = newHotelDemand.HotelDemandId,
                                            OnRequestId = onrequest.OnRequestId,
                                            IsDeleted = false,
                                            IsOpen = request.MainDemandUpsertDto.IsOpen == false ? false: onrequest.IsOpen,
                                            MainDemandId = oldMainDemand.MainDemandId, 
                                            ConfirmationRequested = onrequest.ConfirmationRequested,
                                            AskingForApprovalDepartmentId = onrequest.ConfirmationRequested == true ? departmentId : null,
                                            ApprovalRequestedDepartmentId = onrequest.ApprovalRequestedDepartmentId,

                                        });
                                    }
                                });
                                _hotelDemandOnRequestRepository.SaveChangesAsync().GetAwaiter().GetResult();
                            }
                        });

                        var oldtourDemands = _tourDemandRepository.GetListAsync(x => x.MainDemandId == request.MainDemandUpsertDto.MainDemandId).GetAwaiter().GetResult();
                        request.MainDemandUpsertDto.TourDemandUpsertDtos.ForEach(tourdemand =>
                        {
                            var oldtourDemand = oldtourDemands.Where(x => x.TourDemandId == tourdemand.TourDemandId).FirstOrDefault();
                            if (oldtourDemand != null)
                            {
                                oldtourDemand.Period = tourdemand.Period;
                                oldtourDemand.Description = tourdemand.Description;
                                oldtourDemand.Name = tourdemand.Name;
                                oldtourDemand.AdultCount = tourdemand.AdultCount;
                                oldtourDemand.ChildCount = tourdemand.ChildCount;
                                oldtourDemand.TourId = tourdemand.TourId;
                                oldtourDemand.IsOpen = request.MainDemandUpsertDto.IsOpen == false ? false : tourdemand.IsOpen;
                                oldtourDemand.TotalCount = tourdemand.AdultCount + tourdemand.ChildCount;
                                oldtourDemand.TourId = tourdemand.TourId;
                                oldtourDemand.MainDemandId = oldMainDemand.MainDemandId;
                                _tourDemandRepository.Update(oldtourDemand);
                                _tourDemandRepository.SaveChangesAsync().GetAwaiter().GetResult();

                                var tourActions = _tourDemandActionRepository.GetListAsync(x => x.MainDemandId == request.MainDemandUpsertDto.MainDemandId && x.TourDemandId == tourdemand.TourDemandId).GetAwaiter().GetResult();
                                tourdemand.Actions.ForEach(tourDemandAction =>
                                {
                                    var oldtourAction = tourActions.Where(x => x.TourDemandActionId == tourDemandAction.TourDemandActionId).FirstOrDefault();
                                    if (oldtourAction != null)
                                    {
                                        oldtourAction.ActionId = tourDemandAction.ActionId;
                                        oldtourAction.TourDemandId = oldtourDemand.TourDemandId;
                                        oldtourAction.MainDemandId = oldMainDemand.MainDemandId;
                                        oldtourAction.ActionId = tourDemandAction.ActionId;
                                        oldtourAction.Description = tourDemandAction.Description;
                                        oldtourAction.IsDeleted = false;
                                        oldtourAction.IsOpen = request.MainDemandUpsertDto.IsOpen == false ? false:tourDemandAction.IsOpen;
                                        _tourDemandActionRepository.Update(oldtourAction);
                                    }
                                    else
                                    {
                                        var newTourAction = new TourDemandAction()
                                        {
                                            ActionId = tourDemandAction.ActionId,
                                            CreateDate = DateTime.Now,
                                            CreatedUserName = userName,
                                            Description = tourDemandAction.Description,
                                            IsDeleted = false,
                                            IsOpen = request.MainDemandUpsertDto.IsOpen == false ? false:true,
                                            MainDemandId = oldMainDemand.MainDemandId,
                                            TourDemandId = oldtourDemand.TourDemandId,
                                        };
                                        _tourDemandActionRepository.Add(newTourAction);
                                    }
                                });
                                _tourDemandActionRepository.SaveChangesAsync().GetAwaiter().GetResult();

                                var tourOnRequests = _tourDemandOnRequestRepository.GetListAsync(x => x.MainDemandId == request.MainDemandUpsertDto.MainDemandId).GetAwaiter().GetResult();
                                tourdemand.OnRequests.ForEach(tourDemandOnRequest =>
                                {
                                    var oldtourOnRequest = tourOnRequests.Where(x => x.TourDemandOnRequestId == tourDemandOnRequest.TourDemandOnRequestId).FirstOrDefault();
                                    if (oldtourOnRequest != null)
                                    { 
                                        oldtourOnRequest.ApprovalRequestedDepartmentId = tourDemandOnRequest.ApprovalRequestedDepartmentId;
                                        oldtourOnRequest.AskingForApprovalDepartmentId = tourDemandOnRequest.AskingForApprovalDepartmentId;
                                        oldtourOnRequest.TourDemandId = oldtourDemand.TourDemandId;
                                        oldtourOnRequest.ConfirmationRequested = tourDemandOnRequest.ConfirmationRequested;
                                        oldtourOnRequest.CreatedUserName = userName;
                                        oldtourOnRequest.Description = tourDemandOnRequest.Description;
                                        oldtourOnRequest.IsDeleted = false;
                                        oldtourOnRequest.IsOpen = request.MainDemandUpsertDto.IsOpen == false ? false:tourDemandOnRequest.IsOpen;
                                        oldtourOnRequest.MainDemandId = oldMainDemand.MainDemandId;
                                        oldtourOnRequest.OnRequestId = tourDemandOnRequest.OnRequestId;
                                        _tourDemandOnRequestRepository.Update(oldtourOnRequest);
                                    }
                                    else
                                    {
                                        var newTourOnRequest = new TourDemandOnRequest()
                                        {
                                            ApprovalRequestedDepartmentId = tourDemandOnRequest.ApprovalRequestedDepartmentId, 
                                            AskingForApprovalDepartmentId = tourDemandOnRequest.ConfirmationRequested == true ? departmentId : null,
                                            ConfirmationRequested = tourDemandOnRequest.ConfirmationRequested,
                                            CreateDate = DateTime.Now,
                                            CreatedUserName = userName,
                                            Description = tourDemandOnRequest.Description,
                                            IsDeleted = false,
                                            IsOpen = request.MainDemandUpsertDto.IsOpen == false ? false: tourDemandOnRequest.IsOpen,
                                            MainDemandId = oldMainDemand.MainDemandId,
                                            OnRequestId = tourDemandOnRequest.OnRequestId,
                                            TourDemandId = oldtourDemand.TourDemandId,
                                        };
                                        _tourDemandOnRequestRepository.Add(newTourOnRequest);
                                    }
                                });
                                _tourDemandOnRequestRepository.SaveChangesAsync().GetAwaiter().GetResult();
                            }
                            else
                            {
                                var newTourDemand = new TourDemand()
                                {
                                    AdultCount = tourdemand.AdultCount,
                                    ChildCount = tourdemand.ChildCount,
                                    TourId = tourdemand.TourId,
                                    IsOpen = request.MainDemandUpsertDto.IsOpen == false ? false:true,
                                    CreateDate = DateTime.Now,
                                    CreatedUserName = userName,
                                    Description = tourdemand.Description,
                                    Name = tourdemand.Name,
                                    IsDeleted = false,
                                    Period = tourdemand.Period,
                                    TotalCount = tourdemand.AdultCount + tourdemand.ChildCount,
                                    MainDemandId = oldMainDemand.MainDemandId,
                                };
                                _tourDemandRepository.Add(newTourDemand);
                                _tourDemandRepository.SaveChangesAsync().GetAwaiter().GetResult();

                                request.MainDemandUpsertDto.TourDemandUpsertDtos.ForEach(tourdemand =>
                                {
                                    tourdemand.Actions.ForEach(touraction =>
                                    {
                                        var newTourAction = new TourDemandAction()
                                        {
                                            ActionId = touraction.ActionId,
                                            CreateDate = DateTime.Now,
                                            CreatedUserName = userName,
                                            Description = touraction.Description,
                                            IsDeleted = false,
                                            IsOpen = request.MainDemandUpsertDto.IsOpen == false ? false: touraction.IsOpen,
                                            MainDemandId = oldMainDemand.MainDemandId,
                                            TourDemandId = newTourDemand.TourDemandId
                                        };
                                        _tourDemandActionRepository.Add(newTourAction);
                                    });
                                    _tourDemandActionRepository.SaveChangesAsync().GetAwaiter().GetResult();

                                    tourdemand.OnRequests.ForEach(tourOnRequest =>
                                    {
                                        var newTourOnRequest = new TourDemandOnRequest()
                                        {
                                            ApprovalRequestedDepartmentId = tourOnRequest.ApprovalRequestedDepartmentId, 
                                            AskingForApprovalDepartmentId = tourOnRequest.ConfirmationRequested == true ? departmentId : null, 
                                            ConfirmationRequested = tourOnRequest.ConfirmationRequested,
                                            CreateDate = DateTime.Now,
                                            CreatedUserName = userName,
                                            Description = tourOnRequest.Description,
                                            IsDeleted = false,
                                            IsOpen = request.MainDemandUpsertDto.IsOpen == false ? false: tourOnRequest.IsOpen,
                                            MainDemandId = oldMainDemand.MainDemandId,
                                            OnRequestId = tourOnRequest.OnRequestId,
                                            TourDemandId = newTourDemand.TourDemandId
                                        };
                                        _tourDemandOnRequestRepository.Add(newTourOnRequest);
                                    });
                                    _tourDemandOnRequestRepository.SaveChangesAsync().GetAwaiter().GetResult();

                                });
                            }
                        });
                        return new SuccessDataResult<int>(oldMainDemand.MainDemandId, Messages.Upserted);
                    }
                    
                });
            }
        }
    }
}
