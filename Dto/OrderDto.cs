using System.ComponentModel.DataAnnotations;

namespace CLothes_DOTNET.Dto
{
    /// <summary>
    /// DTO cho Order (ðõn hàng) - dùng cho Admin Bill Management
    /// </summary>
    public class OrderDto
    {
        public int idOrder { get; set; }

        public string fullName { get; set; }

        public string email { get; set; }

        public string phoneNumber { get; set; }

        public string address { get; set; }

        public string note { get; set; }

        public DateTime orderDate { get; set; }

        /// <summary>
        /// Status: 1 = Ch? xác nh?n, 2 = Ðang g?i, 3 = Ð? giao, 4 = Ð? h?y, etc.
        /// </summary>
        public int status { get; set; }

        public decimal totalMoney { get; set; }

        public string userId { get; set; }
    }

    /// <summary>
    /// DTO cho Order Detail (chi ti?t ðõn hàng)
    /// </summary>
    public class OrderDetailDto
    {
        public int id { get; set; }

        public int productId { get; set; }

        public string productName { get; set; }

        public string productImage { get; set; }

        public decimal price { get; set; }

        public int quantity { get; set; }

        public decimal totalMoney { get; set; }

        public int orderId { get; set; }
    }

    /// <summary>
    /// DTO cho Order with details (ðõn hàng + danh sách s?n ph?m)
    /// </summary>
    public class OrderWithDetailsDto
    {
        public int idOrder { get; set; }

        public string fullName { get; set; }

        public string email { get; set; }

        public string phoneNumber { get; set; }

        public string address { get; set; }

        public string note { get; set; }

        public DateTime orderDate { get; set; }

        public int status { get; set; }

        public decimal totalMoney { get; set; }

        public List<OrderDetailDto> details { get; set; }

        public OrderWithDetailsDto()
        {
            details = new List<OrderDetailDto>();
        }
    }

    /// <summary>
    /// DTO input cho update Order Status (thay ð?i tr?ng thái ðõn hàng)
    /// </summary>
    public class UpdateOrderStatusDto
    {
        [Required]
        public int orderId { get; set; }

        [Required]
        public int status { get; set; }
    }
}
