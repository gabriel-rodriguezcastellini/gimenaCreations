using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GimenaCreations.Models;

namespace GimenaCreations.Pages.CatalogItems
{
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public IndexModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CatalogItem> CatalogItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.CatalogItems != null)
            {
                CatalogItem = await _context.CatalogItems
                .Include(c => c.CatalogBrand)
                .Include(c => c.CatalogType).ToListAsync();
            }
        }
    }
}
