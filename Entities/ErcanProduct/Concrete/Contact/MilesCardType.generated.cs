﻿// This file is auto generated by Erol Yaldır with Entity Creator.
// Class create date : 13.8.2014 - 13:29
// Table create Date : 18.7.2014 - 15:03
// Table Description : 

using Entities.Concrete.DomainObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("MilesCardType", Schema = "FLIGHT"), DataContract]
    public partial class MilesCardType : DomainObjectBase
    {
        [Key]
        [DataMember]
        public int MilesCardTypeId { get; set; }



        [MaxLength(50, ErrorMessage = "")]
        [DataMember]
        public string Name { get; set; }



        [MaxLength(4, ErrorMessage = "")]
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public bool? IsDeleted { get; set; }


    }
}