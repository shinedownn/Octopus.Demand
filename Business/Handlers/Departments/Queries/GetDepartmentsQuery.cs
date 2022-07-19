
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Departments.Queries
{

    public class GetDepartmentsQuery : IRequest<IDataResult<IEnumerable<Department>>>
    {
        public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, IDataResult<IEnumerable<Department>>>
        {
            private readonly IDepartmentRepository _departmentRepository;
            private readonly IMediator _mediator;

            public GetDepartmentsQueryHandler(IDepartmentRepository departmentRepository, IMediator mediator)
            {
                _departmentRepository = departmentRepository;
                _mediator = mediator;
            }
             
             
            [LogAspect(typeof(PostgreSqlLogger))] 
            public async Task<IDataResult<IEnumerable<Department>>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Department>>(await _departmentRepository.GetListAsync());
            }
        }
    }
}