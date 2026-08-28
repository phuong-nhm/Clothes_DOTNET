using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Clothes_DOTNET.Models
{
	public class Product
	{
		[Key]
		[Column("idPd")]
		public int ID { get; set; }

        [Column("namePd")]
		[MaxLength(255)]
        [Required]

        public string name { get; set; }
        [Column("import_price")]
		public decimal importprice { get; set; }
		[Column("selling_price")]
		public decimal sellprice	{ get; set; }
		[Column("image")]
        [StringLength(500)]
        public string image { get; set; }
        [Required]

        public int category_id { get; set; }
		[ForeignKey("category_id")]
        //[JsonIgnore]

		public Category category { get; set; }
        [NotMapped]
        public ICollection<Sale> Sales { get; set; }
        public ICollection<ProductVariant> ProductVariants { get; set; }
       
    }
    public class ProductRequest
    {
        public Product Product { get; set; }
        public List<ProductVariant> ProductVariants { get; set; }
    }
}
