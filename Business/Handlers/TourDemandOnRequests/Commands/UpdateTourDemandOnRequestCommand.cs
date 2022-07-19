using Business.Constants;
using Business.Handlers.TourDemandOnRequests.ValidationRules;
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

namespace Business.Handlers.TourDemandOnRequests.Commands
{
    public class UpdateTourDemandOnRequestCommand : IRequest<IResult>
    {
        public int TourDemandOnRequestId { get; set; }
        public string Description { get; set; }
        public int OnRequestId { get; set; }
        public bool IsOpen { get; set; }
        public bool? Approved { get; set; }
        public bool ConfirmationRequested { get; set; } 
        public int? ApprovalRequestedDepartmentId { get; set; }
        public int ApprovingDepartmentId { get; set; } 

        public class UpdateTourDemandOnRequestCommandHandler : IRequestHandler<UpdateTourDemandOnRequestCommand, IResult>
        {
            private readonly ITourDemandOnRequestRepository tourDemandOnRequestRepository;
            public UpdateTourDemandOnRequestCommandHandler(ITourDemandOnRequestRepository tourDemandOnRequestRepository)
            {
                this.tourDemandOnRequestRepository = tourDemandOnRequestRepository;
            }
            [ValidationAspect(typeof(DeleteTourDemandOnRequestValidator), Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger), "Tur talebi için sor-sat güncellendi", Priority = 3)]
            public async Task<IResult> Handle(UpdateTourDemandOnRequestCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() =>
                {
                    var updateTo = tourDemandOnRequestRepository.GetAsync(x => x.TourDemandOnRequestId == request.TourDemandOnRequestId).GetAwaiter().GetResult();
                    if (updateTo == null) return new ErrorResult(Messages.RecordNotFound);
                    updateTo.Description = request.Description;
                    updateTo.OnRequestId = request.OnRequestId;
                    updateTo.IsOpen = request.IsOpen;
                    updateTo.Approved = request.Approved;                    
                    updateTo.ApprovingDepartmentId = updateTo.ConfirmationRequested == true ? Convert.ToInt32(JwtHelper.GetValue("departmentId").ToString()) : null;
                    updateTo.WhoApproves = updateTo.ConfirmationRequested == true ? JwtHelper.GetValue("name").ToString() : null;

                    tourDemandOnRequestRepository.Update(updateTo);
                    tourDemandOnRequestRepository.SaveChangesAsync().GetAwaiter();
                    return new SuccessResult(Messages.Updated);
                });
            }
        }
    }
}
