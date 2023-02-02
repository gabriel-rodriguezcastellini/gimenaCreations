using GimenaCreations.Data;
using GimenaCreations.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GimenaCreations.Pages;

[Authorize]
public class OrderDetailModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public OrderDetailModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [BindProperty]
    public Order Order { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int orderId)
    {
        Order = await _context.Orders.Include(x => x.Address).Include(x => x.Items).FirstAsync(x => x.Id == orderId && x.ApplicationUserId == _userManager.GetUserId(HttpContext.User));
        return Page();
    }
}
