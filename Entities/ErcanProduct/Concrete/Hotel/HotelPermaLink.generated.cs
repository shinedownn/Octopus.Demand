using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ErcanProduct.Concrete.Hotel
{
    [Table("HotelPermaLink", Schema = "HOTEL")]
    public partial class HotelPermaLink : DomainObjectBase
    {
        public int HotelPermaLinkId { get; set; }
        public int HotelId { get; set; }
        public int LanguageId { get; set; }
        public string Link { get; set; } 
    }
}
