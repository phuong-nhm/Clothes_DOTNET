using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Clothes_DOTNET.Models
{
    public class ProductDTO
    {
        public int productvariantid { get; set; }
        public int ID { get; set; }


        public string name { get; set; }
        public decimal sellprice { get; set; }

        public string image { get; set; }
        public int category_id { get; set; }
        public int color_id { get; set; }
        public string color {  get; set; }
        public int size_id { get; set; }
        public string size { get; set; }
        public int count {  get; set; }
/*        public string gender { get; set; }*/
/*        public string name_category { get; set; }  
        public string name_category_claim { get; set; }*/

        public ProductDTO(int iD, string name, decimal sellprice, string image, int category_id, int color_id, string color, int size_id, string size, int count)
        {
            ID = iD;
            this.name = name;
            this.sellprice = sellprice;
            this.image = image;
            this.category_id = category_id;
            this.color_id = color_id;
            this.color = color;
            this.size_id = size_id;
            this.size = size;
            this.count = count;
/*            this.gender = gender;*/
/*            this.name_category = name_category;
            
            this.name_category_claim = name_category_claim;*/
        }

        public ProductDTO()
        {
        }

    }
}
