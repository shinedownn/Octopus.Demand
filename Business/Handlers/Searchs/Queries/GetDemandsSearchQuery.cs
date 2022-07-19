using AutoMapper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Entities.Dtos.Searchs;
using Entities.MainDemands.Dtos;
using LinqKit;
using MediatR;
using Serialize.Linq.Extensions;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Searchs.Queries
{
    public class GetDemandsSearchQuery : IRequest<PagingResult<MainDemandSearchDto>>
    {
        public int? MainDemandId { get; set; }
        public bool IsOpen { get; set; } = true;
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DemandChannel { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool Asc { get; set; } = false;
        public bool? IsFirm { get; set; }
        public string PropertyName { get; set; } = "CreateDate";
        public string ReservationNumber { get; set; }
        public string RequestCode { get; set; }
        public string ProductName { get; set; }

        public class GetDemandsSearchQueryHandler : IRequestHandler<GetDemandsSearchQuery, PagingResult<MainDemandSearchDto>>
        {
            private readonly IMainDemandRepository _mainDemandRepository;
            private readonly IMapper _mapper;
            public GetDemandsSearchQueryHandler(IMainDemandRepository mainDemandRepository, IMapper mapper)
            {
                _mainDemandRepository = mainDemandRepository;
                _mapper = mapper;
            }

            public async Task<PagingResult<MainDemandSearchDto>> Handle(GetDemandsSearchQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run(() =>
                {
                    var predicate = PredicateBuilder.New<MainDemand>();

                    if (request.MainDemandId != null) predicate.Start(x => x.MainDemandId == request.MainDemandId); else predicate.Start(x => x.IsOpen == request.IsOpen);

                    if (!string.IsNullOrEmpty(request.RequestCode)) _ = predicate.IsStarted == true ? predicate.And(x => x.RequestCode == request.RequestCode) : predicate.Start(x => x.RequestCode == request.RequestCode);

                    if (request.IsFirm != null) predicate.And(x => x.IsFirm == request.IsFirm);

                    if (request.StartDate != null) predicate.And(x => x.CreateDate >= Convert.ToDateTime(request.StartDate.Value.ToString("yyyy-MM-dd HH:mm:sss")));

                    if (!string.IsNullOrEmpty(request.Name)) predicate.And(x => x.Name.Contains(request.Name));

                    if (!string.IsNullOrEmpty(request.Description)) predicate.And(x => x.Description == request.Description);

                    if (!string.IsNullOrEmpty(request.Surname)) predicate.And(x => x.Surname == request.Surname);

                    if (!string.IsNullOrEmpty(request.Email)) predicate.And(x => x.Email == request.Email);

                    if (!string.IsNullOrEmpty(request.PhoneNumber)) predicate.And(x => x.PhoneNumber == request.PhoneNumber);

                    if (!string.IsNullOrEmpty(request.DemandChannel)) predicate.And(x => x.DemandChannel == request.DemandChannel);

                    if (request.EndDate != null) predicate.And(x => x.CreateDate <= Convert.ToDateTime(request.EndDate.Value.ToString("yyyy-MM-dd HH:mm:sss")));

                    if (!string.IsNullOrEmpty(request.ReservationNumber)) predicate.And(x => x.ReservationNumber == request.ReservationNumber);

                    var demands = _mainDemandRepository.GetListForPaging(request.CurrentPage, request.PageSize, request.PropertyName, request.Asc, predicate, x => x.HotelDemands, x => x.TourDemands);

                    if (!string.IsNullOrEmpty(request.ProductName))
                    {
                        demands.Data = demands.Data.Where(x => x.TourDemands.Any(a => a.Name.ToLowerInvariant().Contains(request.ProductName.ToLowerInvariant())) || x.HotelDemands.Any(b => b.Name.ToLowerInvariant().Contains(request.ProductName.ToLowerInvariant()))).ToList();

                        demands.Data.ForEach(x =>
                        {
                            x.HotelDemands = x.HotelDemands.Where(h => h.Name.ToLowerInvariant().Contains(request.ProductName.ToLowerInvariant())).ToList();
                            x.TourDemands = x.TourDemands.Where(h => h.Name.ToLowerInvariant().Contains(request.ProductName.ToLowerInvariant())).ToList();
                        });
                    } 

                    var dtos = _mapper.Map<PagingResult<MainDemand>, PagingResult<MainDemandSearchDto>>(new PagingResult<MainDemand>(demands.Data,demands.Data.Count(),true, $"{demands.Data.Count} records listed.")
                     );
                    return dtos;
                });
            }
        }
    }
}
