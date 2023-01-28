using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GimenaCreations.Models;

namespace GimenaCreations.Pages.CatalogTypes
{
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public IndexModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CatalogType> CatalogType { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.CatalogTypes != null)
            {
                CatalogType = await _context.CatalogTypes.ToListAsync();
            }
        }
    }
}
