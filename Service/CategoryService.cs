using Clothes_DOTNET.Models;
using Microsoft.EntityFrameworkCore;

namespace Clothes_DOTNET.Service
{
	public class CategoryService
	{
        private readonly MyDbContext _context;

        public CategoryService(MyDbContext context)
        {
            _context = context;
        }
        public List<string> GetNameCategorys()
        {
            var categoryNames = _context.Categories.Select(c => c.Name).Distinct().ToList();
            return categoryNames;
        }
        public List<Category> GetCategorys()
        {
            var categoryNames = _context.Categories.OrderBy(p=> p.Name).ToList();
            return categoryNames;
        }
        public List<string> GetGenderCategorys()
        {
            var gender = _context.Categories.Select(g => g.Gender).Distinct().ToList();
            return gender;
        }
    }
}
