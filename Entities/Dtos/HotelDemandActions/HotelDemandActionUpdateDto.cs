using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.HotelDemandActions.Dtos
{
    public class HotelDemandActionUpdateDto : IDto
    {
        public int HotelDemandActionId { get; set; }
        public int HotelDemandId { get; set; }
        public int ActionId { get; set; }
        public string Description { get; set; }
        public bool IsOpen { get; set; }
    }
}
