﻿// This file is auto generated by Batuhan Öztürk with Entity Creator.
// Class create date : 10.7.2014 - 09:38
// Table create Date : 10.7.2014 - 08:19
// Table Description : 

using Entities.Concrete.DomainObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("ContactRelationTypeToRelation", Schema = "CONTACT"),DataContract]
	public partial class ContactRelationTypeToRelation: DomainObjectBase
	{
		[Key,DataMember]
        public int ContactRelationTypeToRelationId { get; set; }

		[DataMember]
		public int? MainId{get;set;}

		[DataMember]
		public int? SubId{get;set;}


	}
}
