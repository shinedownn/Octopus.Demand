using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ErcanProduct.Concrete.Tour
{
    public partial class Tour: IEntity
    {
        public Tour()
        { 
            TourPermaLinks = new List<TourPermaLink>();
             
            if (string.IsNullOrEmpty(this.TourPermalink))
                this.TourPermalink = string.Empty;
        }
          
         
        [ForeignKey("TourId")]
        public List<TourPermaLink> TourPermaLinks { get; set; }
             
         
        [NotMapped]
        public string TourPermalink
        {
            get;
            set;
        }
    }
}
