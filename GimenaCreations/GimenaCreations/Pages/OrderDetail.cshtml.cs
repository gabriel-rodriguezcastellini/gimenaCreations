using GimenaCreations.Models;
using GimenaCreations.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GimenaCreations.Pages;

[Authorize]
public class OrderDetailModel : PageModel
{    
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IOrderService _orderService;

    public OrderDetailModel(UserManager<ApplicationUser> userManager, IOrderService orderService)
    {        
        _userManager = userManager;
        _orderService = orderService;
    }

    [BindProperty]
    public Order Order { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int orderId)
    {
        Order = await _orderService.GetOrderByIdAsync(orderId, _userManager.GetUserId(HttpContext.User));
        return Page();
    }
}
