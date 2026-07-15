using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Clothes_DOTNET.Models
{
    public class Ton
    {
        [Key]
        [Column("Id")]
        public int ID { get; set; }
        [Column("quantity")]
        public int quantity { get; set; }
        public int product_id { get; set; }
        [ForeignKey("product_id")]
        public ProductVariant productvariants { get; set; }
    }
}
