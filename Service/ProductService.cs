using Clothes_DOTNET.Models;
using Microsoft.EntityFrameworkCore;

namespace CLothes_DOTNET.Service
{
	public class ProductService
	{
		private readonly MyDbContext _context;

		public ProductService(MyDbContext context)
		{
			_context = context;
		}
        public IQueryable<Product> SearchProducts(SearchProduct? criteria,List<int> list)
        {
            var query = _context.Products.Include(p=> p.category).AsQueryable();
            if (criteria != null)
            {
                if (criteria.Id.HasValue)
                {
                    query = query.Where(p => p.ID == criteria.Id.Value);
                }

                if (!string.IsNullOrEmpty(criteria.Name))
                {
                    query = query.Where(p => p.name.Contains(criteria.Name));
                }

                if (!string.IsNullOrEmpty(criteria.Gender))
                {
                    query = query.Where(p => p.category.Gender.Contains(criteria.Gender));
                }

                if (criteria.startPrice.HasValue)
                {
                    query = query.Where(p => p.sellprice == criteria.startPrice.Value);
                }
                if (!string.IsNullOrEmpty(criteria.Sale))
                {
                    query = query.Where(p => list.Contains(p.ID));
                }
                if (!string.IsNullOrEmpty(criteria.Category))
                {
                    query = query.Where(p => p.category.Name.Contains(criteria.Category));
                }
                if (!string.IsNullOrEmpty(criteria.SortBy))
                {
                    switch (criteria.SortBy.ToLower())
                    {
                        case "name_asc":
                            query = query.OrderBy(p => p.name);
                            break;
                        case "name_desc":
                            query = query.OrderByDescending(p => p.name);
                            break;
                        case "price_asc":
                            query = query.OrderBy(p => p.sellprice);
                            break;
                        case "price_desc":
                            query = query.OrderByDescending(p => p.sellprice);
                            break;
                        default:
                            break;
                    }
                }
                if (criteria.Category_Claim.HasValue)
                {
                    query = query.Where(p => p.category.Category_claim_Id == criteria.Category_Claim.Value);
                }
            }

            return query;
        }
        public List<Product> GetProducts(IQueryable<Product> query)
        {
            return query.ToList();
        }
        public Product GetProductsFirst(IQueryable<Product> query)
        {
            return query.FirstOrDefault();
        }

    }
}
