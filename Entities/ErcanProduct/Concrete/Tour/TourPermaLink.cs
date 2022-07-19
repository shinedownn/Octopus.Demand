using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ErcanProduct.Concrete.Tour
{
	[Table("TourPermaLink", Schema = "TOUR")]
	public partial class TourPermaLink : DomainObjectBase
	{
		[Key]
		public int TourPermaLinkId { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "")]
		[MaxLength(200, ErrorMessage = "")]
		public string Link { get; set; }

		public int? TourId { get; set; }

		public int LanguageId { get; set; }



	}
}
