using Entities.Dtos.TourDemandOnRequests;
using Entities.TourDemandActions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.TourDemands.Dtos
{
    public class TourDemandUpdateDto
    {
        public int TourDemandId { get; set; }
        public int MainDemandId { get; set; }
        public int TourId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Period { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public bool IsOpen { get; set; }
        public List<TourDemandActionUpdateDto> Actions { get; set; } = new();
        public List<TourDemandOnRequestUpdateDto> Requests { get; set; } = new();
    }
}
