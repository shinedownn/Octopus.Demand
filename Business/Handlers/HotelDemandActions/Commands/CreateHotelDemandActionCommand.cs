using Business.Constants;
using Business.Handlers.HotelDemandActions.ValidationRules;
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

namespace Business.Handlers.HotelDemandActions.Commands
{
    public class CreateHotelDemandActionCommand : IRequest<IResult>
    {
         public int MainDemandId { get; set; }
         public int HotelDemandId { get; set; }
         public int ActionId { get; set; }

        public class CreateHotelDemandActionCommandHandler : IRequestHandler<CreateHotelDemandActionCommand, IResult>
        {
            private readonly IHotelDemandActionRepository _HotelDemandActionRepository;

            public CreateHotelDemandActionCommandHandler(IHotelDemandActionRepository HotelDemandActionRepository)
            {
                _HotelDemandActionRepository = HotelDemandActionRepository;
            }
            [ValidationAspect(typeof(CreateHotelDemandActionValidator), Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger),"Hotel talep aksiyonu eklendi",Priority =2)]
            public async Task<IResult> Handle(CreateHotelDemandActionCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run(() => {
                    var addedAction = new HotelDemandAction()
                    {
                        HotelDemandId = request.HotelDemandId,
                        ActionId = request.ActionId,
                        IsOpen = true,
                        IsDeleted = false,
                        CreatedUserName = JwtHelper.GetValue("name").ToString(),
                        CreateDate = DateTime.Now,
                    };
                    _HotelDemandActionRepository.Add(addedAction);
                    _HotelDemandActionRepository.SaveChangesAsync().GetAwaiter().GetResult();
                    return new SuccessResult(Messages.Added);
                }); 
            }
        }
    }
}
