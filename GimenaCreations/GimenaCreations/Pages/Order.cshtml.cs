using GimenaCreations.Models;
using GimenaCreations.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GimenaCreations.Pages;

[Authorize]
public class OrderModel : PageModel
{
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;
    private readonly UserManager<ApplicationUser> _userManager;

    public OrderModel(ICartService cartService, UserManager<ApplicationUser> userManager, IOrderService orderService)
    {
        _cartService = cartService;
        _userManager = userManager;
        _orderService = orderService;
    }

    [BindProperty]
    public BasketCheckout Checkout { get; set; } = null!;

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);

        Checkout = new BasketCheckout
        {
            Basket = await _cartService.GetBasketAsync(user.Id),
            State = user.Address.State,
            City = user.Address.City,
            Country = user.Address.Country,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Street = user.Address.Street,
            ZipCode = user.Address.ZipCode,
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var order = await _cartService.CheckoutAsync(Checkout, _userManager.GetUserId(HttpContext.User));
        await _orderService.CreateOrderAsync(order);
        return RedirectToPage("OrderSubmitted");
    }
}
