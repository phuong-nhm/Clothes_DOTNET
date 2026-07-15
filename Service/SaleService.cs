using Clothes_DOTNET.Models;
using Microsoft.EntityFrameworkCore;

namespace Clothes_DOTNET.Service
{
    public class SaleService
    {
        private readonly MyDbContext _context;

        public SaleService(MyDbContext context)
        {
            _context = context;
        }
        public List<int> Getproductid()
        {
            var id = _context.Sales.Select(s=>s.ProductId).ToList();
            return id;
        }
        public List<Sale> Getproductidobject()
        {
            var sales = _context.Sales.AsNoTracking().ToList();
            return sales;
        }
    }
}
