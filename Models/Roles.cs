using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLothes_DOTNET.Models
{
    [Table("Roles")]
    public class Roles
    {
        [Key]
        [Column("Id")]
        public string Id { get; set; } // Khóa chính của bảng Role
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; } // tên role

        [Required]
        [StringLength(255)]
        public string NormalizedName { get; set; }
        [Required]
        [StringLength(255)]
        public string ConcurrencyStamp { get; set; }

    }
}
