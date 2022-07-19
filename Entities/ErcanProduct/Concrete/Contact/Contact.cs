using Core.Entities; 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Serializable]
    [DataContract]
    public partial class Contact : IEntity
    {
        public Contact()
        {
            Addresses = new List<ContactAddress>();
            Emails = new List<ContactEmail>();
            Phones = new List<ContactPhone>();
            Accounts = new List<Account>();
            ContactRelations = new List<ContactRelation>();
            ToContactRelations = new List<ContactRelation>();
            MilesCardTypes = new List<ContactMilesCardType>();
            ContactSegment = new List<ContactSegment>();
        }

        //[ForeignKey("CountryId")]
        [DataMember]
        public Country Country { get; set; }
        //[ForeignKey("CountryId")]
        [DataMember]
        public Country PasaportCountry { get; set; }
        [ForeignKey("MilesCardTypeId")]
        [DataMember]
        public MilesCardType MilesCardType { get; set; }
        [ForeignKey("ContactId")]
        [DataMember]
        public List<ContactPhone> Phones { get; set; }
        //[ForeignKey("ContactId")]
        [DataMember]
        public List<ContactAddress> Addresses { get; set; }
        //[ForeignKey("ContactId")]
        [DataMember]
        public List<ContactEmail> Emails { get; set; }
        //[ForeignKey("ContactId")]
        [DataMember]
        public List<ContactBank> ContactBanks { get; set; }
        //[ForeignKey("ContactId")]
        [DataMember]
        public List<Account> Accounts { get; set; }
        //[ForeignKey("ContactId")]
        [DataMember]
        public List<ContactSegment> ContactSegment { get; set; }
        //[ForeignKey("FromContactId")]
        [DataMember]
        public List<ContactRelation> ContactRelations { get; set; }

        //[ForeignKey("ToContactId")]
        [DataMember]
        public List<ContactRelation> ToContactRelations { get; set; }

        //[ForeignKey("ContactId")]
        [DataMember]
        public List<ContactMilesCardType> MilesCardTypes { get; set; }
    }
}
