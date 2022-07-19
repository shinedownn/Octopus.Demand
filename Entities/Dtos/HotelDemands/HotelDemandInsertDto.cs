using Core.Entities;
using Entities.Dtos.HotelDemandOnRequests;
using Entities.HotelDemandActions.Dtos; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.HotelDemands.Dtos
{
    public class HotelDemandInsertDto : IDto
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public string Description { get; set; } 
        public List<HotelDemandActionInsertDto> Actions { get; set; } = new();
        public List<HotelDemandOnRequestInsertDto> OnRequests { get; set; } = new();
    }
}
