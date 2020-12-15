using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizationData.Models
{
    [Table("Organizations")]
    public class Organization : IModel
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public long VAT { get; set; }

        [Required]
        [StringLength(50)]
        public string LegalAddress { get; set; }

        [StringLength(50)]
        public string PostalAddress { get; set; }

        [DataType(DataType.Text)]
        public string Notes { get; set; }
    }
}
