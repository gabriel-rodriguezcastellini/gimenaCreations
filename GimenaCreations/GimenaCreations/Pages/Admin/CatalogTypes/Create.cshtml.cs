using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GimenaCreations.Entities;

namespace GimenaCreations.Pages.Admin.CatalogTypes
{
    public class CreateModel : PageModel
    {
        private readonly GimenaCreations.Data.ApplicationDbContext _context;

        public CreateModel(GimenaCreations.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CatalogType CatalogType { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CatalogTypes.Add(CatalogType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
