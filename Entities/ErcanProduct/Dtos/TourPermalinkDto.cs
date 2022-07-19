using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ErcanProduct.Dtos
{
    public class TourPermalinkDto : IDto
    {
        public string Type { get; set; }
        public int PK { get; set; }
        public string Permalink { get; set; }
        public string Title{ get; set; }
    }
}
