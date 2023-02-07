using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GimenaCreations.Data;
using GimenaCreations.Models;

namespace GimenaCreations.Pages.Admin.CatalogItems
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
        ViewData["CatalogTypeId"] = new SelectList(_context.CatalogTypes, "Id", "Type");
            return Page();
        }

        [BindProperty]
        public CatalogItem CatalogItem { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CatalogItems.Add(CatalogItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
