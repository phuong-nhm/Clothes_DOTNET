namespace Clothes_DOTNET.Models
{
    public class SearchProduct
    {
        public int? Id {  get; set; }
        public string? Name { get; set; }
        public decimal? startPrice { get; set; }
        public decimal? endPrice { get; set; }
        public string? Category { get; set; }
        public int? Category_Claim { get; set; }
        public string? Gender { get; set; }
        public string? SortBy { get; set; }
        public string? Sale { get; set; }

        public SearchProduct(int? id,string? name, decimal? startprice, string? category, int? category_Claim, string? gender, string? sortBy, string? sale, decimal? endprice)
        {
            Id = id;
            Name = name;
            startPrice = startprice;
            Category = category;
            Category_Claim = category_Claim;
            Gender = gender;
            SortBy = sortBy;
            Sale = sale;
            endPrice = endprice;
        }
    }
}
