using Clothes_DOTNET.Models;
using Clothes_DOTNET.Service;
using CLothes_DOTNET.Models;
using CLothes_DOTNET.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

using System.Linq;
namespace CLothes_DOTNET.Controllers
{
    [Route("Sale/[action]")]
    public class SaleController : Controller
    {
        private readonly ILogger<SaleController> _logger;
        private readonly ProductService _productService;
        private readonly SizeService _sizeService;
        private readonly ColorService _colorService;
        private readonly ProductVariantService _productVariantService;
        private readonly CartServices _cartServices;
        private readonly SaleService _saleServices;
        public SaleController(ILogger<SaleController> logger,ProductService productService,SizeService sizeService,ColorService colorService, ProductVariantService productVariantService, CartServices cartServices, SaleService saleServices)
        {
            _logger = logger;
            _productService = productService;
            _sizeService = sizeService;
            _colorService = colorService;
            _productVariantService = productVariantService;
            _cartServices = cartServices;
            _saleServices = saleServices;
        }
        public IActionResult AllProduct()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllProduct(string searchname,string category,int? claim,string gender,string sale,string sortby, int? page,int? pagesize)
        {
            try
            {
                var cateclaim = claim ?? null;
                var listid = _saleServices.Getproductid();
                var objectsale = _saleServices.Getproductidobject();
                _logger.LogInformation("Day ne 11111111111 {2}", sale);
                SearchProduct cri = new SearchProduct(null,searchname, null, category, cateclaim, gender, sortby,sale,null);
                IQueryable<Product> query = _productService.SearchProducts(cri,listid);
                if (!query.Any())
                {
                    return Json(new
                    {
                        Data = new List<Product>(), 
                        TotalItems = 0,
                        CurrentPage = page ?? 1,
                        Numberpage = 0,
                        Pagesize = pagesize ?? 3,
                        Objectsale = objectsale,
                    });
                }
                if (page == null)
                {
                    page = 1;

                }
                var _pagesize = pagesize ?? 3;
                var pageindex = page ?? 1;
                var totalpage=query.Count();
                var numberpage=Math.Ceiling((float)totalpage/ _pagesize);
                var start = (pageindex - 1) * pagesize;
                query = query.Skip((pageindex - 1) * _pagesize).Take(_pagesize);
                var products=_productService.GetProducts(query);
                return Json(new 
                { 
                    Data = products, 
                    TotalItems = totalpage,
                    CurrentPage=pageindex,
                    Numberpage=numberpage,
                    Pagesize=_pagesize,
                    Objectsale = objectsale,
                }
                
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { Error = ex.Message });
            }

        }
        public IActionResult Manpage(int? page)
        
        {
            var products = ProductData.GetProducts();
            var maleProducts = products.Where(p => p.Brand == "Nam").ToList();

            {

                int pageSize = 12;
                int pageNumber = page ?? 1;
                var pagedProducts = maleProducts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                // Truyền dữ liệu vào View
                ViewBag.PageNumber = pageNumber;
                ViewBag.TotalPages = Math.Ceiling((double)maleProducts.Count / pageSize);  


                return View(pagedProducts);
            }
        }

        public IActionResult Womenpage(int? page)
        {
            var products = ProductData.GetProducts();
            var womanProducts = products.Where(p => p.Brand == "Nữ").ToList();

            {

                int pageSize = 12;
                int pageNumber = page ?? 1;
                var pagedProducts = womanProducts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                ViewBag.PageNumber = pageNumber;
                ViewBag.TotalPages = Math.Ceiling((double)womanProducts.Count / pageSize);  


                return View(pagedProducts);
            }
        }
        public IActionResult Get_Product_Detail(int id)
        {
            var listid = _saleServices.Getproductid();
            IQueryable<Product> query = _productService.SearchProducts(null,listid);
            var products = _productService.GetProducts(query);

            var product = products.FirstOrDefault(p => p.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["Sizes"] = _sizeService.GetSize();
            ViewData["Colors"] = _colorService.GetColor();
            ViewData["ProductColorVarients"] = _productVariantService.GetColorProductVarient(id);
            ViewData["ProductSizeVarients"] = _productVariantService.GetSizeProductVarient(id);

            return View(product);
        }
        [HttpGet]
        public JsonResult GetProductVariantQuantity(int sizeId, int colorId,int productId)
        {

            var quantity = _productVariantService.Getquatity(colorId, sizeId, productId);
            var id=_productVariantService.Getproductidvariant(colorId,sizeId,productId);
            return Json(new { quantity ,id});
        }
        public IActionResult Product_Detail(int id)
        {
            var products = ProductData.GetProducts();
            
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult Test(int selectedColorId, string selectedColorName, int selectedSizeId, string selectedSizeName, int productid, int productidvariant, decimal price, int Count)
        {
            _logger.LogInformation("Day ne");
            _logger.LogInformation("1: {SelectedColorId}, 2: {SelectedColorName}, 3: {SelectedSizeId}, 4: {SelectedSizeName},5:{productid},6:{productidvariant},7:{price}, 8:{Count}",
             selectedColorId, selectedColorName, selectedSizeId, selectedSizeName, productid,productidvariant, price, Count);

            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        public IActionResult AddToCart(int selectedColorId, string selectedColorName, int selectedSizeId, string selectedSizeName, int productid,int productidvariant,decimal price,int Count,string action)
        {
            var listid = _saleServices.Getproductid();
            SearchProduct cri = new SearchProduct(productid, null, null, null, null, null, null,null,null);
            IQueryable<Product> query = _productService.SearchProducts(cri,listid);
            var product=_productService.GetProductsFirst(query);
            if (product == null)
                return NotFound("Không có sản phẩm");

            var cart = _cartServices.GetCartItems();
            var cartitem = cart.Find(p => p.productvariantid==productidvariant);
            _logger.LogInformation("Day ne");
            if (action == "addtocart")
            {
                if (cartitem != null)
                {
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

                _cartServices.SaveCartSession(cart);
                return RedirectToAction(nameof(Cart));
            }
            List<ProductDTO> ds = new List<ProductDTO>();
            ds.Add(new ProductDTO()
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
            string serializedModel = JsonConvert.SerializeObject(ds);
            return RedirectToAction("Page_2", "Cart", new { modelData = serializedModel });


        }
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {

            var cart = _cartServices.GetCartItems();
            var cartitem = cart.Find(p => p.productvariantid == productid);
            if (cartitem != null)
            {

                cartitem.count = quantity;
            }
            _cartServices.SaveCartSession(cart);
            return Ok();
        }
        [HttpPost]
        [HttpGet]
        public IActionResult RemoveCart([FromQuery] int productid)
        {
            var cart = _cartServices.GetCartItems();

            var cartitem = cart.Find(p => p.productvariantid == productid);
            if (cartitem != null)
            {
                cart.Remove(cartitem);
            }

            _cartServices.SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }
        [Route("/Cart", Name = "cart")]
        public IActionResult Cart()
        {
            return View(_cartServices.GetCartItems());
        }
       
    }
}
