using AutoMapper;
using Business.Constants;
using Business.Handlers.HotelDemandOnRequests.ValidationRules;
using Business.Helpers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.HotelDemandOnRequests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.HotelDemandOnRequests.Commands
{
    public class CreateHotelDemandOnRequestCommand : IRequest<IResult>
    {
        public int OnRequestId { get; set; }
        public string Description { get; set; }
        public int MainDemandId { get; set; }
        public int HotelDemandId { get; set; }
        public bool ConfirmationRequested { get; set; }
        public int? ApprovalRequestedDepartmentId { get; set; }

        public class CreateHotelDemandOnRequestCommandHanndler : IRequestHandler<CreateHotelDemandOnRequestCommand, IResult>
        {
            private readonly IHotelDemandOnRequestRepository hotelDemandOnRequestRepository;

            public CreateHotelDemandOnRequestCommandHanndler(IHotelDemandOnRequestRepository hotelDemandOnRequestRepository)
            {
                this.hotelDemandOnRequestRepository = hotelDemandOnRequestRepository;
            }
            [ValidationAspect(typeof(CreateHotelDemandOnRequestValidator),Priority =1)]
            [LogAspect(typeof(PostgreSqlLogger),"Hotel talep için sor-sat eklendi",Priority =3)]
            public async Task<IResult> Handle(CreateHotelDemandOnRequestCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() => {
                    var onRequest = new HotelDemandOnRequest()
                    {
                        OnRequestId = request.OnRequestId,
                        Description = request.Description,
                        IsDeleted = false,
                        IsOpen = false,
                        MainDemandId = request.MainDemandId,
                        HotelDemandId = request.HotelDemandId,
                        Approved = false,
                        AskingForApprovalDepartmentId = request.ConfirmationRequested==true?Convert.ToInt32(JwtHelper.GetValue("departmentId")):null,
                        ConfirmationRequested = request.ConfirmationRequested,
                        ApprovalRequestedDepartmentId = request.ApprovalRequestedDepartmentId,
                        CreateDate = DateTime.Now,
                        CreatedUserName = JwtHelper.GetValue("name").ToString(),
                    };

                    hotelDemandOnRequestRepository.Add(onRequest);
                    hotelDemandOnRequestRepository.SaveChangesAsync().GetAwaiter().GetResult();
                    return new SuccessResult(Messages.Added);
                }); 
            }
        }
    }
}
