using Core.Entities;
using Entities.Dtos.Searchs;
using Entities.MainDemandActions.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.MainDemands.Dtos
{
    public class MainDemandDto : IDto
    {
        public int MainDemandId { get; set; }
        public string RequestCode { get; set; }
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CountryCode { get; set; }
        public string AreaCode { get; set; }
        public string PhoneNumber { get; set; }
        public string FullPhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string DemandChannel { get; set; }
        public string ReservationNumber { get; set; }
        public bool IsOpen { get; set; }
        public bool IsFirm { get; set; }
        public string FirmName { get; set; }
        public string FirmTitle { get; set; }
        public string CreatedUserName { get; set; }
        public DateTime CreateDate { get; set; }
        public List<object> Actions { get; set; } = new();
        public List<HotelDemandSearchDto> HotelDemands { get; set; }
        public List<TourDemandSearchDto> TourDemands { get; set; }
    }
}
