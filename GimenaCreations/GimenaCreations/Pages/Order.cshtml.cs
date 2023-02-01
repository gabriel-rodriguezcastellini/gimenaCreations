using GimenaCreations.Contracts;
using GimenaCreations.Models;
using GimenaCreations.Services;
using MassTransit;
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
    private readonly ICatalogService _catalogService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IBus bus;

    public OrderModel(ICartService cartService, UserManager<ApplicationUser> userManager, IOrderService orderService, ICatalogService catalogService, IBus bus)
    {
        _cartService = cartService;
        _userManager = userManager;
        _orderService = orderService;
        _catalogService = catalogService;
        this.bus = bus;
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

    /// <summary>
    /// Crea la orden, el stock debe ser actualizado sincrónicamente, porque si se hace asíncronicamente da error EF Core
    /// https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/#avoiding-dbcontext-threading-issues
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> OnPostAsync()
    {
        var order = await _cartService.CheckoutAsync(Checkout, _userManager.GetUserId(HttpContext.User));
        await _orderService.CreateOrderAsync(order);
        order.Items.ToList().ForEach(x => _catalogService.UpdateCatalogItemStockAsync(x.CatalogItemId, -x.Units).Wait());
        await bus.Publish(new OrderStatusChanged(order.Id, order.Status, order.ApplicationUserId));
        return RedirectToPage("OrderManagement");
    }
}
