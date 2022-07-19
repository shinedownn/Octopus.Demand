using Business.Constants;
using Business.Handlers.HotelDemandOnRequests.ValidationRules;
using Business.Helpers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.HotelDemandOnRequests.Commands
{
    public class UpdateHotelDemandOnRequestCommand : IRequest<IResult>
    {
        public int HotelDemandOnRequestId { get; set; }
        public string Description { get; set; }
        public int OnRequestId { get; set; }
        public bool IsOpen { get; set; }
        public bool? Approved { get; set; }
        public bool ConfirmationRequested { get; set; }
        public int? ApprovalRequestedDepartmentId { get; set; }
        public int ApprovingDepartmentId { get; set; }

        public class UpdateHotelDemandOnRequestCommandHandler : IRequestHandler<UpdateHotelDemandOnRequestCommand, IResult>
        {
            private readonly IHotelDemandOnRequestRepository hotelDemandOnRequestRepository;
            public UpdateHotelDemandOnRequestCommandHandler(IHotelDemandOnRequestRepository hotelDemandOnRequestRepository)
            {
                this.hotelDemandOnRequestRepository = hotelDemandOnRequestRepository;
            }
            [ValidationAspect(typeof(UpdateHotelDemandOnRequestValidator), Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger), "Hotel talep sor-sat güncellendi", Priority = 3)]
            public async Task<IResult> Handle(UpdateHotelDemandOnRequestCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() =>
                {
                    var updateTo = hotelDemandOnRequestRepository.GetAsync(x => x.HotelDemandOnRequestId == request.HotelDemandOnRequestId).GetAwaiter().GetResult();
                    if (updateTo == null) return new ErrorResult(Messages.RecordNotFound);
                    updateTo.Description = request.Description;
                    updateTo.OnRequestId = request.OnRequestId;
                    updateTo.IsOpen = request.IsOpen;
                    updateTo.Approved = request.Approved;
                    updateTo.ApprovingDepartmentId = updateTo.ConfirmationRequested == true ? Convert.ToInt32(JwtHelper.GetValue("departmentId").ToString()) : null;
                    updateTo.WhoApproves = updateTo.ConfirmationRequested == true ? JwtHelper.GetValue("name").ToString() : null;

                    hotelDemandOnRequestRepository.Update(updateTo);
                    hotelDemandOnRequestRepository.SaveChangesAsync().GetAwaiter().GetResult();
                    return new SuccessResult(Messages.Updated);
                });
            }
        }
    }
}
