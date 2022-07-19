using Core.Entities;
using Entities.ErcanProduct.Concrete.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Serializable]
    public partial class ContactEmail : IEntity
    {
        public ContactEmail()
        {

        }

        [NotMapped, DataMember]
        public string Guid { get; set; }

        [NotMapped, DataMember]
        public string EmailTypeName
        {
            get => Enum.GetName(typeof(EmailTypes), EmailTypeId);
            set { }
        }
        [NotMapped, DataMember]
        public int? StateId { get; set; }
        public Contact Contact { get; set; }
    }
}
