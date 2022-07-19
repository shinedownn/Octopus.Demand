using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    public class MainDemand : IEntity
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
        public bool IsDeleted { get; set; }
        public bool IsFirm { get; set; }
        public string FirmName { get; set; }
        public string FirmTitle { get; set; }
        public string CreatedUserName { get; set; }
        public DateTime CreateDate { get; set; }

        public List<HotelDemand> HotelDemands { get; set; } = new();
        public List<TourDemand> TourDemands { get; set; } = new();

    } 
}
