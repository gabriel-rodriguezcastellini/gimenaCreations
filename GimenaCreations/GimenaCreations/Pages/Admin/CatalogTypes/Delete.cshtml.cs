using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GimenaCreations.Entities;

namespace GimenaCreations.Pages.Admin.CatalogTypes
{
    public class DeleteModel : PageModel
    {
        private readonly GimenaCreations.Data.ApplicationDbContext _context;

        public DeleteModel(GimenaCreations.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public CatalogType CatalogType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CatalogTypes == null)
            {
                return NotFound();
            }

            var catalogtype = await _context.CatalogTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (catalogtype == null)
            {
                return NotFound();
            }
            else 
            {
                CatalogType = catalogtype;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.CatalogTypes == null)
            {
                return NotFound();
            }
            var catalogtype = await _context.CatalogTypes.FindAsync(id);

            if (catalogtype != null)
            {
                CatalogType = catalogtype;
                _context.CatalogTypes.Remove(CatalogType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
