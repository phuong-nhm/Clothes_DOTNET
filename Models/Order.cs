using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Clothes_DOTNET.Models
{
	public class Order
	{
		[Key]
        [Column("idOder")]  
        public int IDOrder { get; set; } 

        [Required]
        [StringLength(255)]

        public string FullName { get; set; } 

        [Required]
        [StringLength(50)]
        public string Email { get; set; }  

        [Required]
        [StringLength(11)]
        public string PhoneNumber { get; set; }  

        [Required]
        [StringLength(255)]
        public string Address { get; set; } 

        [StringLength(255)]
        public string Note { get; set; }  

        [Required]
        [Column("order_date")] 
        public DateTime OrderDate { get; set; }  

        [Required]
        public int Status { get; set; } 

        [Required]
        [Column("total_money")]  
        public decimal TotalMoney { get; set; }  

        [Required]
        [StringLength(450)]
        [Column("user_id")] 
        public string UserID { get; set; } 
       
		[ForeignKey("UserID")]
		public AppUser User { get; set; }

        public ICollection<Order_detail> order_details { get; set; }

        public Order( string fullName, string email, string phoneNumber, string address, string note, DateTime orderDate, int status, decimal totalMoney, string userID)
        {
  
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Note = note;
            OrderDate = orderDate;
            Status = status;
            TotalMoney = totalMoney;
            UserID = userID;
        }
    }
}
