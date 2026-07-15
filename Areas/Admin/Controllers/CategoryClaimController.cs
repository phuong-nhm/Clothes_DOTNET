using Clothes_DOTNET.Models;
using CLothes_DOTNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System.Linq;


namespace CLothes_DOTNET.Areas.Admin.Controllers
{
    public class CategoryClaimController : Controller
    {
        private readonly MyDbContext db;
        public CategoryClaimController(MyDbContext db)
        {
            this.db = db;
        }
        [Area("Admin")]
        public IActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            ViewBag.searchString = searchString;
            var categoryclaims = db.CategoryClaims.Select(cacl => cacl);
            if (!String.IsNullOrEmpty(searchString))
            {
                categoryclaims = categoryclaims.Where(cacl=> cacl.ClaimName.Contains(searchString));
            }
            return View(categoryclaims.OrderBy(cacl => cacl.ID).ToPagedList(page, pageSize));
        }
        
        [HttpPost]
        [Route("CategoryClaim/Create")]
        public JsonResult Create([FromBody] CategoryClaim cacl)
        {
            Console.WriteLine($"ClaimName: {cacl?.ClaimName}");
            try
            {
                
                if (string.IsNullOrEmpty(cacl.ClaimName)) // Kiểm tra nếu tên danh mục trống
                {
                    return Json(new { status = false, message = "Tên danh mục không được để trống." });
                }
                
                // Kiểm tra nếu tên danh mục đã tồn tại
                bool exists = db.CategoryClaims.Any(c => c.ClaimName == cacl.ClaimName);
                if (exists)
                {
                    return Json(new { status = false, message = "Tên danh mục đã tồn tại. Vui lòng chọn tên khác." });
                }


                db.CategoryClaims.Add(cacl);
                db.SaveChanges();

                return Json(new { status = true, message = "Thêm danh mục thành công!" });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException?.Message;
                return Json(new { status = false, message = "Đã có lỗi xảy ra: " + ex.Message + " | Chi tiết: " + innerException });

            }
        }

        [HttpPost]
        [Route("/CategoryClaim/Load")]       
        public JsonResult Load([FromBody] CategoryClaim cacl)
        {   /*
            CategoryClaim cacl = db.CategoryClaim.Where(a => a.Id.Equals(id)).FirstOrDefault();
            return Json(cacl);
            */
            //Console.WriteLine($"Received id: {id}");
            var cacl1 = db.CategoryClaims.Where(a => a.ID == cacl.ID).FirstOrDefault();
            if (cacl == null)
            {
                return Json(new { status = false, message = "Không tìm thấy dữ liệu." });
            }
            return Json(new { id = cacl1.ID, claimName = cacl1.ClaimName });
        }
        
        [HttpPost]
        [Route("CategoryClaim/Update")]
        public JsonResult Update([FromBody] CategoryClaim cacl)
        {
           
            try
            {
                var update = db.CategoryClaims.FirstOrDefault(a => a.ID == cacl.ID);

                bool exists = db.CategoryClaims.Any(c => c.ClaimName == cacl.ClaimName);
                if (exists)
                {
                    return Json(new { status = false, message = "Tên phân mục đã tồn tại. Vui lòng chọn tên khác." });
                }
                update.ClaimName = cacl.ClaimName;               
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
        [Route("/CategoryClaim/Delete")]
        public JsonResult Delete([FromBody] CategoryClaim cacl)
        {
            try
            {
                var cacl1 = db.CategoryClaims.FirstOrDefault(a => a.ID == cacl.ID);

                if (cacl1 == null)
                {
                    return Json(new { status = false, message = "Không tìm thấy danh mục cần xóa." });
                }

                db.CategoryClaims.Remove(cacl1);
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
