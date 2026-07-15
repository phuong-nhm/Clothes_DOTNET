using CLothes_DOTNET.Controllers;
using Clothes_DOTNET.Service;
using CLothes_DOTNET.Service;
using Microsoft.AspNetCore.Mvc;
using Clothes_DOTNET.Models;
using Newtonsoft.Json;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.AspNetCore.Identity;

namespace Clothes_DOTNET.Controllers
{
    public class CartController : Controller
    {

        private readonly UserManager<AppUser> _usermanager;
        private readonly ILogger<SaleController> _logger;
        private readonly ProductService _productService;
        private readonly SizeService _sizeService;
        private readonly ColorService _colorService;
        private readonly ProductVariantService _productVariantService;
        private readonly CartServices _cartServices;
        private readonly OrderService _orderService;
        private readonly OrderDetailService _orderdetailService;
        public CartController(ILogger<SaleController> logger, ProductService productService, SizeService sizeService, ColorService colorService, ProductVariantService productVariantService, CartServices cartServices, OrderService orderService, OrderDetailService orderdetailService, UserManager<AppUser> usermanager)
        {
            _logger = logger;
            _productService = productService;
            _sizeService = sizeService;
            _colorService = colorService;
            _productVariantService = productVariantService;
            _cartServices = cartServices;
            _orderService = orderService;
            _orderdetailService = orderdetailService;   
            _usermanager = usermanager;

        }
        [HttpGet]
        [HttpPost]
        public IActionResult Page()
        {
            var productDataString = HttpContext.Session.GetString("ProductData");
            List<ProductDTO> products = null;
            products = JsonConvert.DeserializeObject<List<ProductDTO>>(productDataString);
            HttpContext.Session.Remove("ProductData");
            _logger.LogInformation("Day ne 2222222222222222222");
            _logger.LogInformation("Deserialized Products Count: {Count}", products?.Count ?? 0);
            foreach (var product in products)
            {
                _logger.LogInformation("Product: {ProductName}, Price: {ProductPrice}", product.name, product.sellprice);
            }
            return View(products);
        }
        [HttpGet]
        [HttpPost]
        public IActionResult Page_2(string modelData)
        {
            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(modelData);
            _logger.LogInformation("Day ne 2222222222222222222");
            _logger.LogInformation("Model Data: {ModelData}", modelData);
            _logger.LogInformation("Deserialized Products Count: {Count}", products?.Count ?? 0);
            foreach (var product in products)
            {
                _logger.LogInformation("Product: {ProductName}, Price: {ProductPrice}", product.name, product.sellprice);
            }
            return View("Page", products);
        }
        [HttpPost]
        public IActionResult Dodata([FromBody] List<ProductDTO> products)
        {
            _logger.LogInformation("Day ne 11111111111111111111111111111");
            _logger.LogInformation("Received products count: {Count}", products.Count);
            _logger.LogInformation("Received products count: {Count}", products?.Count ?? 0);
            HttpContext.Session.SetString("ProductData", JsonConvert.SerializeObject(products));
            return Json(new
            {
                redirectToUrl = Url.Action("Page", "Cart"),
            });
        }

        [HttpPost]
        public IActionResult AddToCart(int selectedColorId, string selectedColorName, int selectedSizeId, string selectedSizeName, int productid, int productidvariant, decimal price, int Count)
        {
            SearchProduct cri = new SearchProduct(productid, null, null, null, null, null, null,null,null);
            List<int> list=new List<int>();
            IQueryable<Product> query = _productService.SearchProducts(cri, list);
            var product = _productService.GetProductsFirst(query);
            if (product == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = _cartServices.GetCartItems();
            var cartitem = cart.Find(p => p.productvariantid == productidvariant);
            _logger.LogInformation("Day ne");
            foreach (var item in cart)
            {
                var itemJson = System.Text.Json.JsonSerializer.Serialize(item);
                _logger.LogInformation("Cart item: {ItemJson}", itemJson);
            }
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                if (Count != 0)
                {
                    cartitem.count += Count;
                }
                else
                {
                    cartitem.count++;
                }

            }
            else
            {
                //  Thêm mới
                cart.Add(new ProductDTO()
                {
                    ID = product.ID,
                    name = product.name,
                    sellprice = price,
                    image = product.image,
                    category_id = product.category_id,
                    color_id = selectedColorId,
                    color = selectedColorName,
                    size_id = selectedSizeId,
                    size = selectedSizeName,
                    count = Count,
                    productvariantid = productidvariant,

                });
            }

            // Lưu cart vào Session
            _cartServices.SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return View(cart);
        }
        [HttpPost]
        public async Task<IActionResult> Pay([FromBody] SeparateInputModel data)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _usermanager.GetUserAsync(User);
                var userId = user?.Id;
                var modelData = data.products; // Dữ liệu của Model
                var address = data.Address;    // Địa chỉ
                var phone = data.Phone;        // Số điện thoại
                var order =await _orderService.AddOrderAsync(phone, address,address,user.Email,phone,user.Id );

                foreach (var p in modelData)
                {
                    await _orderdetailService.AddOrderDetailAsync(p, order);
                    await _productVariantService.UpdateCountProductVariant(p.productvariantid, p.count);
                }
                var result=await _orderdetailService.TotalMoneyDetailAsync(order.IDOrder);
                await _orderService.UpdateOrderTotalAsync(order.IDOrder, result);
                _logger.LogInformation("address item: {address}", address);
                _logger.LogInformation("phone item: {phone}", phone);
                if (modelData != null && modelData.Count > 0)
                {
                    return Json(new { success = true, message = "Data received successfully!" });
                }

                Console.WriteLine($"User ID: {userId}");
            }
            else
            {
                Console.WriteLine("User is not logged in.");
            }
           
            return Json(new { success = false, message = "Invalid data!" });
        }
    }
}
