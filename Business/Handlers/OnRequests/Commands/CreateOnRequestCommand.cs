using Business.Constants;
using Business.Handlers.OnRequests.ValidationRules;
using Business.Helpers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.OnRequests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.OnRequests.Commands
{
    public class CreateOnRequestCommand : IRequest<IResult>
    { 
        public string Name { get; set; }
        public int? ConfirmationDepartmentId { get; set; }
        public bool ConfirmationRequested { get; set; }

        public class CreateOnRequestCommandHandler : IRequestHandler<CreateOnRequestCommand, IResult>
        {
            private readonly IOnRequestRepository onRequestRepository;
            public CreateOnRequestCommandHandler(IOnRequestRepository onRequestRepository)
            {
                this.onRequestRepository = onRequestRepository;
            }
            [ValidationAspect(typeof(CreateOnRequestValidator),Priority =1)]
            [LogAspect(typeof(PostgreSqlLogger),"Sor-Sat eklendi",Priority =3)]
            public async Task<IResult> Handle(CreateOnRequestCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() => {
                    var onRequest = new OnRequest()
                    {
                        Name = request.Name,
                        CreateDate = DateTime.Now,
                        CreatedUserName = JwtHelper.GetValue("name").ToString(),
                        IsDeleted = false, 
                    };
                    onRequestRepository.Add(onRequest);
                    onRequestRepository.SaveChangesAsync().GetAwaiter();
                    return new SuccessResult(Messages.Added);
                }); 
            }
        }
    }
}
