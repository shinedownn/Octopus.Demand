using Core.Entities;
using Entities.Concrete;
using Entities.HotelDemands.Dtos;
using Entities.MainDemands.Dtos; 
using Entities.TourDemands.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Demands.Dtos
{
    public class DemandsDto : IDto
    {
        public MainDemandDto MainDemandDto { get; set; }
        public List<HotelDemandDto> HotelDemandDtos { get; set; } = new();
        public List<TourDemandDto> TourDemandDtos { get; set; } = new(); 
         
    }
}
