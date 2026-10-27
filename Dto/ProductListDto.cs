using System.ComponentModel.DataAnnotations;

namespace CLothes_DOTNET.Dto
{
    /// <summary>
    /// DTO cho s?n ph?m trong danh sách (GetAllProduct API)
    /// Dùng ð? tr? JSON t? /Sale/GetAllProduct API, lo?i b? circular reference
    /// </summary>
    public class ProductListDto
    {
        /// <summary>
        /// ID s?n ph?m
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Tên s?n ph?m
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Giá bán
        /// </summary>
        public decimal sellprice { get; set; }

        /// <summary>
        /// Ðý?ng d?n ?nh s?n ph?m
        /// </summary>
        public string image { get; set; }

        /// <summary>
        /// ID category (ð? mapping trên client)
        /// </summary>
        public int category_id { get; set; }

        /// <summary>
        /// Tên category (thông tin b? sung, không có circular ref)
        /// </summary>
        public string category_name { get; set; }
    }

    /// <summary>
    /// DTO cho Sale object (khuy?n m?i)
    /// Tr? trong danh sách khuy?n m?i c?a API
    /// </summary>
    public class SaleDto
    {
        /// <summary>
        /// ID Sale
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// ID S?n ph?m ðý?c khuy?n m?i
        /// </summary>
        public int productId { get; set; }

        /// <summary>
        /// % gi?m giá
        /// </summary>
        public decimal discountpercentage { get; set; }

        /// <summary>
        /// Ngày b?t ð?u khuy?n m?i
        /// </summary>
        public DateTime startDate { get; set; }

        /// <summary>
        /// Ngày k?t thúc khuy?n m?i
        /// </summary>
        public DateTime endDate { get; set; }
    }

    /// <summary>
    /// DTO tr? v? t? API /Sale/GetAllProduct
    /// Ch?a danh sách s?n ph?m + thông tin phân trang + danh sách khuy?n m?i
    /// </summary>
    public class GetAllProductResponseDto
    {
        /// <summary>
        /// Danh sách s?n ph?m (không có circular reference)
        /// </summary>
        public List<ProductListDto> data { get; set; }

        /// <summary>
        /// T?ng s? s?n ph?m (sau filter)
        /// </summary>
        public int totalItems { get; set; }

        /// <summary>
        /// Trang hi?n t?i
        /// </summary>
        public int currentPage { get; set; }

        /// <summary>
        /// T?ng s? trang
        /// </summary>
        public int numberpage { get; set; }

        /// <summary>
        /// S? lý?ng s?n ph?m trên m?i trang
        /// </summary>
        public int pagesize { get; set; }

        /// <summary>
        /// Danh sách t?t c? các khuy?n m?i ðang có
        /// (Client dùng ð? check discount cho t?ng s?n ph?m)
        /// </summary>
        public List<SaleDto> objectsale { get; set; }

        public GetAllProductResponseDto()
        {
            data = new List<ProductListDto>();
            objectsale = new List<SaleDto>();
        }
    }
}
