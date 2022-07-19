using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ErcanProduct.Concrete.Tour
{
    public partial class TourPeriod : IEntity
    {
        public TourPeriod()
        {
            //TourPrices = new List<TourPrice>();
        }

        //[ForeignKey("TourPeriodId")]
        //public List<TourPrice> TourPrices { get; set; }

        //[ForeignKey("TourPeriodId")]
        //public List<TourPriceSign> TourPriceSigns { get; set; }


        //[ForeignKey("TourPeriodId")]
        //public List<TourFlight> TourFlights { get; set; }

        [NotMapped]
        public int? ReferanceId
        {
            get;
            set;
        }

        public override string ToString()
        {
            return this.StartDate.Value.ToShortDateString() + " - " + this.EndDate.Value.ToShortDateString();
        }

        #region Gün Gece Hesaplama
        [NotMapped]
        public string Night { get; set; }
        [NotMapped]
        public string Day { get; set; }

        public string DayString
        {
            get
            {
                if (!StartDate.HasValue || !EndDate.HasValue)
                {
                    return "";
                }
                int dayCount = (EndDate.Value.Date - StartDate.Value.Date).Days;
                return string.Format("{0} {1} - {2} {3}", dayCount, Night, dayCount + 1, Day);
            }
        }
        #endregion
    }
}
