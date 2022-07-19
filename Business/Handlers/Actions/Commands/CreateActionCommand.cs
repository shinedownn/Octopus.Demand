using Business.Constants;
using Business.Handlers.Actions.Enums;
using Business.Handlers.Actions.ValidationRules;
using Business.Helpers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ServiceStack.DataAnnotations;
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
    public class CreateActionCommand : IRequest<IResult>
    { 
        public ActionTypeEnum ActionType { get; set; }
        public string Name { get; set; }
        public bool IsOpen { get; set; }

        public class CreateActionCommandHandler : IRequestHandler<CreateActionCommand, IResult>
        {
            private readonly IActionRepository actionRepository;
            public CreateActionCommandHandler(IActionRepository actionRepository)
            {
                this.actionRepository = actionRepository;
            }
            
            [ValidationAspect(typeof(CreateActionValidator),Priority =1)]
            [LogAspect(typeof(PostgreSqlLogger), "Aksiyon eklendi", Priority = 3)]
            public async Task<IResult> Handle(CreateActionCommand request, CancellationToken cancellationToken)
            {
                var action = new Action() {
                    ActionType=request.ActionType.ToString(),
                    IsDeleted=false,
                    Name=request.Name,
                    IsOpen=request.IsOpen,
                    CreatedUserName=JwtHelper.GetValue("name").ToString(),
                    CreateDate=DateTime.Now
                };
                actionRepository.Add(action);
                await actionRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}
