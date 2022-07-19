using AutoMapper;
using Business.Constants;
using Business.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos.Reminder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Reminders.Queries
{
    public class GetMyRemindersQuery : IRequest<IDataResult<IEnumerable<MyReminderDto>>>
    {
        public bool IsActive { get; set; }
        public class GetMyRemindersQueryHaneler : IRequestHandler<GetMyRemindersQuery, IDataResult<IEnumerable<MyReminderDto>>>
        {
            private readonly IReminderRepository _reminderRepository;
            private readonly IHotelDemandRepository _hotelDemandRepository;
            private readonly ITourDemandRepository _tourDemandRepository;
            private readonly IHotelDemandActionRepository _hotelDemandActionRepository;
            private readonly ITourDemandActionRepository _tourDemandActionRepository;
            private readonly IActionRepository _actionRepository;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;
            public GetMyRemindersQueryHaneler(IReminderRepository reminderRepository, IMediator mediator, IHotelDemandActionRepository hotelDemandActionRepository, ITourDemandActionRepository ourDemandActionRepository, IActionRepository actionRepository, IHotelDemandRepository hotelDemandRepository, ITourDemandRepository tourDemandRepository, IMapper mapper)
            {
                _reminderRepository = reminderRepository;
                _mediator = mediator;
                _mapper = mapper;
                _hotelDemandActionRepository = hotelDemandActionRepository;
                _tourDemandActionRepository = ourDemandActionRepository;
                _actionRepository = actionRepository;
                _hotelDemandRepository = hotelDemandRepository;
                _tourDemandRepository = tourDemandRepository;
            }
            public async Task<IDataResult<IEnumerable<MyReminderDto>>> Handle(GetMyRemindersQuery request, CancellationToken cancellationToken)
            {
                var FullName = JwtHelper.GetValue("name").ToString();
                var reminders = await _reminderRepository.GetListAsync(x => x.CreatedUserName == FullName && x.IsActive==request.IsActive);

                if (!reminders.Any()) return new ErrorDataResult<IEnumerable<MyReminderDto>>(Messages.RecordNotFound);

                var hotelDemandActions = await _hotelDemandActionRepository.GetListAsync(x => reminders.Select(r => r.HotelDemandActionId).ToList().Contains(x.HotelDemandActionId));
                var tourDemandActions = await _tourDemandActionRepository.GetListAsync(x => reminders.Select(r => r.TourDemandActionId).ToList().Contains(x.TourDemandActionId));


                var query =
                            (from r in reminders
                             join hda in hotelDemandActions on r.HotelDemandActionId equals hda?.HotelDemandActionId into hotel
                             join tda in tourDemandActions on r.TourDemandActionId equals tda?.TourDemandActionId into tour
                             from hh in hotel.DefaultIfEmpty()
                             from tt in tour.DefaultIfEmpty()
                             join a in _actionRepository.Query() on hh?.ActionId equals a.ActionId into ha
                             join a1 in _actionRepository.Query() on tt?.ActionId equals a1.ActionId into ta
                             from haction in ha.DefaultIfEmpty()
                             from taction in ta.DefaultIfEmpty()
                             join t in _tourDemandRepository.Query() on tt?.TourDemandId equals t.TourDemandId into td
                             join h in _hotelDemandRepository.Query() on hh?.HotelDemandId equals h.HotelDemandId into hd
                             from tdemand in td.DefaultIfEmpty()
                             from hdemand in hd.DefaultIfEmpty()
                             where r.IsActive=request.IsActive
                             select new MyReminderDto
                             {
                                 ReminderId = r.ReminderId,
                                 HotelName = hdemand?.Name,
                                 TourName = tdemand?.Name ?? "",
                                 ActionName = haction?.Name ?? taction?.Name,
                                 CreateDate = r.CreateDate,
                                 IsActive=r.IsActive,
                                 Description=r.Description,
                                 MainDemandId=hdemand?.MainDemandId ?? tdemand?.MainDemandId,
                                 HotelDemandId=hdemand?.HotelDemandId ?? null,
                                 TourDemandId=tdemand?.TourDemandId ?? null,
                                 ReminderDate= r.ReminderDate,
                                 HotelDemandActionId=hh?.HotelDemandActionId,
                                 TourDemandActionId=tt?.TourDemandActionId
                             }).OrderByDescending(x=>x.ReminderDate).ToList(); 

                var dtos = _mapper.Map<IEnumerable<MyReminderDto>>(query);
                return new SuccessDataResult<IEnumerable<MyReminderDto>>(query);
            }
        }
    }
}
