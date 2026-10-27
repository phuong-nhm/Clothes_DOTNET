using System.ComponentModel.DataAnnotations;

namespace CLothes_DOTNET.Dto
{
    /// <summary>
    /// DTO cho Sale (khuy?n m?i) - dùng cho Admin Sale Management
    /// </summary>
    public class SaleManagementDto
    {
        public int id { get; set; }

        public int productId { get; set; }

        public string productName { get; set; }

        public decimal discountpercentage { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }
    }

    /// <summary>
    /// DTO input cho create Sale
    /// </summary>
    public class CreateSaleDto
    {
        [Required]
        public List<int> productIds { get; set; }

        [Required]
        [Range(0.01, 100)]
        public decimal discountpercentage { get; set; }

        [Required]
        public DateTime startDate { get; set; }

        [Required]
        public DateTime endDate { get; set; }
    }
}
