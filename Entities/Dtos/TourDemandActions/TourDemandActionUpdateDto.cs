using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.TourDemandActions.Dtos
{
    public class TourDemandActionUpdateDto : IDto
    {
        public int TourDemandActionId { get; set; }
        public int TourDemandId { get; set; }
        public string Description { get; set; }
        public int ActionId { get; set; }
        public bool IsOpen { get; set; }
    }
}
