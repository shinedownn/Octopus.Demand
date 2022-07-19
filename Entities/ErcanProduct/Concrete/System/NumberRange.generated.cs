using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ErcanProduct.Concrete.System
{
    [Table("NumberRange", Schema = "SYSTEM")]
    public partial class NumberRange : DomainObjectBase
    {
        [Key]
        public int NumberRangeId { get; set; }

        ////
        //
        [MaxLength(3, ErrorMessage = "")]
        public string Prefix { get; set; }

        ////
        //
        public decimal? Value { get; set; }

        ////
        //
        public int? ModulUINameId { get; set; }


    }
}
