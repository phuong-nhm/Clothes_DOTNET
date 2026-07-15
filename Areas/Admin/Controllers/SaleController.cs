using Clothes_DOTNET.Models;
using CLothes_DOTNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PagedList.Core;


namespace CLothes_DOTNET.Areas.Admin.Controllers
{
    public class SaleController : Controller
    {
        private readonly MyDbContext db;

        public SaleController(MyDbContext db)
        {
            this.db = db;
        }

        [Area("Admin")]
        public IActionResult Index(int? page)
        {
            var products = db.Products.ToList();
            ViewBag.Product = products;

            int pageSize = 5; // Số lượng mục trên mỗi trang
            int pageNumber = 1; // Nếu không có tham số trang, mặc định là 1

            var salesData = db.Sales
                .Include(s => s.product) // Load thông tin sản phẩm đi kèm
                .AsQueryable() // Đảm bảo dữ liệu là IQueryable
                .ToPagedList(pageNumber, pageSize); // Chuyển sang IPagedList

            return View(salesData); // Trả về IPagedList vào View
        }

        [HttpPost]
        [Route("/Sale/AddSale")]

        public IActionResult AddSale([FromBody] SaleRequest request)
        {
            if (request.ProductIds == null || !request.ProductIds.Any())
            {
                return Json(new { success = false, message = "Chưa chọn sản phẩm nào." });
            }

            if (request.DiscountPercentage <= 0)
            {
                return Json(new { success = false, message = "Giá trị giảm giá không hợp lệ." });
            }

            if (request.StartDate >= request.EndDate)
            {
                return Json(new { success = false, message = "Ngày bắt đầu phải trước ngày kết thúc." });
            }

            try
            {
                // Lưu thông báo trùng lặp (nếu có)
                var duplicateProducts = new List<int>();

                // Lưu từng sản phẩm với thông tin khuyến mãi
                foreach (var productId in request.ProductIds)
                {
                    // Kiểm tra trùng lặp: Sản phẩm đã có trong phiếu sale khác trong khoảng thời gian này
                    bool isDuplicate = db.Sales.Any(s =>
                        s.ProductId == productId &&
                        ((request.StartDate >= s.StartDate && request.StartDate <= s.EndDate) ||
                        (request.EndDate >= s.StartDate && request.EndDate <= s.EndDate) ||
                        (request.StartDate <= s.StartDate && request.EndDate >= s.EndDate)));

                    if (isDuplicate)
                    {
                        // Ghi nhận sản phẩm trùng
                        duplicateProducts.Add(productId);
                        continue;
                    }

                    // Nếu không trùng lặp, thêm vào bảng Sale
                    var sale = new Sale
                    {
                        ProductId = productId,
                        discountpercentage = request.DiscountPercentage,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate
                    };

                    db.Sales.Add(sale);
                }

                // Lưu tất cả thay đổi vào database
                db.SaveChanges();

                // Trả về kết quả
                if (duplicateProducts.Any())
                {
                    return Json(new
                    {
                        success = false,
                        message = $"Một số sản phẩm đã tồn tại trong phiếu sale khác: {string.Join(", ", duplicateProducts)}"
                    });
                }

                return Json(new { success = true, message = "Tạo phiếu sale thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("/Sale/LoadSale")]
        public JsonResult LoadSale(int slid)
        {
            var sale = db.Sales.Include("Product").FirstOrDefault(c => c.ID == slid);
            if (sale == null)
            {
                return Json(new { success = false, message = "Phiếu sale không tồn tại." });
            }
            return Json(new { success = true, data = sale });

        }

        [HttpPost]
        [Route("/Sale/Delete")]
        public JsonResult Delete(int id)
        {
            try
            {
                var sl = db.Sales.FirstOrDefault(s => s.ID == id);
                if (sl == null)
                {
                    return Json(new { status = false, message = "Không thể xóa phiếu sale này" });
                }
                if (sl != null)
                {
                    db.Sales.Remove(sl);
                }
                db.SaveChanges();
                return Json(new { success = true, message = "Xóa phiếu sale này thành công !" });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException?.Message;
                return Json(new { status = false, message = "Đã có lỗi xảy ra: " + ex.Message + " | Chi tiết: " + innerException });
            }
        }
    }

    

    public class SaleRequest
    {
        public List<int> ProductIds { get; set; } // Danh sách ID sản phẩm
        public decimal DiscountPercentage { get; set; } // % giảm giá
        public DateTime StartDate { get; set; } // Ngày bắt đầu
        public DateTime EndDate { get; set; } // Ngày kết thúc
    }

   
}