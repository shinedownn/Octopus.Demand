using Core.Entities;
using Entities.Dtos.HotelDemandOnRequests;
using Entities.HotelDemandActions.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.HotelDemands.Dtos
{
    public class HotelDemandDto : IDto
    { 
        public int HotelDemandId { get; set; } 
        public int HotelId { get; set; }
        public string Name { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public string Description { get; set; }
        public bool IsOpen { get; set; } 
        public string CreatedUserName { get; set; }
        public DateTime CreateDate { get; set; }
        public List<object> Actions { get; set; } = new();
        public List<object> OnRequests { get; set; } = new();

    }
}
