using System.ComponentModel.DataAnnotations;

namespace CLothes_DOTNET.Dto
{
    /// <summary>
    /// DTO cho CategoryClaim (phân m?c)
    /// </summary>
    public class CategoryClaimDto
    {
        public int id { get; set; }

        public string claimName { get; set; }
    }

    /// <summary>
    /// DTO input cho create/update CategoryClaim
    /// </summary>
    public class CreateUpdateCategoryClaimDto
    {
        [Required]
        [StringLength(255)]
        public string claimName { get; set; }
    }
}
