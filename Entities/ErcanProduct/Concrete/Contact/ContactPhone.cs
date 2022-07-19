using Core.Entities;
using Entities.ErcanProduct.Concrete.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Serializable]
    public partial class ContactPhone : IEntity
    { 
        public ContactPhone()
        {
            //PhoneType = new PhoneType();
        }

        //[ForeignKey("PhoneTypeId")]
        //public PhoneType PhoneType { get; set; }

        public override string ToString()
        {
            return FullPhone;
        }

        [NotMapped]
        public string PhoneTypeName
        {
            get
            {
                if (PhoneTypeId != null)
                {
                    return Enum.GetName(typeof(PhoneTypeEnum), PhoneTypeId);
                }
                else
                {
                    return "";
                }
            }
        }

        [NotMapped]
        public string Guid { get; set; }

        [NotMapped, DataMember]
        public int? StateId { get; set; }
         
        public Contact Contact { get; set; }
    }
}
