using Clothes_DOTNET.Models;
using CLothes_DOTNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PagedList.Core;
using System.Collections.Generic;
//using System.Web.Mvc;

namespace CLothes_DOTNET.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatisticalController : Controller
    {
        private readonly MyDbContext db;
        public StatisticalController(MyDbContext db)
        {
            this.db = db;
        }
        // Phương thức thống kê đơn hàng
        public IActionResult Index(int? year)
        {
            decimal totalRevenue = db.Orders.Sum(o => o.TotalMoney);
            // Thống kê tổng hóa đơn
            int totalOrders = db.Orders.Count();
            int currentYear = year ?? DateTime.Now.Year;
            int quantity = db.Order_details.Sum(o => o.Quantity);

            // Tạo danh sách tất cả các tháng trong năm đã chọn
            var allMonths = Enumerable.Range(1, 12).Select(m => new { Year = currentYear, Month = m }).ToList();
            var monthlyRevenue = db.Orders
                    .Where(o => o.OrderDate.Year == currentYear) // Lọc theo năm
                    .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                    .Select(g => new
                    {
                        Month = g.Key.Month,
                        Year = g.Key.Year,
                        Revenue = g.Sum(o => o.TotalMoney)
                    })
                    .OrderBy(m => m.Year).ThenBy(m => m.Month)
                    .ToList();

            // Kết hợp tất cả các tháng với doanh thu, nếu không có doanh thu thì gán là 0
            var result = allMonths
                .GroupJoin(monthlyRevenue,
                    month => new { month.Year, month.Month },
                    revenue => new { revenue.Year, revenue.Month },
                    (month, revenue) => new
                    {
                        Year = month.Year,
                        Month = month.Month,
                        Revenue = revenue.Any() ? revenue.First().Revenue : 0m
                    })
                .OrderBy(m => m.Year)
                .ThenBy(m => m.Month)
                .ToList();
            var orderCountByStatus = db.Orders
                .GroupBy(o => o.Status) // Nhóm theo Status
                .Select(g => new
                {
                    Status = g.Key, // Giá trị của Status (1, 2, ...)
                    Count = g.Count() // Số lượng đơn hàng tương ứng
                })
                .ToList();
            var productTopSell = db.Order_details
               .GroupBy(o => o.product_id)
               .Select(g => new
               {
                   ProductID = g.Key,
                   QuantityPD = g.Sum(o => o.Quantity)
               })
               .Join(
                   db.Products,
                   order => order.ProductID,
                   product => product.ID,
                   (order, product) => new
                   {
                       ProductID = order.ProductID,
                       ProductName = product.name,
                       QuantityPD = order.QuantityPD,
                       Price = product.importprice
                   }
               )
               .OrderByDescending(product => product.QuantityPD)  // Sắp xếp theo QuantityPD giảm dần
               .Take(10)  // Lấy 10 sản phẩm bán chạy nhất
               .ToList();


            ViewBag.ProductTopSell = productTopSell;
            ViewBag.OderofSTT = orderCountByStatus;
            foreach (var product in productTopSell)
            {
                Console.WriteLine($"ProductID: {product.ProductID}, ProductName: {product.ProductName}, Quantity: {product.QuantityPD}");
            }

            // Truyền dữ liệu vào View
            ViewBag.MonthlyRevenue = result;
            ViewBag.SelectedYear = currentYear;
            ViewBag.TotalQuantity = quantity;

            // Đưa dữ liệu qua View
            ViewBag.TotalRevenue = totalRevenue;
            ViewBag.TotalOrders = totalOrders;
            ViewBag.MonthlyRevenue = monthlyRevenue;
            return View();
        }
    }
}
