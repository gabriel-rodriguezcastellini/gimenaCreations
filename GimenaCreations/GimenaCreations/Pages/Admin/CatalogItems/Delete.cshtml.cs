﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GimenaCreations.Data;
using GimenaCreations.Models;

namespace GimenaCreations.Pages.Admin.CatalogItems
{
    public class DeleteModel : PageModel
    {
        private readonly GimenaCreations.Data.ApplicationDbContext _context;

        public DeleteModel(GimenaCreations.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public CatalogItem CatalogItem { get; set; }

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
            else 
            {
                CatalogItem = catalogitem;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.CatalogItems == null)
            {
                return NotFound();
            }
            var catalogitem = await _context.CatalogItems.FindAsync(id);

            if (catalogitem != null)
            {
                CatalogItem = catalogitem;
                _context.CatalogItems.Remove(CatalogItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
