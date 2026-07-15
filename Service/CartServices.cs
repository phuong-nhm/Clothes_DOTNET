using Clothes_DOTNET.Models;
using Newtonsoft.Json;

namespace Clothes_DOTNET.Service
{
    public class CartServices
    {
        public const string CARTKEY = "cart";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpContext _httpContext;
        public CartServices (IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpContext = httpContextAccessor.HttpContext;
        }
        public List<ProductDTO> GetCartItems()
        {

            var session = _httpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<ProductDTO>>(jsoncart);
            }
            return new List<ProductDTO>();
        }
        public void ClearCart()
        {
            var session = _httpContext.Session;
            session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        public void SaveCartSession(List<ProductDTO> ls)
        {
            var session = _httpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
    }
}
