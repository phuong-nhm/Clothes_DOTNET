using System.ComponentModel.DataAnnotations;

namespace CLothes_DOTNET.Dto
{
    /// <summary>
    /// DTO cho Color (màu s?c)
    /// </summary>
    public class ColorDto
    {
        public int colorId { get; set; }

        public string colorName { get; set; }
    }

    /// <summary>
    /// DTO input cho create/update Color
    /// </summary>
    public class CreateUpdateColorDto
    {
        [Required]
        [StringLength(255)]
        public string colorName { get; set; }
    }
}
