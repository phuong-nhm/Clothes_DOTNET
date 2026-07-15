using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clothes_DOTNET.Models
{
	public class AppUser :IdentityUser
	{
        [Column("address")]
        public string? Address { get; set; }
        [Column("FullName")]
        public string? FullName { get; set; }
        [Column("BirthDate")]
        public DateTime? BirthDate { get; set; }
        [Column("Gender")]
        public string? Gender { get; set; }
    }
}
