using AutoMapper;
using Business.Constants;
using Business.Handlers.Searchs.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.MainDemands.Dtos;
using LinqKit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Searchs.Queries
{
    public class GetDemandsSearchByContactIdQuery : IRequest<PagingResult<MainDemandDto>>
    {
        public int ContactId { get; set; }
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
        public string ProductName { get; set; }

        public class GetDemandsSearchByContactIdQueryHandler : IRequestHandler<GetDemandsSearchByContactIdQuery, PagingResult<MainDemandDto>>
        {
            private readonly IMainDemandRepository _mainDemandRepository;
            private readonly IMainDemandActionRepository _mainDemandActionRepository;
            private readonly IActionRepository _actionRepository;
            private readonly IMapper _mapper;
            public GetDemandsSearchByContactIdQueryHandler(IMainDemandRepository mainDemandRepository, IMainDemandActionRepository mainDemandActionRepository, IActionRepository actionRepository, IMapper mapper)
            {
                _mainDemandRepository = mainDemandRepository;
                _mainDemandActionRepository = mainDemandActionRepository;
                _actionRepository = actionRepository;
                _mapper = mapper;

            }
            [ValidationAspect(typeof(GetDemandsSearchByContactIdQueryValidator))]
            public async Task<PagingResult<MainDemandDto>> Handle(GetDemandsSearchByContactIdQuery request, CancellationToken cancellationToken)
            {

                return await Task.Run(() =>
                {
                    var predicate = PredicateBuilder.New<MainDemand>();

                    predicate.Start(x=>x.ContactId==request.ContactId);

                    if (request.MainDemandId != null) predicate.And(x => x.MainDemandId == request.MainDemandId); else predicate.And(x => x.IsOpen == request.IsOpen); 

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

                    var dtos = _mapper.Map<PagingResult<MainDemandDto>>(new PagingResult<MainDemand>(demands.Data,demands.Data.Count,true, $"{demands.Data.Count} records listed.") { });
                    return dtos;
                });  

                //var mainDemand = await _mainDemandRepository.GetAsync(x => x.ContactId == request.ContactId);
                //if (mainDemand == null) return new ErrorDataResult<MainDemandDto>(Messages.RecordNotFound);
                //var dto = _mapper.Map<MainDemandDto>(mainDemand);
                //dto.Actions = _mainDemandActionRepository.GetListAsync(x => x.MainDemandId == mainDemand.MainDemandId).GetAwaiter().GetResult().Select(x => new {
                //    MainDemandActionId = x.MainDemandActionId,
                //    ActionId = x.ActionId,
                //    Name = _actionRepository.GetAsync(a => a.ActionId == x.ActionId).Result.Name,
                //    CreateDate = x.CreateDate,
                //    CreatedUserName = x.CreatedUserName,
                //    IsOpen = x.IsOpen,
                //    Description = x.Description 
                //}).ToList<object>();

                //return new SuccessDataResult<MainDemandDto>(dto);
            }
        }
    }
}
