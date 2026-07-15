using Clothes_DOTNET.Models;
using CLothes_DOTNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PagedList.Core;

namespace CLothes_DOTNET.Areas.Admin.Controllers
{
    
    public class RoleClaimController : Controller
    {
        private readonly MyDbContext db;
        public RoleClaimController(MyDbContext db)
        {
            this.db = db;
        }

        [Area("Admin")]
        public IActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            ViewBag.searchString = searchString;
            var rolecl = db.RoleClaims.Select(cacl => cacl);
            if (!String.IsNullOrEmpty(searchString))
            {
                rolecl = rolecl.Where(cacl => cacl.RoleId.Contains(searchString));
            }
            // Sắp xếp và phân trang
            var Roleclaim = rolecl.OrderBy(cacl => cacl.Id).ToPagedList(page, pageSize);
            
            return View(Roleclaim);

        }

        [HttpPost]
        [Route("/RoleClaim/Create")]
        public JsonResult Create([FromBody] RoleClaim data)
        {
            var idrole = data.RoleId;
            Console.WriteLine($"Con mẹ mày: {idrole}");
                if (string.IsNullOrEmpty(data.RoleId))  // Kiểm tra nếu tên role trống
                {
                    return Json(new { status = false, message = "Tên role không được để trống." });
                }

                // Kiểm tra nếu id role đã tồn tại
                bool exists = db.RoleClaims.Any(s => s.RoleId == data.RoleId);
                if (exists)
                {
                    return Json(new { status = false, message = "Id role đã tồn tại. Vui lòng chọn tên khác." });
                }

            db.RoleClaims.Add(data);
            db.SaveChanges();

                return Json(new { status = true, message = "Thêm Role thành công!" });
        }

        [HttpPost]
        [Route("/RoleClaim/Load")]
        public JsonResult Load([FromBody] RoleClaim data)
        {
            Console.WriteLine($"Received ID: {data.Id}"); // In ra ID nhận được
            var cacl1 = db.RoleClaims.Where(rl => rl.Id == data.Id).FirstOrDefault();
            if (cacl1 == null)
            {
                return Json(new { status = false, message = "Không tìm thấy dữ liệu." });
            }
            var rlid = cacl1.RoleId;
            Console.WriteLine("ccccc:"+rlid);
            return Json(new {
                idrl = cacl1.Id, 
                rlid = cacl1.RoleId,
                cltp = cacl1.ClaimType,
                clvl = cacl1.ClaimValue
                
            });
        }


        [HttpPost]
        [Route("/RoleClaim/Update")]
        public JsonResult ChangeRole([FromBody] RoleClaim data)
        {
            // Kiểm tra xem ID có hợp lệ không
            var roleclm = db.RoleClaims.FirstOrDefault(r => r.Id == data.Id);
            if (roleclm == null)
            {
                return Json(new { status = false, message = "Không tìm thấy vai trò." });
            }
            var role = db.Roles.FirstOrDefault(rl => rl.Id == data.RoleId);
            if(role == null){
                return Json(new { status = false, message = "Không tìm thấy vai trò." });
            }
            Console.WriteLine($"-----------------------{data.RoleId}");

            // Cập nhật vai trò
            roleclm.RoleId = data.RoleId;
            roleclm.ClaimType = data.ClaimType;
            roleclm.ClaimValue = data.ClaimValue;

            // Lưu vào database
            db.Entry(roleclm).State = EntityState.Modified;
            db.SaveChanges();

            return Json(new { status = true, message = "Cập nhật thành công." });
        }



        [HttpPost]
        [Route("/RoleClaim/Delete")]
        public JsonResult Delete([FromBody] RoleClaim role)
        {
            // Kiểm tra xem role có tồn tại trong cơ sở dữ liệu không
            var roleToDelete = db.RoleClaims.FirstOrDefault(r => r.Id == role.Id);
            if (roleToDelete == null)
            {
                return Json(new { status = false, message = "Role không tồn tại." });
            }

            try
            {
                // Xóa role
                db.RoleClaims.Remove(roleToDelete);
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
