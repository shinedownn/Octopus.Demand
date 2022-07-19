using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ErcanProduct.Concrete.Contact
{
    [Table("Template", Schema = "CONTACT")]
    public partial class Template : IEntity
    {
        [Key]
        public int TemplateId { get; set; }
        public int ContactTypeId { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string MiddleName { get; set; }
        [StringLength(100)]
        public string Surname { get; set; }
        public int? GenderTypeId { get; set; }
        public int? DiscourseId { get; set; }
        [StringLength(20)]
        public string TaxNumber { get; set; }
        public int? MaritalStatusTypeId { get; set; }
        [StringLength(50)]
        public string TaxDepartment { get; set; }
        [StringLength(20)]
        public string TcKimlik { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }
        public int? PasaportCountryId { get; set; }
        [StringLength(10)]
        public string PasaportNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PasaportEndDate { get; set; }
        [StringLength(50)]
        public string PasaportIssuedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PasaportStartDate { get; set; }
        public int? PasaportTypeId { get; set; }
        public int? MilesCardTypeId { get; set; }
        [StringLength(50)]
        public string MilesCardNo { get; set; }
        [StringLength(100)]
        public string WebSite { get; set; }
        [StringLength(100)]
        public string FacebookAccount { get; set; }
        [StringLength(100)]
        public string TwitterAccount { get; set; }
        [StringLength(100)]
        public string LinkedInAccount { get; set; }
        [StringLength(50)]
        public string Email1 { get; set; }
        [StringLength(50)]
        public string Email2 { get; set; }
        [StringLength(30)]
        public string Telephone1 { get; set; }
        [StringLength(30)]
        public string Telephone2 { get; set; }
        [StringLength(30)]
        public string MobilePhone1 { get; set; }
        [StringLength(30)]
        public string MobilePhone2 { get; set; }
        [StringLength(30)]
        public string Fax { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public int? CountryId { get; set; }
        [StringLength(100)]
        public string DataSource { get; set; }
        [StringLength(20)]
        public string InternalPhone1 { get; set; }
        [StringLength(20)]
        public string InternalPhone2 { get; set; }
        public int? ContactId { get; set; }
        [StringLength(500)]
        public string Category { get; set; }
        public int? Gonderim { get; set; }

        [ForeignKey(nameof(ContactId))]
        [InverseProperty("Templates")]
        public virtual Contact Contact { get; set; }
        [ForeignKey(nameof(CountryId))]
        [InverseProperty("TemplateCountries")]
        public virtual Country Country { get; set; }
        [ForeignKey(nameof(PasaportCountryId))]
        [InverseProperty("TemplatePasaportCountries")]
        public virtual Country PasaportCountry { get; set; }
    }
}
