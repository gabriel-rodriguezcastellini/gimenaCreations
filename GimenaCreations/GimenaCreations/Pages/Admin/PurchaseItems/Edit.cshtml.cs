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

namespace GimenaCreations.Pages.Admin.PurchaseItems
{
    public class EditModel : PageModel
    {
        private readonly GimenaCreations.Data.ApplicationDbContext _context;

        public EditModel(GimenaCreations.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PurchaseItem PurchaseItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PurchaseItems == null)
            {
                return NotFound();
            }

            var purchaseitem = await _context.PurchaseItems.FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseitem == null)
            {
                return NotFound();
            }
            PurchaseItem = purchaseitem;
            ViewData["CatalogItemId"] = new SelectList(_context.CatalogItems, "Id", "Name");
            ViewData["PurchaseId"] = new SelectList(_context.Purchases, "Id", "Id");
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
                var oldQuantity = (await _context.PurchaseItems.AsNoTracking().FirstAsync(x => x.Id == PurchaseItem.Id)).Quantity;
                _context.Attach(PurchaseItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                if (oldQuantity != PurchaseItem.Quantity)
                {
                    var catalogItem = await _context.CatalogItems.FirstAsync(x => x.Id == PurchaseItem.CatalogItemId);

                    if (oldQuantity < PurchaseItem.Quantity)
                    {
                        catalogItem.AvailableStock += PurchaseItem.Quantity - oldQuantity;
                    }
                    else
                    {
                        catalogItem.AvailableStock -= oldQuantity - PurchaseItem.Quantity;
                    }

                    _context.Update(catalogItem);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseItemExists(PurchaseItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Admin/Purchases/Edit", new { id = PurchaseItem.PurchaseId });
        }

        private bool PurchaseItemExists(int id)
        {
            return _context.PurchaseItems.Any(e => e.Id == id);
        }
    }
}
