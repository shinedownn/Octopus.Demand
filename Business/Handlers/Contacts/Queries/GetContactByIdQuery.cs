using AutoMapper;
using Core.Utilities.Results;
using DataAccess.Abstract.ErcanProduct;
using Entities.ErcanProduct.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Contacts.Queries
{
    public class GetContactByIdQuery : IRequest<IDataResult<ContactDTO>>
    {
        public int ContactId { get; set; }

        public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, IDataResult<ContactDTO>>
        {
            private readonly IContactRepository _contactRepository;
            private readonly IMapper _mapper;
            public GetContactByIdQueryHandler(IContactRepository contactRepository, IMapper mapper)
            {
                _contactRepository = contactRepository;
                _mapper = mapper;
            }
            public async Task<IDataResult<ContactDTO>> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run(async () =>
                {
                    var contacts = await (_contactRepository.GetListAsync(x => x.ContactId==request.ContactId, new System.Linq.Expressions.Expression<System.Func<Entities.ErcanProduct.Concrete.Contact.Contact, object>>[]
                    {
                        x => x.Country,
                        x => x.MilesCardTypes,
                        x => x.Phones,
                        x => x.ContactRelations,
                        x => x.Addresses,
                        x => x.ToContactRelations,
                        x => x.ContactBanks,
                        x => x.Emails,
                        x => x.MilesCardTypes,
                        x => x.ContactSegment,
                        x => x.PasaportCountry
                    },
                        x => x.AsQueryable().Include(a => a.ContactSegment).ThenInclude(b => b.ContactSegmentType)
                    ));
                    var dto = _mapper.Map<List<ContactDTO>>(contacts.ToList())?.FirstOrDefault();
                    return new SuccessDataResult<ContactDTO>(dto);
                });
            }
        }
    }
}
