using System.ComponentModel.DataAnnotations;

namespace CLothes_DOTNET.Dto
{
    /// <summary>
    /// DTO cho Category (danh m?c)
    /// Dùng cho Admin CRUD Category
    /// </summary>
    public class CategoryDto
    {
        public int id { get; set; }

        public string name { get; set; }

        public string gender { get; set; }

        /// <summary>
        /// ID CategoryClaim (phân m?c)
        /// </summary>
        public int category_claim_id { get; set; }

        /// <summary>
        /// Tên CategoryClaim (b? sung thông tin)
        /// </summary>
        public string claim_name { get; set; }
    }

    /// <summary>
    /// DTO dùng cho create/update Category (input)
    /// </summary>
    public class CreateUpdateCategoryDto
    {
        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [StringLength(50)]
        public string gender { get; set; }

        [Required]
        public int category_claim_id { get; set; }
    }
}
