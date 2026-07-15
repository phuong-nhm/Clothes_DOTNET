using Clothes_DOTNET.Models;

namespace Clothes_DOTNET.Service
{
    public class CartService
    {
        private readonly MyDbContext _context;

        public CartService(MyDbContext context)
        {
            _context = context;
        }
        public List<string> GetNameCategorys()
        {
            var categoryNames = _context.Categories.Select(c => c.Name).ToList();
            return categoryNames;
        }
    }
}
