using Clothes_DOTNET.Models;

namespace Clothes_DOTNET.Service
{
    public class SizeService
    {
        private readonly MyDbContext _context;

        public SizeService(MyDbContext context)
        {
            _context = context;
        }
        public List<Size> GetSize()
        {
            var SizeNames = _context.Sizes.ToList();
            return SizeNames;
        }
    }
}
