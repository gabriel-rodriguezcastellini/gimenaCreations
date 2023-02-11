using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GimenaCreations.Entities;

namespace GimenaCreations.Pages.Admin.CatalogItems
{
    public class DetailsModel : PageModel
    {
        private readonly GimenaCreations.Data.ApplicationDbContext _context;

        public DetailsModel(GimenaCreations.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public CatalogItem CatalogItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CatalogItems == null)
            {
                return NotFound();
            }

            var catalogitem = await _context.CatalogItems.Include(x=>x.CatalogType).FirstOrDefaultAsync(m => m.Id == id);
            if (catalogitem == null)
            {
                return NotFound();
            }
            else 
            {
                CatalogItem = catalogitem;
            }
            return Page();
        }
    }
}
