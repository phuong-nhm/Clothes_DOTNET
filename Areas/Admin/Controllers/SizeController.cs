using Clothes_DOTNET.Models;
using CLothes_DOTNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PagedList.Core;

namespace CLothes_DOTNET.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizeController : Controller
    {
        private readonly MyDbContext db;
        public SizeController(MyDbContext db)
        {
            this.db = db;
        }


        public IActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            ViewBag.searchString = searchString;
            var size = db.Sizes.Select(s => s);
            if (!String.IsNullOrEmpty(searchString))
            {
                size = size.Where(s => s.SizeName.Contains(searchString));
            }
            return View(size.OrderBy(cacl => cacl.SizeID).ToPagedList(page, pageSize));
        }

        [HttpPost]
        [Route("/Size/Create")]
        public JsonResult Create([FromBody] Size size)
        {
            Console.WriteLine($"ClaimName: {size?.SizeName}");
            try
            {

                if (string.IsNullOrEmpty(size.SizeName)) // Kiểm tra nếu tên danh mục trống
                {
                    return Json(new { status = false, message = "Tên Size không được để trống." });
                }

                // Kiểm tra nếu tên danh mục đã tồn tại
                bool exists = db.Sizes.Any(s => s.SizeName == size.SizeName);
                if (exists)
                {
                    return Json(new { status = false, message = "Tên Size đã tồn tại. Vui lòng chọn tên khác." });
                }

                db.Sizes.Add(size);
                db.SaveChanges();

                return Json(new { status = true, message = "Thêm Size thành công!" });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException?.Message;
                return Json(new { status = false, message = "Đã có lỗi xảy ra: " + ex.Message + " | Chi tiết: " + innerException });

            }
        }

        [HttpPost]
        [Route("/Size/Load")]
        public JsonResult Load([FromBody] Size size)
        {   /*
            CategoryClaim cacl = db.CategoryClaim.Where(a => a.Id.Equals(id)).FirstOrDefault();
            return Json(cacl);
            */
            //Console.WriteLine($"Received id: {id}");
            var s = db.Sizes.Where(a => a.SizeID == size.SizeID).FirstOrDefault();
            if (s == null)
            {
                return Json(new { status = false, message = "Không tìm thấy dữ liệu." });
            }
            return Json(new { id = s.SizeID, sizeName = s.SizeName });
        }

        [HttpPost]
        [Route("Size/Update")]
        public JsonResult Update([FromBody] Size size)
        {

            try
            {
                var update = db.Sizes.FirstOrDefault(a => a.SizeID == size.SizeID);

                bool exists = db.Sizes.Any(c => c.SizeName == size.SizeName);
                if (exists)
                {
                    return Json(new { status = false, message = "Tên Size đã tồn tại. Vui lòng chọn tên khác." });
                }
                update.SizeName = size.SizeName;
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
        [Route("/Size/Delete")]
        public JsonResult Delete([FromBody] Size size)
        {
            try
            {
                var si = db.Sizes.FirstOrDefault(a => a.SizeID == size.SizeID);

                if (si == null)
                {
                    return Json(new { status = false, message = "Không tìm thấy danh mục cần xóa." });
                }

                db.Sizes.Remove(si);
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
