using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLothes_DOTNET.Models
{
    [Table("RoleClaims")]
    public class RoleClaim : IdentityRoleClaim<string>
    {
        //[Key]
        //[Column("Id")]
        //public int Id { get; set; } // Khóa chính của bảng Role
        
        //[ForeignKey("Roles")]
        //[StringLength(255)]
        //[Column("RoleId")]
        //public string RoleId { get; set; } // tên role

        //[Required]
        //[StringLength(255)]
        //public string ClaimType { get; set; }
        //[Required]
        //[StringLength(255)]
        //public string ClaimValue { get; set; }

    }
}
