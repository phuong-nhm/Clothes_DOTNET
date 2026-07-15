using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Clothes_DOTNET.Models
{
    public class Size
    {
        [Key]
        [Column("Id")]
        public int SizeID { get; set; }
        [Column("SizeName")]
        [MaxLength(255)]
        [Required]
      

        public string SizeName { get; set; }
        public ICollection<ProductVariant> ProductVariants { get; set; }
        public Size()
        {

            ProductVariants = new HashSet<ProductVariant>();
        }
    }
}
