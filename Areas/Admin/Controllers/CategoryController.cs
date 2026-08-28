using Clothes_DOTNET.Models;
using CLothes_DOTNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PagedList.Core;
//using System.Web.Mvc;

namespace CLothes_DOTNET.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoryController : Controller
    {
        private readonly MyDbContext db;
        public CategoryController(MyDbContext db)
        {
            this.db = db;
        }      
    
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            ViewBag.searchString = searchString;
            ViewBag.CategoryClaim = db.CategoryClaims.Select(d => d);
            var danhmucs = db.Categories.Include(c=> c.claim).Select(p => p);
            if (!String.IsNullOrEmpty(searchString))
            {
                danhmucs = danhmucs.Where(sp => sp.Name.Contains(searchString));
            }
            return View(danhmucs.OrderBy(sp => sp.ID).ToPagedList(page, pageSize));
        }   

    [HttpPost]
        [Route("Category/Create")]
        public JsonResult Create([FromBody] Category dm)
        {
            Console.WriteLine($"NameCategory: {dm?.Name}");
            try
            {   
                if (string.IsNullOrEmpty(dm.Name)) // Kiểm tra nếu tên danh mục trống
                {
                    return Json(new { status = false, message = "Tên danh mục không được để trống." });
                }
                
                // Kiểm tra nếu tên danh mục đã tồn tại
                bool exists = db.Categories.Any(c => c.Name == dm.Name);
                if (exists)
                {
                    return Json(new { status = false, message = "Tên danh mục đã tồn tại. Vui lòng chọn tên khác." });
                }
                

                db.Categories.Add(dm);
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
        [Route("Category/GetCategoryById")]
        public JsonResult GetCategoryById(int id)
        {
            var category = db.Categories.FirstOrDefault(a => a.ID == id);

            if (category == null)
            {
                return Json(new { success = false, message = "Category not found." });
            }

            var result = new
            {
                IDCategory = category.ID,
                NameCategory = category.Name,
                CategoryClaimID = category.Category_claim_Id,
                Gender = category.Gender
            };

            Console.WriteLine("Dữ liệu trả về: " + JsonConvert.SerializeObject(result));

            return Json(new { success = true, data = result });
        }








        [HttpPost]
        [Route("Category/Update")]
        public JsonResult Update([FromBody] Category dm)
        {
            if (dm == null || dm.ID == 0)
            {
                return Json(new { status = false, message = "ID danh mục không hợp lệ" });
            }

            var update = db.Categories.FirstOrDefault(a => a.ID == dm.ID);
            bool exists = db.Categories.Any(c => c.Name == dm.Name);
            if (exists)
            {
                return Json(new { status = false, message = "Tên phân mục đã tồn tại. Vui lòng chọn tên khác." });
            }
            if (update == null)
            {
                return Json(new { status = false, message = "Không tìm thấy danh mục trong cơ sở dữ liệu" });
            }

            try
            {
                update.Name = dm.Name;
                update.Gender = dm.Gender;
                update.Category_claim_Id = dm.Category_claim_Id;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { status = true, message = "Sửa thông tin thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = $"Lỗi: {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("Category/Delete")]
        public JsonResult Delete(int id)
        {
            try
            {
                Category dm = db.Categories.Where(a => a.ID.Equals(id)).FirstOrDefault();
                db.Categories.Remove(dm);
                db.SaveChanges();
                return Json(new { status = true });
            }
            catch (Exception)
            {
                return Json(new { status = false});
            }
        }


    }
}
