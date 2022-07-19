using Business.Constants;
using Business.Handlers.OnRequests.ValidationRules;
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
    public class DeleteOnRequestCommand : IRequest<IResult>
    {
        public int OnRequestId { get; set; }

        public class DeleteOnRequestCommandHandler : IRequestHandler<DeleteOnRequestCommand, IResult>
        {
            private readonly IOnRequestRepository onRequestRepository;
            public DeleteOnRequestCommandHandler(IOnRequestRepository onRequestRepository)
            {
                this.onRequestRepository = onRequestRepository;
            }
            [ValidationAspect(typeof(DeleteOnRequestValidator), Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger),"Sor-Sat silindi", Priority = 2)]
            public async Task<IResult> Handle(DeleteOnRequestCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() => {
                    var deleteTo = onRequestRepository.GetAsync(x => x.OnRequestId == request.OnRequestId).GetAwaiter().GetResult();
                    if (deleteTo == null) return new ErrorResult(Messages.RecordNotFound);
                    deleteTo.IsDeleted = true;
                    onRequestRepository.Update(deleteTo);
                    onRequestRepository.SaveChangesAsync().GetAwaiter();
                    return new ErrorResult(Messages.Deleted);
                }); 
            }
        }
    }
}
