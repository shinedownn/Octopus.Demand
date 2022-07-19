using Entities.Concrete.DomainObjects;
using Magnus.Server.DomainObjects.Contact.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("Contact", Schema = "CONTACT")]
    public partial class Contact : DomainObjectBase
    {
        [Key]
        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        public int ContactTypeId { get; set; }

        [MaxLength(100, ErrorMessageResourceType = typeof(ContactResource), ErrorMessageResourceName = "TitleMaxLength")]
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string DeNormalizeTitle { get; set; }

        [MaxLength(100, ErrorMessageResourceType = typeof(ContactResource), ErrorMessageResourceName = "NameMaxLength")]
        [DataMember]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessageResourceType = typeof(ContactResource), ErrorMessageResourceName = "NameMaxLength")]
        [DataMember]
        public string DeNormalizeName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string DeNormalizeMiddleName { get; set; }

        [MaxLength(100, ErrorMessageResourceType = typeof(ContactResource), ErrorMessageResourceName = "SurnameMaxLength")]
        [DataMember]
        public string Surname { get; set; }

        [MaxLength(100, ErrorMessageResourceType = typeof(ContactResource), ErrorMessageResourceName = "SurnameMaxLength")]
        [DataMember]
        public string DeNormalizeSurname { get; set; }

        [DataMember]
        public Enums.GenderType? GenderTypeId { get; set; }

        [DataMember]
        public int? DiscourseId { get; set; }


        [MaxLength(10, ErrorMessage = "Vergi Numarası 10 karakterden uzun olamaz")]
        [DataMember]
        public string TaxNumber { get; set; }

        [DataMember]
        public Enums.MaritalStatusType? MaritalStatusTypeId { get; set; }

        [MaxLength(50, ErrorMessageResourceType = typeof(ContactResource), ErrorMessageResourceName = "TaxDepartmentMaxLength")]
        [DataMember]
        public string TaxDepartment { get; set; }

        [MaxLength(20, ErrorMessageResourceType = typeof(ContactResource), ErrorMessageResourceName = "TcKimlikMaxLength")]
        [RegularExpression(@"^[1-9]{1}[0-9]{9}[0,2,4,6,8]{1}$"
            , ErrorMessageResourceType = typeof(ContactResource), ErrorMessageResourceName = "InvalidTcKimlikFormat")]
        [DataMember]
        public string TcKimlik { get; set; }

        [DataMember]
        public DateTime? Birthday { get; set; }

        [DataMember]
        public bool IsDelete { get; set; }

        [DataMember]
        public bool IsFirm { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public string DeNormalizeFullName { get; set; }

        [DataMember]
        public int? CountryId { get; set; }

        [DataMember]
        public int? PasaportCountryId { get; set; }

        [MaxLength(50, ErrorMessage = "")]
        [DataMember]
        public string PasaportNo { get; set; }

        [DataMember]
        public DateTime? PasaportEndDate { get; set; }

        [DataMember]
        public string PasaportIssuedBy { get; set; }

        [DataMember]
        public DateTime? PasaportStartDate { get; set; }

        [DataMember]
        public int? PasaportTypeId { get; set; }

        [DataMember]
        public int? MilesCardTypeId { get; set; }

        [MaxLength(50, ErrorMessage = "")]
        [DataMember]
        public string MilesCardNo { get; set; }

        [MaxLength(100, ErrorMessage = "")]
        [DataMember]
        public string WebSite { get; set; }

        [MaxLength(100, ErrorMessage = "")]
        [DataMember]
        public string FacebookAccount { get; set; }

        [MaxLength(100, ErrorMessage = "")]
        [DataMember]
        public string TwitterAccount { get; set; }

        [MaxLength(100, ErrorMessage = "")]
        [DataMember]
        public string LinkedInAccount { get; set; }

        [DataMember]
        public bool SmsBlock { get; set; }

        [DataMember]
        public bool EmailBlock { get; set; }

        [DataMember]
        public bool PhoneBlock { get; set; }

        [DataMember]
        public string ImagePath { get; set; }

        [DataMember]
        public int Score { get; set; }

        [DataMember]
        public string DataSource { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public bool? IsLimitControl { get; set; }

        [DataMember]
        public decimal? CreditLimit { get; set; }

        [DataMember]
        public int? MaxPaymentDateTypeId { get; set; }

        [DataMember]
        public int? MaxPaymentDay { get; set; }
        [DataMember]
        public bool? IsNotUseRisk { get; set; }
        [DataMember]
        public bool? IsUserDataProtection { get; set; }
        [DataMember]
        public bool? IsWebLimitControl { get; set; }
        [DataMember]
        public bool? IsWebLimitControlForFlight { get; set; }
        [DataMember]
        public bool? IsWebLimitControlForPackage { get; set; }
        [DataMember]
        public bool? IsCommissionUse { get; set; }
        [DataMember]
        public bool? UseDifferentCurrency { get; set; }
        [DataMember]
        public int? MainCurrencyId { get; set; }
    }
}

