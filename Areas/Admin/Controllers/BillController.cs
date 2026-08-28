using Microsoft.AspNetCore.Mvc;
using CLothes_DOTNET.Models;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using Newtonsoft.Json;
using Clothes_DOTNET.Models;

namespace CLothes_DOTNET.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BillController : Controller
    {
        private readonly MyDbContext db;
        public BillController(MyDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            ViewBag.searchString = searchString;
            var order = db.Orders.Select(cacl => cacl);
            if (!String.IsNullOrEmpty(searchString))
            {
                order = order.Where(cacl => cacl.FullName.Contains(searchString));
            }
            return View(order.OrderBy(cacl => cacl.IDOrder).ToPagedList(page, pageSize));
        }

        [HttpPost]
        [Route("/Bill/Load")]
        public IActionResult Load([FromBody] Order cacl)
        {
            var cacl1 = db.Orders.Where(a => a.IDOrder == cacl.IDOrder).FirstOrDefault();
            if (cacl == null)
            {
                return Json(new { status = false, message = "Không tìm thấy dữ liệu." });
            }
            // Lấy danh sách sản phẩm thuộc đơn hàng
            var products = db.Order_details
                .Where(od => od.idOder == cacl1.IDOrder)
                .Join(
                    db.Products,
                    od => od.product_id,
                    p => p.ID,
                    (od, p) => new
                    {
                        // od.IDOrderDetail,
                        pdn = p.name,
                        image = p.image,
                        soluong = od.Quantity,
                        gia = od.Price,
                        tongtien = od.TotalMoney
                    }
                )
                .ToList();
                // Ghi log danh sách sản phẩm
                Console.WriteLine("Danh sách sản phẩm:");
                foreach (var product in products)
                {
                    Console.WriteLine($" Name: {product.pdn}, Image: {product.image}, Quantity: {product.soluong}, Price: {product.gia}, TotalMoney: {product.tongtien}");
                }


                return Json(new
                {
                    idOrder = cacl1.IDOrder,
                    fullName = cacl1.FullName,
                    email = cacl1.Email,
                    ngaydat = cacl1.OrderDate,
                    address = cacl1.Address,
                    sdt = cacl1.PhoneNumber,
                    status = cacl1.Status,
                    note = cacl1.Note,
                    product = products
                });

        }
        [HttpPost]
        [Route("Bill/ChangeStatus")]
      
        public JsonResult ChangeStatus(int Status, int ido)
        {
            var order = db.Orders.FirstOrDefault(o => o.IDOrder == ido);
            if (order != null)
            {
                order.Status = Status;
                db.SaveChanges();
                return Json(new { status = true });
            }
            return Json(new { status = false });
        }
    }
}