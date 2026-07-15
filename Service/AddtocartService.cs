using Clothes_DOTNET.Models;

namespace Clothes_DOTNET.Service
{
    public class AddtocartService
    {
        private List<Productcart> _cart=new List<Productcart>();

        public AddtocartService(List<Productcart> cart)
        {
            _cart = cart;
        }
        public void addcart(Productcart productcart)
        {
            _cart.Add(productcart);
        }
        public void RemoveProductFromCart(int productId)
        {
            var product = _cart.FirstOrDefault(p => p.price == productId);
            if (product != null)
            {
                _cart.Remove(product);
            }
        }
    }
}
