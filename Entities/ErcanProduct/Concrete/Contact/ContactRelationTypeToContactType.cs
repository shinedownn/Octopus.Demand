﻿// This file is auto generated by Batuhan Öztürk with Entity Creator.
// Class create date : 10.7.2014 - 09:38
// Table create Date : 10.7.2014 - 08:19
// Table Description : 

using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities.ErcanProduct.Concrete.Contact
{
	public partial class ContactRelationTypeToContactType : IEntity
	{
        [NotMapped,DataMember]
	    public string ContactTypeName { get; set; }
	}
}