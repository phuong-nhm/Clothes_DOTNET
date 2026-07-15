using Microsoft.DotNet.Scaffolding.Shared.Project;

namespace Clothes_DOTNET.Models
{
    public class SeparateInputModel
    {
        public List<ProductDTO> products { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
