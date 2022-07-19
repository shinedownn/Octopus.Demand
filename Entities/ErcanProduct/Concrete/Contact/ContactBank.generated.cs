﻿// This file is auto generated by Batuhan Öztürk with Entity Creator.
// Class create date : 13.6.2014 - 08:38
// Table create Date : 10.6.2014 - 14:26
// Table Description : 

using Entities.Concrete.DomainObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("ContactBank", Schema = "CONTACT")]
	public partial class ContactBank: DomainObjectBase
	{
		[Key,DataMember]
		public int ContactBankId{get;set;}

	    [DataMember]
        public int? BankBranchId{get;set;}

	    [DataMember]
        public int? ContactId{get;set;}

		
		[MaxLength(50, ErrorMessage = ""),DataMember]
		public string AccountNumber{get;set;}

		
		[MaxLength(50, ErrorMessage = ""),DataMember]
		public string Iban{get;set;}

	    [DataMember]
        public int? UnitId{get;set;}

	    [DataMember]
        public bool IsDelete{get;set;}
	    [DataMember]
        public int BankId
	    {
	        get;
	        set;
	    }

	}
}