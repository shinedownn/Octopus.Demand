
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Departments.Queries
{
    public class GetDepartmentQuery : IRequest<IDataResult<Department>>
    {
        public int DepartmentId { get; set; }

        public class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentQuery, IDataResult<Department>>
        {
            private readonly IDepartmentRepository _departmentRepository;
            private readonly IMediator _mediator;

            public GetDepartmentQueryHandler(IDepartmentRepository departmentRepository, IMediator mediator)
            {
                _departmentRepository = departmentRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(PostgreSqlLogger))] 
            public async Task<IDataResult<Department>> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
            {
                var department = await _departmentRepository.GetAsync(p => p.DepartmentId == request.DepartmentId);
                return new SuccessDataResult<Department>(department);
            }
        }
    }
}
