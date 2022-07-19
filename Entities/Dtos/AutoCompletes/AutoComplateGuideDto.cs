using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.AutoCompletes.Dtos
{
    public class AutoComplateGuideDto
    { 
        public int pK { get; set; } 
        public string name { get; set; } 
        public string replacename { get; set; } 
        public string type { get; set; } 
        public string link { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public string guides { get; set; } 
        private List<int> _GuideList { get; set; }
        public List<int> GuideList
        {
            get
            {
                if (_GuideList != null && _GuideList.Any()) return _GuideList;
                var Ids = new List<int>();
                if (string.IsNullOrEmpty(guides))
                {
                    return new List<int>();
                }
                var split = guides.Split(',');
                if (split.Any())
                {
                    foreach (var s in split)
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            int i;
                            var isCast = int.TryParse(s, out i);
                            if (isCast) Ids.Add(i);
                        }
                    }
                }
                return Ids;
            }
            set { _GuideList = value; }
        }
    }
}
