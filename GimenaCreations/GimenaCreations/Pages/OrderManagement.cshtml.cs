using GimenaCreations.Models;
using GimenaCreations.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GimenaCreations.Pages;

[Authorize]
public class OrderManagementModel : PageModel
{
    private readonly IOrderService _orderService;
    private readonly ICatalogService _catalogService;
    private readonly UserManager<ApplicationUser> _userManager;

    public OrderManagementModel(IOrderService orderService, UserManager<ApplicationUser> userManager, ICatalogService catalogService)
    {
        _orderService = orderService;
        _userManager = userManager;
        _catalogService = catalogService;
    }

    public ICollection<Order> Orders { get; set; } = null!;

    public async Task OnGetAsync()
    {
        Orders = await _orderService.GetAllOrdersAsync(_userManager.GetUserId(HttpContext.User));
    }

    /// <summary>
    /// Cancela la orden, el stock debe ser actualizado sincrónicamente, porque si se hace asíncronicamente da error EF Core
    /// https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/#avoiding-dbcontext-threading-issues
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public async Task<IActionResult> OnGetCancelAsync(int orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId, _userManager.GetUserId(HttpContext.User));
        await _orderService.CancelOrderAsync(order);
        order.Items.ToList().ForEach(x => _catalogService.UpdateCatalogItemStockAsync(x.CatalogItemId, x.Units).Wait());
        return RedirectToPage("OrderManagement");
    }
}
