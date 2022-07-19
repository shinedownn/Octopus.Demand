using AutoMapper;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using Entities.Actions.Dtos;
using Entities.Concrete;
using Entities.Dtos; 
using Entities.Dtos.HotelDemandOnRequests;
using Entities.Dtos.Reminder;
using Entities.Dtos.RequestChannel;
using Entities.Dtos.Searchs;
using Entities.Dtos.TourDemandOnRequests;
using Entities.ErcanProduct.Concrete.CallCenter;
using Entities.ErcanProduct.Concrete.Contact;
using Entities.ErcanProduct.Concrete.Hotel;
using Entities.ErcanProduct.Concrete.Tour;
using Entities.ErcanProduct.Dtos;
using Entities.HotelDemandActions.Dtos;
using Entities.HotelDemands.Dtos;
using Entities.MainDemandActions.Dtos;
using Entities.MainDemands.Dtos; 
using Entities.TourDemandActions.Dtos;
using Entities.TourDemands.Dtos;
using System.Collections.Generic;

namespace Business.Helpers
{
    public class AutoMapperHelper : Profile
    { 
        public AutoMapperHelper()
        { 
            CreateMap<MainDemand, MainDemandDto>().ReverseMap(); 
            CreateMap<MainDemand, MainDemandSearchDto>().ReverseMap(); 
            CreateMap<PagingResult<MainDemand>, PagingResult<MainDemandSearchDto>>().ReverseMap();
            CreateMap<PagingResult<MainDemand>, PagingResult<MainDemandDto>>().ReverseMap();
            CreateMap<MainDemandAction, MainDemandActionDto>().ReverseMap();
            CreateMap<HotelDemand, HotelDemandDto>().ReverseMap();
            CreateMap<HotelDemand, HotelDemandSearchDto>().ReverseMap();
            CreateMap<TourDemand, TourDemandDto>().ReverseMap(); 
            CreateMap<TourDemand, TourDemandSearchDto>().ReverseMap(); 
            CreateMap<HotelDemandOnRequest, HotelDemandOnRequestDto>().ReverseMap(); 
            CreateMap<TourDemandOnRequest, TourDemandOnRequestDto>().ReverseMap(); 
            CreateMap<HotelDemandAction, HotelDemandActionDto>().ReverseMap();
            CreateMap<TourDemandAction, TourDemandActionDto>().ReverseMap();
            CreateMap<Action, ActionDto>().ReverseMap();
            CreateMap<TourPeriod, TourAvailablePeriod>().ReverseMap(); 
            CreateMap<Entities.Concrete.RequestChannel, Entities.Dtos.RequestChannel.RequestChannelDto>().ReverseMap();
            CreateMap<Reminder, ReminderDto>().ReverseMap();
            CreateMap<Contact,ContactDTO>().ReverseMap();
            CreateMap<HotelPermaLink, HotelPermaLinkDto>().ReverseMap();
            CreateMap<CallCenterPersonel, CallCenterPersonelDto>().ReverseMap();
        }
    }
}
