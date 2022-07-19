using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("Unit_T", Schema = "COMMON")]
    public partial class UnitT : BaseTranslateEntity
    {
        //[Key]
        //public int UnitTId { get; set; }

        //[Display(Name = "", Order ="" )]
        //
        //public int UnitId { get; set; }

        //[Display(Name = "", Order = "")]
        //
        //[Required(AllowEmptyStrings = false, ErrorMessage = "")]
        //[MaxLength(50, ErrorMessage = "")]
        //public string UnitName { get; set; }
     
        [Key]
        public int UnitTId { get; set; }

        public int UnitId { get; set; }
        
        public string UnitName { get; set; }

    }
}
