using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GimenaCreations.Data;
using GimenaCreations.Models;

namespace GimenaCreations.Pages.Admin.CatalogItems
{
    public class EditModel : PageModel
    {
        private readonly GimenaCreations.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(GimenaCreations.Data.ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public CatalogItem CatalogItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CatalogItems == null)
            {
                return NotFound();
            }

            var catalogitem = await _context.CatalogItems.FirstOrDefaultAsync(m => m.Id == id);
            if (catalogitem == null)
            {
                return NotFound();
            }
            CatalogItem = catalogitem;
            ViewData["CatalogTypeId"] = new SelectList(_context.CatalogTypes, "Id", "Type");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                if (CatalogItem.FormFile != null)
                {
                    if (CatalogItem.FormFile.Length > 0)
                    {
                        using (var stream = System.IO.File.Create($"{_webHostEnvironment.WebRootPath}\\{CatalogItem.FormFile.FileName}"))
                        {
                            await CatalogItem.FormFile.CopyToAsync(stream);
                            CatalogItem.PictureFileName = CatalogItem.FormFile.FileName;
                        }
                    }
                }

                _context.Attach(CatalogItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogItemExists(CatalogItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CatalogItemExists(int id)
        {
            return _context.CatalogItems.Any(e => e.Id == id);
        }
    }
}
