using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.OnRequestApprovements.Queries
{
    public class GetOnRequestApprovementByOnRequestIdQuery : IRequest<IDataResult<IEnumerable<Department>>>
    {
        public int OnRequestId { get; set; }

        public class GetOnRequestApprovementByOnRequestIdQueryHandler : IRequestHandler<GetOnRequestApprovementByOnRequestIdQuery, IDataResult<IEnumerable<Department>>>
        {
            private readonly IOnRequestApprovementRepository _onRequestApprovementRepository;
            private readonly IDepartmentRepository _departmentRepository;
            private readonly IMediator _mediator;
            public GetOnRequestApprovementByOnRequestIdQueryHandler(IOnRequestApprovementRepository onRequestApprovementRepository, IDepartmentRepository departmentRepository, IMediator mediator)
            {
                _onRequestApprovementRepository = onRequestApprovementRepository; 
                _departmentRepository = departmentRepository;
                _mediator = mediator;
            }
            public async Task<IDataResult<IEnumerable<Department>>> Handle(GetOnRequestApprovementByOnRequestIdQuery request, CancellationToken cancellationToken)
            {
                var departmentIds = _onRequestApprovementRepository.GetListAsync(x => x.OnRequestId == request.OnRequestId).Result.Select(x=>x.DepartmentId);
                var departments = await _departmentRepository.GetListAsync(x => departmentIds.Contains(x.DepartmentId)); 
                return new SuccessDataResult<IEnumerable<Department>>(departments);
            }
        }
    }
}
