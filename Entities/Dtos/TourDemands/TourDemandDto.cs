using Core.Entities;
using Entities.Dtos.TourDemandOnRequests;
using Entities.TourDemandActions.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.TourDemands.Dtos
{
    public class TourDemandDto : IDto
    { 
        public int TourDemandId { get; set; } 
        public int TourId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Period { get; set; } 
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public bool IsOpen { get; set; } 
        public string CreatedUserName { get; set; }
        public DateTime CreateDate { get; set; }
        public List<object> Actions { get; set; } = new();
        public List<object> OnRequests { get; set; } = new();

    }
}
