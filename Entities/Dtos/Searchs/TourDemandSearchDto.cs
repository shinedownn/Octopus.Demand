using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Searchs
{
    public class TourDemandSearchDto : IDto
    {
        public int TourId { get; set; }
        public string Name { get; set; }
        
    }
}
