using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GimenaCreations.Entities;

namespace GimenaCreations.Pages.Admin.Purchases
{
    public class DetailsModel : PageModel
    {
        private readonly GimenaCreations.Data.ApplicationDbContext _context;

        public DetailsModel(GimenaCreations.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Purchase Purchase { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases.Include(x=>x.Supplier).FirstOrDefaultAsync(m => m.Id == id);
            if (purchase == null)
            {
                return NotFound();
            }
            else 
            {
                Purchase = purchase;
            }
            return Page();
        }
    }
}
