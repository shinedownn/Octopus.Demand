
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Departments.ValidationRules;
using Business.Helpers;
using System;

namespace Business.Handlers.Departments.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateDepartmentCommand : IRequest<IResult>
    {

        public string Name { get; set; } 


        public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, IResult>
        {
            private readonly IDepartmentRepository _departmentRepository;
            private readonly IMediator _mediator;
            public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IMediator mediator)
            {
                _departmentRepository = departmentRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateDepartmentValidator), Priority = 1)] 
            [LogAspect(typeof(PostgreSqlLogger))] 
            public async Task<IResult> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
            {
                var isThereDepartmentRecord = _departmentRepository.Query().Any(u => u.Name == request.Name);

                if (isThereDepartmentRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedDepartment = new Department
                {
                    Name = request.Name,
                    CreateDate = DateTime.Now,
                    CreatedUserName = JwtHelper.GetValue("name").ToString(), 
                };

                _departmentRepository.Add(addedDepartment);
                await _departmentRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}