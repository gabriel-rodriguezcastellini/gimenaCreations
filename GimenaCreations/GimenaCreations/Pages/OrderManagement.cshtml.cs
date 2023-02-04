using GimenaCreations.Models;
using GimenaCreations.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
}
