using Clothes_DOTNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace CLothes_DOTNET.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ColorController : Controller
    {
        private readonly MyDbContext db;
        public ColorController(MyDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            ViewBag.searchString = searchString;
            var color = db.Colors.Select(c => c);
            if (!String.IsNullOrEmpty(searchString))
            {
                color = color.Where(c => c.ColorName.Contains(searchString));
            }
            return View(color.OrderBy(cacl => cacl.ColorID).ToPagedList(page, pageSize));
        }

        [HttpPost]
        [Route("/Color/Create")]
        public JsonResult Create([FromBody] Color color)
        {
            Console.WriteLine($"ClaimName: {color?.ColorName}");
            try
            {

                if (string.IsNullOrEmpty(color.ColorName)) // Kiểm tra nếu tên danh mục trống
                {
                    return Json(new { status = false, message = "Tên màu không được để trống." });
                }

                // Kiểm tra nếu tên danh mục đã tồn tại
                bool exists = db.Colors.Any(s => s.ColorName == color.ColorName);
                if (exists)
                {
                    return Json(new { status = false, message = "Tên màu đã tồn tại. Vui lòng chọn tên khác." });
                }

                db.Colors.Add(color);
                db.SaveChanges();

                return Json(new { status = true, message = "Thêm màu thành công!" });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException?.Message;
                return Json(new { status = false, message = "Đã có lỗi xảy ra: " + ex.Message + " | Chi tiết: " + innerException });

            }
        }

        [HttpPost]
        [Route("/Color/Load")]
        public JsonResult Load([FromBody] Color color)
        {   /*
            CategoryClaim cacl = db.CategoryClaim.Where(a => a.Id.Equals(id)).FirstOrDefault();
            return Json(cacl);
            */
            //Console.WriteLine($"Received id: {id}");
            var c = db.Colors.Where(a => a.ColorID == color.ColorID).FirstOrDefault();
            if (color == null)
            {
                return Json(new { status = false, message = "Không tìm thấy dữ liệu." });
            }
            return Json(new { id = c.ColorID, colorName = c.ColorName });
        }

        [HttpPost]
        [Route("Color/Update")]
        public JsonResult Update([FromBody] Color color)
        {

            try
            {
                var update = db.Colors.FirstOrDefault(a => a.ColorID == color.ColorID);

                bool exists = db.Colors.Any(c => c.ColorName == color.ColorName);
                if (exists)
                {
                    return Json(new { status = false, message = "Tên màu đã tồn tại. Vui lòng chọn tên khác." });
                }
                update.ColorName = color.ColorName;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { status = true, message = "Sửa thông tin thành công" });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException?.Message;
                return Json(new { status = false, message = "Đã có lỗi xảy ra: " + ex.Message + " | Chi tiết: " + innerException });
            }
        }



        [HttpPost]
        [Route("/Color/Delete")]
        public JsonResult Delete([FromBody] Color color)
        {
            try
            {
                var si = db.Colors.FirstOrDefault(a => a.ColorID == color.ColorID);

                if (si == null)
                {
                    return Json(new { status = false, message = "Không tìm thấy danh mục cần xóa." });
                }

                db.Colors.Remove(si);
                db.SaveChanges();
                return Json(new { status = true });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException?.Message;
                return Json(new { status = false, message = "Đã có lỗi xảy ra: " + ex.Message + " | Chi tiết: " + innerException });
            }
        }
    }
}
