using Entities.Concrete.DomainObjects;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("PreBook", Schema = "FLIGHT")]
    public partial class PreBook : DomainObjectBase
    {
        [Key]
        public int PreBookId { get; set; }

        [MaxLength(20, ErrorMessage = "")]
        public string ProviderCode { get; set; }

        [MaxLength(20, ErrorMessage = "")]
        public string Pnr { get; set; }

        [MaxLength(50, ErrorMessage = "")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "")]
		public string Surname { get; set; }

        public DateTimeOffset? TimeLimit { get; set; }

        public string PrebookData { get; set; }
       

    }
}
