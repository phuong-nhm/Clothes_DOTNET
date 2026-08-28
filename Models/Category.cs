using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Clothes_DOTNET.Models
{
	public class Category
	{
		[Key]
		[Column("idCategory")]
		public int ID { get; set; }
		[Column("nameCategory")]
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Column("Gender")]
        [Required]
        public string Gender { get; set; }
        public int Category_claim_Id { get; set; }
        
        [ForeignKey("Category_claim_Id")]
        //[JsonIgnore]
        public CategoryClaim claim { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
