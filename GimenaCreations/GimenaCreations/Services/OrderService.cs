using GimenaCreations.Data;
using GimenaCreations.Models;
using Microsoft.EntityFrameworkCore;

namespace GimenaCreations.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context) => _context = context;

    public async Task CreateOrderAsync(Order order)
    {
        await _context.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Order>> GetAllOrdersAsync(string userId) => await _context.Orders.Where(x => x.ApplicationUserId == userId).Include(x => x.Items).AsNoTracking().ToListAsync();
}
