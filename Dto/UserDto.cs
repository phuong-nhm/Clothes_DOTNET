using System.ComponentModel.DataAnnotations;

namespace CLothes_DOTNET.Dto
{
    /// <summary>
    /// DTO cho AppUser (ngý?i dùng) - thông tin cõ b?n
    /// Dùng cho các API tr? v? user info (Order, Checkout, Profile)
    /// </summary>
    public class UserDto
    {
        public string id { get; set; }

        public string userName { get; set; }

        public string email { get; set; }

        public string fullName { get; set; }

        public string address { get; set; }

        public string phoneNumber { get; set; }

        public DateTime? birthDate { get; set; }

        public string gender { get; set; }
    }

    /// <summary>
    /// DTO input cho update user profile
    /// </summary>
    public class UpdateUserProfileDto
    {
        [Required]
        [StringLength(255)]
        public string fullName { get; set; }

        [StringLength(500)]
        public string address { get; set; }

        [StringLength(11)]
        public string phoneNumber { get; set; }

        public DateTime? birthDate { get; set; }

        [StringLength(50)]
        public string gender { get; set; }
    }
}
