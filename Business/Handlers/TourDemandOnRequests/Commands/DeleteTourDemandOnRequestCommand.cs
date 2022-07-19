using Business.Constants;
using Business.Handlers.TourDemandOnRequests.ValidationRules;
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
    public class DeleteTourDemandOnRequestCommand : IRequest<IResult>
    {
        public int TourDemandOnRequestId { get; set; }

        public class DeleteTourDemandOnRequestCommandHandler : IRequestHandler<DeleteTourDemandOnRequestCommand, IResult>
        {
            private readonly ITourDemandOnRequestRepository tourDemandOnRequestRepository;
            public DeleteTourDemandOnRequestCommandHandler(ITourDemandOnRequestRepository tourDemandOnRequestRepository)
            {
                this.tourDemandOnRequestRepository = tourDemandOnRequestRepository;
            }
            [ValidationAspect(typeof(DeleteTourDemandOnRequestValidator),Priority =1)]
            [LogAspect(typeof(PostgreSqlLogger),"Tur talebi için sor-sat silindi",Priority =3)]
            public async Task<IResult> Handle(DeleteTourDemandOnRequestCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() => {
                    var deleteTo = tourDemandOnRequestRepository.GetAsync(x => x.TourDemandOnRequestId == request.TourDemandOnRequestId).GetAwaiter().GetResult();
                    if (deleteTo == null) return new ErrorResult(Messages.RecordNotFound);
                    deleteTo.IsDeleted = true;
                    tourDemandOnRequestRepository.Update(deleteTo);
                    tourDemandOnRequestRepository.SaveChangesAsync();
                    return new SuccessResult(Messages.Deleted);
                }); 
            }
        }
    }
}
