using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Clothes_DOTNET.Models
{
	public class Order_detail
	{
        [Key]
        [Column("idOrder_detail")]
        public int Id { get; set; }

        public int product_id { get; set; }

        [Column("price")]
        [Required]
        
        public decimal Price { get; set; }

        [Column("quantity")]
        [Required]
        public int Quantity { get; set; }
        [Column("total_money")]
        [Required]
        public decimal TotalMoney { get; set; }
        public int idOder { get; set; }
        [ForeignKey("idOder")]
        public Order order { get; set; }
        [ForeignKey("product_id")]
        public ProductVariant productvariants { get; set; }

        public Order_detail( int product_id, decimal price, int quantity, decimal totalMoney, int idOder)
        {
            this.product_id = product_id;
            Price = price;
            Quantity = quantity;
            TotalMoney = totalMoney;
            this.idOder = idOder;
        }

    }
}
