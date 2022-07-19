using AutoMapper;
using Core.Utilities.Results;
using DataAccess.Abstract.ErcanProduct;
using Entities.ErcanProduct.Concrete.CallCenter;
using Entities.ErcanProduct.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ErcanProduct.CallCenters.Queries
{
    public class GetCallCenterPersonelsQuery : IRequest<IDataResult<IEnumerable<object>>>
    {
        public class GetCallCenterPersonelsQueryHandler : IRequestHandler<GetCallCenterPersonelsQuery, IDataResult<IEnumerable<object>>>
        {
            private readonly ICallCenterPersonelRepository _callCenterPersonelRepository;
            private readonly IContactRepository _contactRepository;
            private readonly IMapper _mapper;
            public GetCallCenterPersonelsQueryHandler(ICallCenterPersonelRepository callCenterPersonelRepository, IContactRepository contactRepository, IMapper mapper)
            {
                _callCenterPersonelRepository = callCenterPersonelRepository;
                _mapper = mapper;
                _contactRepository = contactRepository;
            }
            public async Task<IDataResult<IEnumerable<object>>> Handle(GetCallCenterPersonelsQuery request, CancellationToken cancellationToken)
            {
                var contactList = (from c in _contactRepository.GetList()
                                   join call in _callCenterPersonelRepository.GetList() on c.ContactId equals call.ContactId
                                   where c.IsDelete == false
                                   select new
                                   {
                                       ContactId=c.ContactId,
                                       FullName = c.FullName
                                   }
                            );
                 

                //var callCenterPersonel = await _callCenterPersonelRepository.GetListAsync(x => x.IsDeleted == false);
                //var dtos = _mapper.Map<IEnumerable<ContactDTO>>(contactList);
                return new SuccessDataResult<IEnumerable<object>>(contactList);
            }
        }
    }
}
