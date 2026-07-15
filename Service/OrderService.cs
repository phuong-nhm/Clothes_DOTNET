using Clothes_DOTNET.Models;

namespace Clothes_DOTNET.Service
{
    public class OrderService
    {
        private readonly MyDbContext _context;

        public OrderService(MyDbContext context)
        {
            _context = context;
        }
        public async Task<Order> AddOrderAsync(string note,string name,string address,string email,string phone,string userid)
        {
            Order order = new Order(name, email, phone, address, note, DateTime.Now, 1, 0, userid);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
        public async Task UpdateOrderTotalAsync(int orderId, decimal totalMoney)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.TotalMoney = totalMoney;
                await _context.SaveChangesAsync();
            }
        }
    }
}
