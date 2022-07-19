using Business.Constants;
using Business.Handlers.HotelDemandOnRequests.ValidationRules;
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
    public class DeleteHotelDemandOnRequestCommand : IRequest<IResult>
    {
        public int HotelDemandOnRequestId { get; set; }

        public class DeleteHotelDemandOnRequestCommandHandler : IRequestHandler<DeleteHotelDemandOnRequestCommand, IResult>
        {
            private readonly IHotelDemandOnRequestRepository hotelDemandOnRequestRepository;
            public DeleteHotelDemandOnRequestCommandHandler(IHotelDemandOnRequestRepository hotelDemandOnRequestRepository)
            {
                this.hotelDemandOnRequestRepository = hotelDemandOnRequestRepository;
            }
            [ValidationAspect(typeof(DeleteHotelDemandOnRequestValidator),Priority =1)]
            [LogAspect(typeof(PostgreSqlLogger),"Hotel talep sor-sat silindi",Priority =3)]
            public async Task<IResult> Handle(DeleteHotelDemandOnRequestCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() => {
                    var deleteTo = hotelDemandOnRequestRepository.GetAsync(x => x.HotelDemandOnRequestId == request.HotelDemandOnRequestId).GetAwaiter().GetResult();
                    if (deleteTo == null) return new ErrorResult(Messages.RecordNotFound);
                    if (deleteTo.IsOpen) return new ErrorResult(Messages.OnRequestIsOpenCannotDelete);
                    deleteTo.IsDeleted = true;
                    hotelDemandOnRequestRepository.Update(deleteTo);
                    hotelDemandOnRequestRepository.SaveChangesAsync().GetAwaiter().GetResult();
                    return new SuccessResult(Messages.Deleted);
                }); 
            }
        }
    }
}
