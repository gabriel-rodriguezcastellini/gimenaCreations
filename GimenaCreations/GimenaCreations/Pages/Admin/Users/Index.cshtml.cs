using GimenaCreations.Data;
using GimenaCreations.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GimenaCreations.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IList<ApplicationUser> Users { get; set; } = default!;
        public string CurrentUserId { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Users != null)
            {
                Users = await _context.Users.ToListAsync();
            }

            CurrentUserId = _userManager.GetUserId(HttpContext.User);

            foreach (var item in Users)
            {
                foreach (var role in _roleManager.Roles.Select(x=>x.Name))
                {
                    if (await _userManager.IsInRoleAsync(item, role))
                    {
                        item.Roles.Add(role);
                    }
                }
            }
        }
        
        public async Task<IActionResult> OnPostChangeStatusAsync(string id)
        {
            var user = await _context.Users.FirstAsync(x=>x.Id == id);
            user.Active = !user.Active;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
