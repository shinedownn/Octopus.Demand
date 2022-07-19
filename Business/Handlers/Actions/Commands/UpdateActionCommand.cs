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

namespace Business.Handlers.Actions.Commands
{
    public class UpdateActionCommand : IRequest<IResult>
    {         
         public int ActionId { get;  set; }
         public string Name { get;  set; } 
         public bool IsOpen { get; set; }

        public class UpdateActionCommandHandler : IRequestHandler<UpdateActionCommand, IResult>
        {
            private readonly IActionRepository actionRepository;
            public UpdateActionCommandHandler(IActionRepository actionRepository)
            {
                this.actionRepository = actionRepository;
            }
            [ValidationAspect(typeof(UpdateActionValidator),Priority =1)]
            [LogAspect(typeof(PostgreSqlLogger),"Aksiyon güncellendi",Priority =3)]
            public async Task<IResult> Handle(UpdateActionCommand request, CancellationToken cancellationToken)
            {
                var action = await actionRepository.GetAsync(x => x.ActionId == request.ActionId);
                if (action == null) return new ErrorResult(Messages.RecordNotFound);
                action.Name = request.Name;
                action.IsOpen = request.IsOpen; 
                actionRepository.Update(action);
                await actionRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);                
            }
        }
    }
}
