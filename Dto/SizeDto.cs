using System.ComponentModel.DataAnnotations;

namespace CLothes_DOTNET.Dto
{
    /// <summary>
    /// DTO cho Size (kích thý?c)
    /// </summary>
    public class SizeDto
    {
        public int sizeId { get; set; }

        public string sizeName { get; set; }
    }

    /// <summary>
    /// DTO input cho create/update Size
    /// </summary>
    public class CreateUpdateSizeDto
    {
        [Required]
        [StringLength(255)]
        public string sizeName { get; set; }
    }
}
