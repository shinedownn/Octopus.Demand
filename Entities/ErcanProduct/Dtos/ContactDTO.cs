using Entities.ErcanProduct.Concrete.Contact;
using Entities.ErcanProduct.Concrete.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ErcanProduct.Dtos
{
    [Serializable]
    [JsonObject(NamingStrategyType = typeof(Newtonsoft.Json.Serialization.SnakeCaseNamingStrategy))]
    public class ContactDTO
    {
        public ContactDTO()
        {
            Addresses = new List<ContactAddress>();
            Emails = new List<ContactEmail>();
            Phones = new List<ContactPhone>();
            Accounts = new List<Account>();
            ContactRelations = new List<ContactRelation>();
            ToContactRelations = new List<ContactRelation>();
            MilesCardTypes = new List<ContactMilesCardType>();
            ContactSegment = new List<ContactSegment>();
        }

        public int ContactId { get; set; }
        public int ContactTypeId { get; set; }
        /// <summary>
        /// Tabela adı sadece şirketler için
        /// </summary>
        public string Title { get; set; }
        public string DeNormalizeTitle { get; set; }
        public string Name { get; set; }
        public string DeNormalizeName { get; set; }
        public string MiddleName { get; set; }
        public string DeNormalizeMiddleName { get; set; }
        public string Surname { get; set; }
        public string DeNormalizeSurname { get; set; }
        public int? GenderTypeId { get; set; }
        public int? DiscourseId { get; set; }
        public string TaxNumber { get; set; }
        public int? MaritalStatusTypeId { get; set; }
        public string TaxDepartment { get; set; }
        public string TcKimlik { get; set; }
        public DateTime? Birthday { get; set; }
        public bool IsDelete { get; set; }
        public bool? IsFirm { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ChangedDate { get; set; }
        public int? ChangedBy { get; set; }
        public int? CreatedBy { get; set; }
        public string FullName { get; set; }
        public string DeNormalizeFullName { get; set; }
        public int? CountryId { get; set; }
        public int? PasaportCountryId { get; set; }
        public string PasaportNo { get; set; }
        public DateTime? PasaportEndDate { get; set; }
        public string PasaportIssuedBy { get; set; }
        public DateTime? PasaportStartDate { get; set; }
        public int? PasaportTypeId { get; set; }
        public int? MilesCardTypeId { get; set; }
        public string MilesCardNo { get; set; }
        public string WebSite { get; set; }
        public string FacebookAccount { get; set; }
        public string TwitterAccount { get; set; }
        public string LinkedInAccount { get; set; }
        public bool? SmsBlock { get; set; }
        public bool? EmailBlock { get; set; }
        public bool? PhoneBlock { get; set; }
        public string ImagePath { get; set; }
        public int? Score { get; set; }
        public string DataSource { get; set; }
        public string Category { get; set; }
        public bool? IsLimitControl { get; set; }
        public decimal? CreditLimit { get; set; }
        public int? MaxPaymentDateTypeId { get; set; }
        public int? MaxPaymentDay { get; set; }
        public decimal? RiskBalance { get; set; }
        public decimal? RiskSaleInvoice { get; set; }
        public decimal? RiskReceivedAdvance { get; set; }
        public decimal? RiskGivenAdvance { get; set; }
        public decimal? RiskPurchaseInvoice { get; set; }
        public decimal? RiskReceivedQuarantine { get; set; }
        public decimal? RiskOffAccount { get; set; }
        public bool? IsNotUseRisk { get; set; }
        public bool? IsUserDataProtection { get; set; }
        public bool? IsWebLimitControl { get; set; }
        public bool? IsWebLimitControlForFlight { get; set; }
        public bool? IsWebLimitControlForPackage { get; set; }
        public int? PaymentDay { get; set; }
        public bool? IsCommissionUse { get; set; }
        public bool? UseDifferentCurrency { get; set; }
        public int? MainCurrencyId { get; set; }
        public Country Country { get; set; }
        public Country PasaportCountry { get; set; }
        public virtual ContactType ContactType { get; set; }
        public virtual MilesCardType MilesCardType { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<ContactAddress> Addresses { get; set; }
        public virtual ICollection<ContactAjandaContact> ContactAjandaContacts { get; set; }
        public virtual ICollection<ContactBank> ContactBanks { get; set; }
        public virtual ICollection<ContactEmail> Emails { get; set; }
        public virtual ICollection<ContactMilesCardType> MilesCardTypes { get; set; }
        public virtual ICollection<ContactPhone> Phones { get; set; }
        public virtual ICollection<ContactRelation> ContactRelations { get; set; }
        public virtual ICollection<ContactRelation> ToContactRelations { get; set; }
        public virtual ICollection<ContactSegment> ContactSegment { get; set; }

        [NotMapped]
        [DataMember]
        public string BirthdayString { get; set; }

        [NotMapped]
        [DataMember]
        public string PasaportEndDateString { get; set; }

        [NotMapped]
        [DataMember]
        public string PasaportStartDateString { get; set; }


        public string ContactTypeName
        {
            get { return Enum.GetName(typeof(ContactType), ContactTypeId); }
        }


        public override string ToString()
        {
            return this.FullName;
        }

        [NotMapped]
        [DataMember]
        public int? CallCenterPersonnelId { get; set; }

        private string _dAdres;
        [NotMapped]
        [DataMember]
        public string DefaultAdressText
        {
            get
            {
                if (Addresses != null && Addresses.Any())
                {
                    var dAdres = Addresses.FirstOrDefault(x => x.IsDefault == true);
                    if (dAdres != null)
                    {
                        _dAdres = dAdres.Address;
                    }
                }
                return _dAdres;
            }
            set
            {
                _dAdres = value;
            }
        }

        private string _dPhone;
        [NotMapped]
        [DataMember]
        public string DefaultPhoneText
        {
            get
            {
                if (Phones != null && Phones.Any())
                {
                    var phone = Phones.FirstOrDefault(x => x.Isdefault == true);
                    if (phone != null)
                    {
                        _dPhone = phone.FullPhone;
                    }
                }
                return _dPhone;
            }
            set
            {
                _dPhone = value;
            }
        }

        private string _dEmail;
        [NotMapped]
        [DataMember]
        public string DefaultEmailText
        {
            get
            {
                if (Emails != null && Emails.Any())
                {
                    var email = Emails.FirstOrDefault(x => x.IsDefault == true);
                    if (email != null)
                    {
                        _dEmail = email.Email;
                    }
                }
                return _dEmail;
            }
            set
            {
                _dEmail = value;
            }
        }
        [NotMapped]
        [DataMember]
        public string FacebookUserId { get; set; }
        [NotMapped]
        [DataMember]
        public int ExContactId { get; set; }
        [NotMapped]
        [DataMember]
        public Guid ExContact2ID { get; set; }
        [NotMapped]
        [DataMember]
        public string OldPassword { get; set; }
        [NotMapped]
        [DataMember]
        public string UserName { get; set; }

        [NotMapped]
        [DataMember]
        public string GeziPara { get; set; }

        [NotMapped]
        [DataMember]
        public int TemplateId { get; set; }

        [NotMapped]
        [DataMember]
        public string TemplateMessage { get; set; }
    }
}
