using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Concrete.DomainObjects;
using Magnus.Server.DomainObjects.Contact.Resources;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("Discourse", Schema = "CONTACT")]
    public partial class Discourse : DomainObjectBase
    {
        [Key]
        public int DiscourseId { get; set; }

        public bool IsDelete { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(DiscourseResource), ErrorMessageResourceName = "DiscourseNameRequired")]
        [MaxLength(50, ErrorMessageResourceType = typeof(DiscourseResource), ErrorMessageResourceName = "DiscourseNameMaxLength")]
        public string DiscourseName { get; set; }

      
    }
}
