using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ElasticSearchModel : IDto
    {
        public int pK { get; set; }
        public string type { get; set; }
        public string link { get; set; }
        public string name { get; set; }
        public string replacename { get; set; }
        public int productId { get; set; }
        public string picture { get; set; }
        public string loc { get; set; }
        public object[] channelList { get; set; }
        public object[] showAgencyList { get; set; }
        public int orderId { get; set; }
    }
}
