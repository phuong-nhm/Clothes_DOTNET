using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Clothes_DOTNET.Models
{
    public class CategoryClaim
    {
        [Key]
        [Column("Id")]
        public int ID { get; set; }
        [Column("ClaimName")]
        [Required]
        [StringLength(255)]
        public string ClaimName { get; set; }
        public ICollection<Category> categories { get; set; }
    }
}
