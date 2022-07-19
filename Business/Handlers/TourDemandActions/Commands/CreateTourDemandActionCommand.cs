using Business.Constants;
using Business.Handlers.TourDemandActions.ValidationRules;
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.TourDemandActions.Commands
{
    public class CreateTourDemandActionCommand : IRequest<IResult>
    { 
         public int TourDemandId { get; set; }
         public int ActionId { get; set; }

        public class CreateTourDemandActionCommandHandler : IRequestHandler<CreateTourDemandActionCommand, IResult>
        {
            private readonly ITourDemandActionRepository _TourDemandActionRepository;

            public CreateTourDemandActionCommandHandler(ITourDemandActionRepository TourDemandActionRepository)
            {
                _TourDemandActionRepository = TourDemandActionRepository;
            }
            [ValidationAspect(typeof(CreateTourDemandActionValidator), Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger),"Tur talebi oluşturuldu",Priority =3)]
            public async Task<IResult> Handle(CreateTourDemandActionCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run(() => {
                    var addedAction = new TourDemandAction()
                    {
                        TourDemandId = request.TourDemandId,
                        ActionId = request.ActionId,
                        IsOpen = true,
                        IsDeleted = false,
                        CreatedUserName = JwtHelper.GetValue("name").ToString(),
                        CreateDate = DateTime.UtcNow,
                    };
                    _TourDemandActionRepository.Add(addedAction);
                    _TourDemandActionRepository.SaveChangesAsync().GetAwaiter();
                    return new SuccessResult(Messages.Added);
                }); 
            }
        }
    }
}
