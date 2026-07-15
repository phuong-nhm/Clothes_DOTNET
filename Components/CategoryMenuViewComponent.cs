using Clothes_DOTNET.Service;
using Microsoft.AspNetCore.Mvc;

namespace Clothes_DOTNET.Components
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly CategoryService _categoryService;
    }
}
