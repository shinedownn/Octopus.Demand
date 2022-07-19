using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [DataContract, Serializable]
    public partial class Country : IEntity 
    {
        public Country()
        {

            Translate = new List<CountryT>();
            Addresses = new HashSet<ContactAddress>();
            //Contact1 = new Contact();
        }

        [ForeignKey("UnitIdMoney")]
        public Unit MoneyUnit { get; set; }

        [DataMember]
        public ICollection<CountryT> Translate
        {
            get;
            set;
        }

        public override string ToString()
        {
            return CountryName;
        }

        [InverseProperty(nameof(ContactAddress.Country))]
        public virtual ICollection<ContactAddress> Addresses { get; set; }
    }
}
