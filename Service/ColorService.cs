using Clothes_DOTNET.Models;

namespace Clothes_DOTNET.Service
{
    public class ColorService
    {
        private readonly MyDbContext _context;

        public ColorService(MyDbContext context)
        {
            _context = context;
        }
        public List<Color> GetColor()
        {
            var Colornames = _context.Colors.ToList();
            return Colornames;
        }
    }
}
