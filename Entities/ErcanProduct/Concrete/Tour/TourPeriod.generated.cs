using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ErcanProduct.Concrete.Tour
{
    [Table("TourPeriod", Schema = "TOUR")]
    public partial class TourPeriod : DomainObjectBase
    {
        [Key]
        public int TourPeriodId { get; set; }

        ////
        //
        public DateTime? StartDate { get; set; }

        ////
        //
        public DateTime? EndDate { get; set; }

        ////
        //
        public int? TourId { get; set; }
        public int? PaxQuota { get; set; }

        public int? TourContractId { get; set; }

        public int? UsedPaxQuota { get; set; }

        public decimal? HotelSingle { get; set; }

        public decimal? HotelDouble { get; set; }

        public decimal? HotelTriple { get; set; }

        public decimal? HotelInf { get; set; }

        public decimal? HotelChild { get; set; }

        public decimal? Guide { get; set; }

        public decimal? Transfer { get; set; }

        public decimal? Flight { get; set; }

        public decimal? Other { get; set; }

        public decimal? ReferencePax { get; set; }

        public decimal? RiskTotal { get; set; }
    }
}
