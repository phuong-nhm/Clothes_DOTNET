using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Clothes_DOTNET.Models
{
    public class ProductVariant
    {
        [Key]
        [Column("Id")]
        public int VariantID { get; set; }
        [ForeignKey("ProductID")]
        public int ProductID { get; set; }
        public Product Product { get; set; }
        [ForeignKey("SizeID")]
        [Required]

        public int? SizeID { get; set; }
        public Size Size { get; set; }
        [ForeignKey("ColorID")]
        [Required]

        public int? ColorID { get; set; }
        public Color Color { get; set; }
        [Column("Quantity")]
        public int Quantity { get; set; }
        public ICollection<Order_detail> Order_Details { get; set; }

        public ICollection<Nhap> Nhaps { get; set; }

        public ICollection<Xuat> Xuats { get; set; }

        public ICollection<Ton> Tons { get; set; }
    }
}
