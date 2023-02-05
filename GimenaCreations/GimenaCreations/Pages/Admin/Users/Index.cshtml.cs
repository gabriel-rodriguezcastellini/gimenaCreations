using GimenaCreations.Data;
using GimenaCreations.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GimenaCreations.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ApplicationUser> Users { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Users != null)
            {
                Users = await _context.Users.ToListAsync();
            }
        }
    }
}
