using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GimenaCreations.Entities;

namespace GimenaCreations.Pages.Admin.CatalogItems
{
    public class IndexModel : PageModel
    {
        private readonly GimenaCreations.Data.ApplicationDbContext _context;

        public IndexModel(GimenaCreations.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CatalogItem> CatalogItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.CatalogItems != null)
            {
                CatalogItem = await _context.CatalogItems
                .Include(c => c.CatalogType).ToListAsync();
            }
        }
    }
}
