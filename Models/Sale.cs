using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Clothes_DOTNET.Models
{
    public class Sale
    {
        [Key]
        [Column("SaleId")]
        public int ID { get; set; }
        [Column("DiscountPercentage")]
        public decimal discountpercentage { get; set; }
        [Column("StartDate")]
        public DateTime StartDate { get; set; }
        [Column("EndDate")]
        public DateTime EndDate { get; set; }
        [Required]
        [StringLength(255)]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [JsonIgnore]
        public Product product { get; set; }

    }
}
