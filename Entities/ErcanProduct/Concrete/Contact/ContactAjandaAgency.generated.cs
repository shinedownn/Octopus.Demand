﻿// This file is auto generated by Batuhan Öztürk with Entity Creator.
// Class create date : 15.10.2014 - 09:59
// Table create Date : 15.10.2014 - 08:40
// Table Description : 

using Entities.Concrete.DomainObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("ContactAjandaAgency", Schema = "CONTACT")]
	public partial class ContactAjandaAgency: DomainObjectBase
	{
		[Key]
		public int ContactAjandaAgencyId{get;set;}

		 
		public int ContactAjandaId{get;set;}

		 
		public int AgencyId{get;set;}
	}
}
