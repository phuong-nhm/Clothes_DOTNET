using Clothes_DOTNET.Models;
using CLothes_DOTNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PagedList.Core;
using System.Drawing;
using System.Runtime.Intrinsics.Arm;

namespace CLothes_DOTNET.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly MyDbContext db;
        public ProductController(MyDbContext db)
        {
            this.db = db;
        }

        public ActionResult Index(string searchString, int page = 1, int pageSize = 9)
        {

            ViewBag.searchString = searchString;
            ViewBag.Category = db.Categories.ToList();
            ViewBag.Size = db.Sizes.ToList();
            ViewBag.Color = db.Colors.Select(c => c);
            var sanphams = db.Products.Include(p=>p.category).Select(s => s);
            if (!String.IsNullOrEmpty(searchString))
            {
                sanphams = sanphams.Where(sp => sp.name.Contains(searchString));
            }
            return View(sanphams.OrderBy(sp => sp.ID).ToPagedList(page, pageSize));
        }

        [Route("Product/AddVariants")]

        [HttpPost]
        public IActionResult AddVariants(int productId, string[] selectedColors, string[] selectedSizes, Dictionary<string, string> quantities)
        {
            foreach (var colorId in selectedColors)
            {
                foreach (var sizeId in selectedSizes)
                {
                    // Lấy số lượng tương ứng cho từng biến thể (màu + kích thước)
                    quantities.TryGetValue(sizeId, out var quantity);

                    // Thêm vào bảng ProductVariant
                    var productVariant = new ProductVariant
                    {
                        ProductID = productId,
                        ColorID = int.Parse(colorId),
                        SizeID = int.Parse(sizeId),
                        Quantity = int.Parse(quantity)
                    };
                    db.ProductVariants.Add(productVariant);
                }
            }

            db.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        [Route("Product/Create")]
        public JsonResult Create(IFormCollection form)
        {
            // Lấy dữ liệu từ form
            var productJson = form["product"];
            var product = JsonConvert.DeserializeObject<Product>(productJson);
            IFormFile image = form.Files["image"];

            // Kiểm tra nếu product.namePd rỗng
            if (string.IsNullOrEmpty(product.name))
            {
                return Json(new { status = false, message = "Tên sản phẩm không được bỏ trống!" });
            }
           
            try
            {
                // Xử lý ảnh
                if (image != null && image.Length > 0)
                {
                    string filename = new Random().Next() + Path.GetFileName(image.FileName);
                    string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserImage", "images", filename);

                    // Tạo thư mục nếu chưa tồn tại
                    var directory = Path.GetDirectoryName(uploadPath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    using (var stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }
                    product.image = "/UserImage/images/" + filename;
                }
                else
                {
                    product.image = "/UserImage/images/noimage.jpg";
                }
                // Thêm sản phẩm
                db.Add(product);
                db.SaveChanges(); // Lưu sản phẩm

                // Lấy Id của sản phẩm vừa thêm            
                
                return Json(new { status = true, message = "Thêm thành công !" });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException?.Message;
                return Json(new { status = false, message = "Đã có lỗi xảy ra: " + ex.Message + " | Chi tiết: " + innerException });
            }
        }

        [HttpPost]
        [Route("Product/LoadProduct")]
        public JsonResult LoadProduct(int id) {
            ViewBag.Category = db.Categories.Select(c => c);
            var product = db.Products.Include("Category").FirstOrDefault(p => p.ID == id);

            if(product == null)
            {
                return Json(new { success = false, message = "Product (Controller) not found." });
            }
            var result = new
            {
                IdPd = product.ID,             
                CategoryId = product.category_id,
                NamePd = product.name,
                ImportPrice = product.importprice,
                SellingPrice = product.sellprice,              
                Image = product.image

            };
            Console.WriteLine("Dữ liệu trả về: " + JsonConvert.SerializeObject(result));

            return Json(new { success = true, data = product });
        }

        [HttpPost]
        [Route("Product/Update")]
        public JsonResult Upddate(IFormCollection form)
        {
            var productJson = form["product"];
            var product = JsonConvert.DeserializeObject<Product>(productJson);           
            IFormFile image = form.Files["image"];
            var update = db.Products.Where(u => u.ID == product.ID).FirstOrDefault();

            try
            {
                // Xử lý ảnh
                if (image != null && image.Length > 0)
                {
                    string filename = new Random().Next() + Path.GetFileName(image.FileName);
                    string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserImage", "images", filename);

                    // Tạo thư mục nếu chưa tồn tại
                    var directory = Path.GetDirectoryName(uploadPath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    using (var stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }
                    //cập nhật ảnh mới
                    update.image = "/UserImage/images/" + filename;

                }
                
                    // Nếu không tải lên hình ảnh mới, giữ nguyên ảnh cũ
                update.image = string.IsNullOrEmpty(update.image) ? "/UserImage/images/noimage.jpg" : update.image;
                
                //Cập nhật 
                //update.Image = product.Image;
                update.category_id = product.category_id;
                update.name = product.name;
                update.importprice = product.importprice;
                update.sellprice =  product.sellprice;
                // Thêm sản phẩm
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges(); // Lưu sản phẩm
                return Json(new { status = true, message = "Sửa thành công !" });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException?.Message;
                return Json(new { status = false, message = "Đã có lỗi xảy ra: " + ex.Message + " | Chi tiết: " + innerException });
            }
        }

        [HttpPost]
        [Route("/Product/Delete")]
        public JsonResult Delete(int id)
        {
            try { 
                var pd = db.Products.FirstOrDefault(p => p.ID == id);
                var pv = db.ProductVariants.FirstOrDefault(pv => pv.VariantID == id);
                if (pd == null && pv == null)
                {                   
                    return Json(new { status = false, message = "Sản phẩm không tồn tại!" });
                }
                var od = db.Order_details.FirstOrDefault(o => o.product_id == id);
                if (od != null)
                {
                    return Json(new { status = false, message = "Xóa sản phẩm này không thành công. Vì sản phẩm này đang liên kết hóa đơn !" });
                }
                var sl = db.Sales.FirstOrDefault(s => s.ProductId == id);
                if (sl != null)
                {
                    return Json(new { status = false, message = "Xóa sản phẩm này không thành công. Vì sản phẩm này đang thuộc sản phẩm sale !" });
                }
                if (pd != null) db.Products.Remove(pd);
                if (pv != null) db.ProductVariants.Remove(pv);              
                db.SaveChanges();
                return Json(new { status = true, message = "Xóa thành công !" });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException?.Message;
                return Json(new { status = false, message = "Đã có lỗi xảy ra: " + ex.Message + " | Chi tiết: " + innerException });
            }
        }

    }
}




