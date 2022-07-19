using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Serializable]
    [DataContract]
    public partial class Unit : IEntity
    {
        public Unit()
        {
            Translate = new List<UnitT>();
        }
        [DataMember]
        public ICollection<UnitT> Translate
        {
            get;
            set;
        }


        [ForeignKey("BaseUnitId")]
        [DataMember]
        public Unit BaseUnit { get; set; }
        public override string ToString()
        {
            return this.UnitName;
        }
    }
}

