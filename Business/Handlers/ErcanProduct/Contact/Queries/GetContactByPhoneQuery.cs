using AutoMapper;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
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

namespace Business.Handlers.ErcanProduct.Contact.Queries
{
    public class GetContactByPhoneQuery : IRequest<IDataResult<List<ContactDTO>>>
    {
        public string Phone { get; set; }
        public class GetContactByPhoneQueryHandler : IRequestHandler<GetContactByPhoneQuery, IDataResult<List<ContactDTO>>>
        {
            private readonly IContactRepository _contactRepository;
            private readonly IContactPhoneRepository _contactPhoneRepository;
            private readonly IContactSegmentTypeRepository _contactSegmentTypeRepository;
            private readonly IMapper _mapper;
            public GetContactByPhoneQueryHandler(IContactRepository contactRepository, IContactSegmentTypeRepository contactSegmentTypeRepository, IContactPhoneRepository contactPhoneRepository, IMapper mapper)
            {
                _contactRepository = contactRepository; 
                _contactSegmentTypeRepository = contactSegmentTypeRepository;
                _contactPhoneRepository = contactPhoneRepository;
                _mapper = mapper;
            }
            //[LogAspect(typeof(PostgreSqlLogger))]
            public async Task<IDataResult<List<ContactDTO>>> Handle(GetContactByPhoneQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run<IDataResult<List<ContactDTO>>>(async () =>
                {
                    var a = _contactPhoneRepository.GetListAsync(b => b.FullPhone == request.Phone.Trim()).GetAwaiter().GetResult();

                    if (a.Count() == 0) return new ErrorDataResult<List<ContactDTO>>(Messages.RecordNotFound);

                    var contactIds = a.Select(x => x.ContactId);

                    var data = await (_contactRepository.GetListAsync(x => contactIds.Contains(x.ContactId), new System.Linq.Expressions.Expression<System.Func<Entities.ErcanProduct.Concrete.Contact.Contact, object>>[]
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
                    var dtos = _mapper.Map<List<ContactDTO>>(data.ToList());
                    return new SuccessDataResult<List<ContactDTO>>(dtos);                    
                }); 
            }
        }
    }
}
