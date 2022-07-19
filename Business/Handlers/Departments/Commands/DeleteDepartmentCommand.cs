
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Departments.ValidationRules;

namespace Business.Handlers.Departments.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteDepartmentCommand : IRequest<IResult>
    {
        public int DepartmentId { get; set; }

        public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, IResult>
        {
            private readonly IDepartmentRepository _departmentRepository;
            private readonly IMediator _mediator;

            public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository, IMediator mediator)
            {
                _departmentRepository = departmentRepository;
                _mediator = mediator;
            }
             
            [ValidationAspect(typeof(DeleteDepartmentValidator),Priority =1)]
            [LogAspect(typeof(PostgreSqlLogger),Priority =2)] 
            public async Task<IResult> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
            {
                var departmentToDelete = _departmentRepository.Get(p => p.DepartmentId == request.DepartmentId);

                _departmentRepository.Delete(departmentToDelete);
                await _departmentRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

