using Clothes_DOTNET.Models;

namespace Clothes_DOTNET.Service
{
    public class CategoryClaimService
    {
        private readonly MyDbContext _context;

        public CategoryClaimService(MyDbContext context)
        {
            _context = context;
        }
        public List<string> SearchCategoryClaims(SearchCategoryClaim? criteria)
        {
            var query = _context.CategoryClaims.AsQueryable();

            if(criteria!=null)
            {
                if (criteria.CategoryId.HasValue)
                {
                    query = query.Where(c => c.ID == criteria.CategoryId.Value);
                }

                if (!string.IsNullOrEmpty(criteria.Gender))
                {
                    query = query.Where(c => c.categories.Any(cat => cat.Gender == criteria.Gender));
                }
            }

            var result = query.Select(c => c.ClaimName).ToList();
            return result;
        }
        public List<CategoryClaim> getcategoryclaims()
        {
            var result = _context.CategoryClaims.ToList();
            return result;
        }
    }
}
