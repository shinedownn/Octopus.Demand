using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.HotelDemands
{
    public class HotelDemandUpsertDto : IDto
    {
        public int? HotelDemandId { get; set; }
        public int? MainDemandId { get; set; }
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
        public List<HotelDemandAction> Actions { get; set; } = new();
        public List<HotelDemandOnRequest> OnRequests { get; set; } = new();
    }
}
