using Clothes_DOTNET.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Clothes_DOTNET.Service
{
    public class OrderDetailService
    {
        private readonly MyDbContext _context;

        public OrderDetailService(MyDbContext context)
        {
            _context = context;
        }

        public async Task AddOrderDetailAsync(ProductDTO dto, Order order)
        {
            decimal totalmoney = dto.sellprice * dto.sellprice;
            Order_detail orderDetail = new Order_detail(dto.productvariantid, dto.sellprice, dto.count, totalmoney, order.IDOrder);
            _context.Order_details.Add(orderDetail);
            await _context.SaveChangesAsync();
        }
        public async Task<decimal> TotalMoneyDetailAsync(int orderId)
        {
            var orderDetails = await _context.Order_details
                .Where(od => od.idOder == orderId) 
                .ToListAsync();

            decimal totalAmount = orderDetails.Sum(od => od.TotalMoney); 
            return totalAmount; 
        }
    }
}
