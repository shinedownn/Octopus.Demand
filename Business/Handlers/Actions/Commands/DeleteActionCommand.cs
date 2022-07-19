using Business.Constants;
using Business.Handlers.Actions.ValidationRules;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Action = Entities.Concrete.Action;

namespace Business.Handlers.Actions.Commands
{
    public class DeleteActionCommand : IRequest<IResult>
    {
         public int ActionId { get; set; }

        public class DeleteActionCommandHandler : IRequestHandler<DeleteActionCommand, IResult>
        {
            private readonly IActionRepository actionRepository;
            private readonly IMainDemandActionRepository mainDemandActionRepository;
            private readonly IHotelDemandActionRepository hotelDemandActionRepository;
            private readonly ITourDemandActionRepository tourDemandActionRepository;
            public DeleteActionCommandHandler(IActionRepository actionRepository, IMainDemandActionRepository mainDemandActionRepository, IHotelDemandActionRepository hotelDemandActionRepository, ITourDemandActionRepository tourDemandActionRepository)
            {
                this.actionRepository = actionRepository;
                this.mainDemandActionRepository = mainDemandActionRepository;
                this.hotelDemandActionRepository = hotelDemandActionRepository;
                this.tourDemandActionRepository = tourDemandActionRepository;
            }
            [ValidationAspect(typeof(DeleteActionValidator),Priority =1)]
            [LogAspect(typeof(PostgreSqlLogger),"Aksiyon silindi",Priority =3)]
            public async Task<IResult> Handle(DeleteActionCommand request, CancellationToken cancellationToken)
            {
                var action = await actionRepository.GetAsync(x => x.ActionId == request.ActionId);
                if (action == null) return new ErrorResult(Messages.RecordNotFound);
                if(await mainDemandActionRepository.GetAsync(x=>x.ActionId==request.ActionId)!=null) return new ErrorResult(Messages.ThereIsMainDemandActionThisActionCannotDelete);
                if (await hotelDemandActionRepository.GetAsync(x => x.ActionId == request.ActionId) != null) return new ErrorResult(Messages.ThereIsHotelDemandActionThisActionCannotDelete);
                if (await tourDemandActionRepository.GetAsync(x => x.ActionId == request.ActionId) != null) return new ErrorResult(Messages.ThereIsTourDemandActionThisActionCannotDelete);

                action.IsDeleted = true;
                await actionRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}
