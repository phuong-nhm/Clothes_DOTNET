// Models/Product.cs

public class Products
{  public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public required string ImageUrl { get; set; }
    public List<string> AdditionalImages { get; set; } = new List<string>(); // Hình ảnh phụ
    public string Category { get; set; } // Phân loại theo danh mục
    public string Brand { get; set; } // Phân loại theo nhãn hiệu
    public List<string> Colors { get; set; } = new List<string>(); // Danh sách màu
    public List<string> Sizes { get; set; } = new List<string>(); // Danh sách kích thước
    
    public int Stock { get; set; } // Số lượng tồn kho
    public double Rating { get; set; } // Đánh giá (số sao)
    public int Sold { get; set; } // Số lượng đã bán
    public int Views { get; set; } // Lượt xem sản phẩm
}
