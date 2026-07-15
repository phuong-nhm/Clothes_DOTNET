using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Clothes_DOTNET.Models
{
    public class Color
    {
        [Key]
        [Column("Id")]
        public int ColorID { get; set; }
        [Column("ColorName")]
        [Required]
        [StringLength(255)]
        public string ColorName { get; set; }
        public ICollection<ProductVariant> ProductVariants { get; set; }
        public Color()
        {
            ProductVariants = new HashSet<ProductVariant>();
        }
    }

}
