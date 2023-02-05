using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GimenaCreations.Data;
using GimenaCreations.Models;

namespace GimenaCreations.Pages.Admin.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly GimenaCreations.Data.ApplicationDbContext _context;

        public DetailsModel(GimenaCreations.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(x => x.ApplicationUser).Include(x => x.Items).ThenInclude(x => x.CatalogItem).FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            else
            {
                Order = order;
            }
            return Page();
        }
    }
}
