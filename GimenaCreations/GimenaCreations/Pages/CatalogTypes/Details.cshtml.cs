using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GimenaCreations.Data;
using GimenaCreations.Models;

namespace GimenaCreations.Pages.CatalogTypes
{
    public class DetailsModel : PageModel
    {
        private readonly GimenaCreations.Data.ApplicationDbContext _context;

        public DetailsModel(GimenaCreations.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
