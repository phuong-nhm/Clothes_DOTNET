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
    public class RolesController : Controller
    {
        private readonly MyDbContext db;
        public RolesController(MyDbContext db)
        {
            this.db = db;
        }


        public IActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            ViewBag.searchString = searchString;
            var role = db.Roles.Select(cacl => cacl);
            if (!String.IsNullOrEmpty(searchString))
            {
                role = role.Where(cacl => cacl.Name.Contains(searchString));
            }
            // Sắp xếp và phân trang
            var roles = role.OrderBy(cacl => cacl.Id).ToPagedList(page, pageSize);
            
            return View(roles);

        }

        [HttpPost]
        [Route("/Roles/Create")]
        public JsonResult Create([FromBody] Roles role)
        {
            Console.WriteLine($"RoleName: {role?.Id}");
            try
            {

                if (string.IsNullOrEmpty(role.Id))
                {
                    return Json(new { status = false, message = "Id role không được để trống." });
                }
                if (string.IsNullOrEmpty(role?.Name))  // Kiểm tra nếu tên role trống
                {
                    return Json(new { status = false, message = "Tên role không được để trống." });
                }

                // Kiểm tra nếu tên role đã tồn tại
                bool exists = db.Roles.Any(s => s.Name == role.Name);
                if (exists)
                {
                    return Json(new { status = false, message = "Id role đã tồn tại. Vui lòng chọn tên khác." });
                }

                db.Roles.Add(role);
                db.SaveChanges();

                return Json(new { status = true, message = "Thêm Role thành công!" });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException?.Message;
                return Json(new { status = false, message = "Đã có lỗi xảy ra: " + ex.Message + " | Chi tiết: " + innerException });

            }
        }

        [HttpPost]
        [Route("/Roles/Load")]
        public JsonResult Load([FromBody] Roles data)
        {
            Console.WriteLine($"Received ID: {data.Id}"); // In ra ID nhận được
            var cacl1 = db.Roles.Where(rl => rl.Id == data.Id).FirstOrDefault();
            if (cacl1 == null)
            {
                return Json(new { status = false, message = "Không tìm thấy dữ liệu." });
            }
            return Json(new {
                idrl = cacl1.Id, 
                name = cacl1.Name,
                nn = cacl1.NormalizedName,
                cs = cacl1.ConcurrencyStamp
            });
        }


        [HttpPost]
        [Route("/Roles/Update")]
        public JsonResult ChangeRole([FromBody] Roles data)
        {
            // Kiểm tra xem ID có hợp lệ không
            var role = db.Roles.FirstOrDefault(r => r.Id == data.Id);
            if (role == null)
            {
                return Json(new { status = false, message = "Không tìm thấy vai trò." });
            }

            // Cập nhật vai trò
            role.Name = data.Name;
            role.NormalizedName = data.NormalizedName;
            role.ConcurrencyStamp = data.ConcurrencyStamp;

            // Lưu vào database
            db.SaveChanges();

            return Json(new { status = true, message = "Cập nhật thành công." });
        }



        [HttpPost]
        [Route("/Roles/Delete")]
        public JsonResult Delete([FromBody] Roles role)
        {
            // Kiểm tra xem role có tồn tại trong cơ sở dữ liệu không
            var roleToDelete = db.Roles.FirstOrDefault(r => r.Id == role.Id);
            if (roleToDelete == null)
            {
                return Json(new { status = false, message = "Role không tồn tại." });
            }

            try
            {
                // Xóa role
                db.Roles.Remove(roleToDelete);
                db.SaveChanges();

                return Json(new { status = true, message = "Xóa role thành công!" });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                var innerException = ex.InnerException?.Message ?? "Không có thông tin chi tiết";
                return Json(new { status = false, message = "Đã có lỗi xảy ra: " + ex.Message + " | Chi tiết: " + innerException });
            }
        }
    }
}
