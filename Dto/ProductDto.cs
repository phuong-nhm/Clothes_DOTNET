using System.ComponentModel.DataAnnotations;

namespace CLothes_DOTNET.Dto
{
    /// <summary>
    /// DTO cho Product (s?n ph?m) - dùng cho Admin Product Management
    /// Ch?a thông tin ð?y ð?, khác v?i ProductListDto (dùng cho client listing)
    /// </summary>
    public class ProductDto
    {
        public int id { get; set; }

        public string name { get; set; }

        public decimal importprice { get; set; }

        public decimal sellprice { get; set; }

        public string image { get; set; }

        public int category_id { get; set; }

        /// <summary>
        /// Tên category (thông tin b? sung)
        /// </summary>
        public string category_name { get; set; }
    }

    /// <summary>
    /// DTO input cho create Product
    /// </summary>
    public class CreateProductDto
    {
        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [Required]
        public decimal importprice { get; set; }

        [Required]
        public decimal sellprice { get; set; }

        /// <summary>
        /// URL ho?c file path ?nh (n?u upload file th? x? l? tách)
        /// </summary>
        public string image { get; set; }

        [Required]
        public int category_id { get; set; }
    }

    /// <summary>
    /// DTO input cho update Product
    /// </summary>
    public class UpdateProductDto
    {
        [Required]
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [Required]
        public decimal importprice { get; set; }

        [Required]
        public decimal sellprice { get; set; }

        public string image { get; set; }

        [Required]
        public int category_id { get; set; }
    }

    /// <summary>
    /// DTO cho ProductVariant (bi?n th? s?n ph?m)
    /// </summary>
    public class ProductVariantDto
    {
        public int variantId { get; set; }

        public int productId { get; set; }

        public int sizeId { get; set; }

        public string sizeName { get; set; }

        public int colorId { get; set; }

        public string colorName { get; set; }

        public int quantity { get; set; }
    }

    /// <summary>
    /// DTO input cho create ProductVariant
    /// </summary>
    public class CreateProductVariantDto
    {
        [Required]
        public int productId { get; set; }

        [Required]
        public int sizeId { get; set; }

        [Required]
        public int colorId { get; set; }

        [Required]
        public int quantity { get; set; }
    }
}
