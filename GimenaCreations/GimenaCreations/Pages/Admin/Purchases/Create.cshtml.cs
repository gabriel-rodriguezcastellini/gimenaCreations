using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GimenaCreations.Data;
using GimenaCreations.Models;
using Microsoft.EntityFrameworkCore;

namespace GimenaCreations.Pages.Admin.Purchases
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
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Purchase Purchase { get; set; }        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(ICollection<PurchaseItem> purchaseItems)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (purchaseItems == null || !purchaseItems.Any())
            {
                ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name");                
                ModelState.AddModelError(string.Empty, "The order must be created with at least one item.");
                return Page();
            }

            Purchase.Items = purchaseItems;
            _context.Purchases.Add(Purchase);
            await _context.SaveChangesAsync();

            foreach (var purchaseItem in purchaseItems)
            {
                var catalogItem = await _context.CatalogItems.FirstAsync(x => x.Id == purchaseItem.CatalogItemId);
                catalogItem.AvailableStock += purchaseItem.Quantity;
                _context.Update(catalogItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
