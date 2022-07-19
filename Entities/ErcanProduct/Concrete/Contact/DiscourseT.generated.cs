using Magnus.Server.DomainObjects.Contact.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("Discourse_T", Schema = "CONTACT")]
    public partial class DiscourseT : BaseTranslateEntity
    {
        [Key]
        public int DiscourseTId { get; set; }
        public int DiscourseId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DiscourseResource), ErrorMessageResourceName = "DiscourseNameRequired")]
        [MaxLength(50, ErrorMessageResourceType = typeof(DiscourseResource), ErrorMessageResourceName = "DiscourseNameMaxLength")]
        public string DiscourseName { get; set; }
    }
}
