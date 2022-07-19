using Business.Constants;
using Business.Handlers.TourDemandOnRequests.ValidationRules;
using Business.Helpers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.TourDemandOnRequests.Commands
{
    public class CreateTourDemandOnRequestCommand : IRequest<IResult>
    {
        public int OnRequestId { get; set; }
        public string Description { get; set; }
        public int MainDemandId { get; set; }
        public int TourDemandId { get; set; } 
        public bool ConfirmationRequested { get; set; }
        public int? ApprovalRequestedDepartmentId { get; set; }
        public int ApprovingDepartmentId { get; set; }
        public string WhoApproves { get; set; }

        public class CreateTourDemandOnRequestCommandHandler : IRequestHandler<CreateTourDemandOnRequestCommand, IResult>
        {
            private readonly ITourDemandOnRequestRepository tourDemandOnRequestRepository;
            public CreateTourDemandOnRequestCommandHandler(ITourDemandOnRequestRepository tourDemandOnRequestRepository)
            {
                this.tourDemandOnRequestRepository = tourDemandOnRequestRepository;
            }
            [ValidationAspect(typeof(CreateTourDemandOnRequestValidator), Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger), "Tur talebi için sor-sat oluşturuldu", Priority = 3)]
            public async Task<IResult> Handle(CreateTourDemandOnRequestCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() =>
                {
                    var createTo = new TourDemandOnRequest()
                    {
                        OnRequestId = request.OnRequestId,
                        CreateDate = DateTime.Now,
                        CreatedUserName = JwtHelper.GetValue("name").ToString(),
                        Description = request.Description,
                        IsDeleted = false,
                        IsOpen = true,
                        MainDemandId = request.MainDemandId,
                        TourDemandId = request.TourDemandId,
                        Approved = false,
                        ConfirmationRequested = request.ConfirmationRequested,
                        AskingForApprovalDepartmentId = request.ConfirmationRequested == true ? Convert.ToInt32(JwtHelper.GetValue("departmentId").ToString()) : null,
                        ApprovalRequestedDepartmentId = request.ApprovalRequestedDepartmentId,
                        ApprovingDepartmentId=request.ApprovingDepartmentId,
                        WhoApproves=request.WhoApproves
                    };
                    tourDemandOnRequestRepository.Add(createTo);
                    tourDemandOnRequestRepository.SaveChangesAsync().GetAwaiter();
                    return new SuccessResult(Messages.Added);
                });
            }
        }
    }
}
