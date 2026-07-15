using Clothes_DOTNET.Models;
using Microsoft.CodeAnalysis;
using System.Drawing;

namespace Clothes_DOTNET.Service
{
    public class ProductVariantService
    {
        private readonly MyDbContext _context;
        private readonly IServiceScopeFactory _scopeFactory;

        public ProductVariantService(MyDbContext context, IServiceScopeFactory scopeFactory)
        {
            _context = context;
            _scopeFactory = scopeFactory;
        }
        public List<int> GetNameSize(int ProductID)
        {
            var sizeNames = _context.ProductVariants.Where(pv=>pv.ProductID==ProductID)
                .Select(pv => pv.Size.SizeID)
                .Distinct()
                .ToList();
            return sizeNames;
        }
        public List<int> GetNameColor(int ProductID)
        {
            var colorNames = _context.ProductVariants.Where(pv => pv.ProductID == ProductID)
                .Select(pv => pv.Color.ColorID)
                .Distinct()
                .ToList();
            return colorNames;
        }
        public List<ProductVariant> GetColorProductVarient(int ProductID)
        {
            var productvariant = _context.ProductVariants.Where(pv => pv.ProductID == ProductID)
                .GroupBy(pv => pv.ColorID)
                .Select(g => g.FirstOrDefault())
                .ToList();
            return productvariant;
        }
        public List<ProductVariant> GetSizeProductVarient(int ProductID)
        {
            var productvariant = _context.ProductVariants.Where(pv => pv.ProductID == ProductID)
                .GroupBy(pv => pv.SizeID)
                .Select(g => g.FirstOrDefault())
                .ToList();
            return productvariant;
        }
        public int Getquatity(int colorid, int sizeid,int productid)
        {
            int totalQuantity = _context.ProductVariants
                            .Where(pv => pv.ProductID == productid && pv.ColorID == colorid && pv.SizeID == sizeid)
                            .Select(pv => pv.Quantity)
                            .FirstOrDefault();
            //if (colorid != null && sizeid != null)
            //{

            //}
            //else
            //{
            //    if (colorid != null )
            //    {

            //        int totalQuantity = _context.ProductVariants
            //                               .Where(pv => pv.ProductID == productid &&
            //                               (pv.ColorID == colorid))
            //                               .Sum(pv => pv.Quantity);
            //    }
            //    if (sizeid != null)
            //    {
            //        int totalQuantity = _context.ProductVariants
            //                               .Where(pv => pv.ProductID == productid &&
            //                               (pv.SizeID == sizeid))
            //                               .Sum(pv => pv.Quantity);
            //    }

            //}
            return totalQuantity;
        }
        public int Getproductidvariant(int colorid, int sizeid, int productid)
        {
            int totalQuantity = _context.ProductVariants
                            .Where(pv => pv.ProductID == productid && pv.ColorID == colorid && pv.SizeID == sizeid)
                            .Select(pv => pv.VariantID)
                            .FirstOrDefault();
            return totalQuantity;
        }
        public async Task UpdateCountProductVariant(int ProductID,int count)
        {
            using (var scope = _scopeFactory.CreateScope()) 
            {
                var context = scope.ServiceProvider.GetRequiredService<MyDbContext>(); 
                var productVariant = await context.ProductVariants.FindAsync(ProductID);
                if (productVariant != null)
                {
                    productVariant.Quantity -= count;
                    await context.SaveChangesAsync();
                }

            }
        }
    }
}
