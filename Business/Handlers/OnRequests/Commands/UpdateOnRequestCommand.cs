using Business.Constants;
using Business.Handlers.OnRequests.ValidationRules;
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

namespace Business.Handlers.OnRequests.Commands
{
    public class UpdateOnRequestCommand : IRequest<IResult>
    {
        public int OnRequestId { get; set; }
        public string Name { get; set; }
        public bool ConfirmationRequested { get; set; }
        public bool? Confirmed { get; set; }
        public int? ConfirmationDepartmentId { get; set; }

        public class UpdateOnRequestCommandHandler : IRequestHandler<UpdateOnRequestCommand, IResult>
        {
            private readonly IOnRequestRepository onRequestRepository;
            public UpdateOnRequestCommandHandler(IOnRequestRepository onRequestRepository)
            {
                this.onRequestRepository = onRequestRepository;
            }
            [ValidationAspect(typeof(UpdateOnRequestValidator), Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger), "Sor-Sat güncellendi", Priority = 2)]
            public async Task<IResult> Handle(UpdateOnRequestCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() =>
                {
                    var updateTo = onRequestRepository.GetAsync(x => x.OnRequestId == request.OnRequestId).GetAwaiter().GetResult();
                    if (updateTo == null) return new ErrorResult(Messages.RecordNotFound);
                    updateTo.Name = request.Name; 
                    onRequestRepository.Update(updateTo);
                    onRequestRepository.SaveChangesAsync().GetAwaiter();
                    return new SuccessResult(Messages.Updated);
                });
            }
        }
    }
}
