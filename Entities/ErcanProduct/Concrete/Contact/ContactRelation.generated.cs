﻿// This file is auto generated by Batuhan Öztürk with Entity Creator.
// Class create date : 10.7.2014 - 09:37
// Table create Date : 10.7.2014 - 08:19
// Table Description : 

using Entities.Concrete.DomainObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("ContactRelation", Schema = "CONTACT"),DataContract]
	public partial class ContactRelation: DomainObjectBase
	{
		[Key,DataMember]
		public int ContactRelationId { get; set; }

		[DataMember]
		public int FromContactId{get;set;}

		[DataMember]
		public int ToContactId { get; set; }

		[DataMember]
		public int FromRelationTypeId{get;set;}

		[DataMember]
		public int? ToRelationTypeId{get;set;}
		[DataMember]
		public string Description { get; set; }

	}
}
