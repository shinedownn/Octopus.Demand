﻿// This file is auto generated by Batuhan Öztürk with Entity Creator.
// Class create date : 15.10.2014 - 09:59
// Table create Date : 13.10.2014 - 13:44
// Table Description : 

using Entities.Concrete.DomainObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("ContactAjandaContact", Schema = "CONTACT")]
	public partial class ContactAjandaContact: DomainObjectBase
	{
		[Key]
		public int ContactAjandaContactId{get;set;}

		 
		public int ContactAjandaId{get;set;}

		 
		public int ContactId{get;set;}

		 
		public int? AgencyId{get;set;}

        public int? AgencyPersonnelId { get; set; }

        public bool? IsReed { get; set; } //true ise okunmuştur
	}
}
