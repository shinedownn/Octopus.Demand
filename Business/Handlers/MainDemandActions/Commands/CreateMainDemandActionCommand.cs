using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.MainDemandActions.ValidationRules;
using Business.Helpers;
using Core.Aspects.Autofac.Caching;
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

namespace Business.Handlers.MainDemandActions.Commands
{
    public class CreateMainDemandActionCommand:IRequest<IResult>
    {
         public int MainDemandId { get;  set; }
         public int ActionId { get;  set; }

        public class CreateMainDemandActionCommandHandler : IRequestHandler<CreateMainDemandActionCommand, IResult>
        {
            private readonly IMainDemandActionRepository _mainDemandActionRepository;

            public CreateMainDemandActionCommandHandler(IMainDemandActionRepository mainDemandActionRepository)
            {
                _mainDemandActionRepository = mainDemandActionRepository;
            }
            [ValidationAspect(typeof(CreateMainDemandActionValidator), Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger),"Genel talep aksiyonu oluşturuldu",Priority =3)]
            public async Task<IResult> Handle(CreateMainDemandActionCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run(() => {
                    var addedAction = new MainDemandAction()
                    {
                        MainDemandId = request.MainDemandId,
                        ActionId = request.ActionId,
                        IsDeleted = false,
                        IsOpen = true,
                        CreatedUserName = JwtHelper.GetValue("name").ToString(),
                        CreateDate = DateTime.Now,
                    };
                    _mainDemandActionRepository.Add(addedAction);
                    _mainDemandActionRepository.SaveChangesAsync().GetAwaiter();
                    return new SuccessResult(Messages.Added);
                });               
            }
        }
    }
}
