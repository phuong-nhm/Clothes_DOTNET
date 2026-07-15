using System.Collections.Generic;

namespace Models
{
    public static class ProductData
    {
        public static List<Products> GetProducts()
        {
            return new List<Products>
            {
                new Products
                {
                    Id = 1,
                    Name = "Áo Polo Nữ Coolmax",
                    Description = "Áo polo nữ mát mẻ, thoải mái, dễ chịu.",
                    Price = 99000,
                    ImageUrl = "~/asset/nu_test_1.jpg",
                    AdditionalImages = new List<string> { "nu_test_1.jpg", "~/asset/nu_test_1.jpg" },
                    Colors = new List<string> { "Xanh Ngọc", "Vàng", "Trắng", "Đen" },
                    Sizes = new List<string> { "S", "M", "L", "XL" },
                    Stock = 15,
                    Rating = 4.8,
                    Sold = 1200,
                    Views = 500,
                    Category = "Áo", // Thêm danh mục
                    Brand = "Nữ" // Thêm thương hiệu
                },
                new Products
                {
                    Id = 2,
                    Name = "Quần Short Nam Thể Thao",
                    Description = "Quần short nam thoáng khí, phù hợp cho vận động.",
                    Price = 150000,
                    ImageUrl = "~/asset/nam_test_1.jpg",
                    AdditionalImages = new List<string> { "~/asset/nam_test_2.jpg", "~/asset/nam_test_2.jpg" },
                    Colors = new List<string> { "Xanh Dương", "Đen", "Xám" },
                    Sizes = new List<string> { "S", "M", "L", "XL", "XXL" },
                    Stock = 20,
                    Rating = 4.5,
                    Sold = 850,
                    Views = 750,
                    Category = "Quần", // Thêm danh mục
                    Brand = "Nam" // Thêm thương hiệu
                },
                new Products
                {
                    Id = 3,
                    Name = "Áo Hoodie Unisex",
                    Description = "Áo hoodie unisex thời trang, phù hợp cả nam và nữ.",
                    Price = 250000,
                    ImageUrl = "~/asset/nam_test_1.jpg",
                    AdditionalImages = new List<string> { "~/asset/nam_test_2.jpg", "~/asset/nam_test_3.jpg" },
                    Colors = new List<string> { "Xám", "Đen", "Trắng", "Đỏ" },
                    Sizes = new List<string> { "M", "L", "XL" },
                    Stock = 10,
                    Rating = 4.7,
                    Sold = 300,
                    Views = 400,
                    Category = "Áo", // Thêm danh mục
                    Brand = "Unisex" // Thêm thương hiệu
                },
                new Products
                {
                    Id = 4,
                    Name = "Áo Thun Nam Cotton 100%",
                    Description = "Áo thun nam thoải mái, chất liệu cotton mềm mại.",
                    Price = 80000,
                    ImageUrl = "~/asset/nam_test_3.jpg",
                    AdditionalImages = new List<string> { "~/asset/nam_test_2.jpg" },
                    Colors = new List<string> { "Trắng", "Đen", "Xanh Lá", "Vàng" },
                    Sizes = new List<string> { "S", "M", "L", "XL", "XXL" },
                    Stock = 25,
                    Rating = 4.3,
                    Sold = 500,
                    Views = 1200,
                    Category = "Áo", // Thêm danh mục
                    Brand = "Nam" // Thêm thương hiệu
                },
                new Products
                {
                    Id = 5,
                    Name = "Giày Thể Thao Nữ",
                    Description = "Giày thể thao nữ phong cách, thoải mái khi vận động.",
                    Price = 400000,
                    ImageUrl = "~/asset/giay_nu_1.png",
                    AdditionalImages = new List<string> { "~/asset/giay_nu_2.png", "~/asset/giay_nu_3.png" },
                    Colors = new List<string> { "Hồng", "Trắng", "Đen" },
                    Sizes = new List<string> { "36", "37", "38", "39", "40" },
                    Stock = 12,
                    Rating = 4.9,
                    Sold = 1500,
                    Views = 900,
                    Category = "Giày", // Thêm danh mục
                    Brand = "Nữ" // Thêm thương hiệu
                },
                new Products
                {
                    Id = 6,
                    Name = "Túi Tote Thời Trang",
                    Description = "Túi tote tiện dụng, phong cách trẻ trung.",
                    Price = 120000,
                    ImageUrl = "~/asset/tui_tote_1.png",
                    AdditionalImages = new List<string> { "~/asset/tui_tote_2.png" },
                    Colors = new List<string> { "Trắng", "Đen", "Be" },
                    Sizes = new List<string> { "One Size" },
                    Stock = 30,
                    Rating = 4.6,
                    Sold = 700,
                    Views = 350,
                    Category = "Áo", // Thêm danh mục
                    Brand = "Unisex" // Thêm thương hiệu
                },
                new Products
                {
                    Id = 7,
                    Name = "Mũ Lưỡi Trai Unisex",
                    Description = "Mũ lưỡi trai phong cách unisex, phù hợp mọi giới tính.",
                    Price = 60000,
                    ImageUrl = "~/asset/mu_1.png",
                    AdditionalImages = new List<string> { "~/asset/mu_2.png" },
                    Colors = new List<string> { "Xám", "Đen", "Xanh" },
                    Sizes = new List<string> { "One Size" },
                    Stock = 50,
                    Rating = 4.4,
                    Sold = 1100,
                    Views = 800,
                    Category = "Áo", // Thêm danh mục
                    Brand = "Unisex" // Thêm thương hiệu
                },
                new Products
                {
                    Id = 8,
                    Name = "Đồng Hồ Thông Minh",
                    Description = "Đồng hồ thông minh với nhiều tính năng hiện đại.",
                    Price = 1500000,
                    ImageUrl = "~/asset/dong_ho_1.png",
                    AdditionalImages = new List<string> { "~/asset/dong_ho_2.png", "~/asset/dong_ho_3.png" },
                    Colors = new List<string> { "Đen", "Bạc" },
                    Sizes = new List<string> { "One Size" },
                    Stock = 5,
                    Rating = 4.8,
                    Sold = 300,
                    Views = 1500,
                    Category = "Áo", // Thêm danh mục
                    Brand = "Unisex" // Thêm thương hiệu
                }
                // Thêm các sản phẩm còn lại...
            };
        }
    }
}
