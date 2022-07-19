using Core.Entities;
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
    public class DemandsInsertDto : IDto
    {
        public MainDemandInsertDto MainDemandDto { get; set; }
        public List<HotelDemandInsertDto> HotelDemandDtos { get; set; } = new();
        public List<TourDemandInsertDto> TourDemandDtos { get; set; } = new(); 
    }
}
