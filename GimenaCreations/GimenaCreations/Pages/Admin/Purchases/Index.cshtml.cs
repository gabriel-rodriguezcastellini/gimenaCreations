using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GimenaCreations.Data;
using GimenaCreations.Models;

namespace GimenaCreations.Pages.Admin.Purchases
{
    public class IndexModel : PageModel
    {
        private readonly GimenaCreations.Data.ApplicationDbContext _context;

        public IndexModel(GimenaCreations.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Purchase> Purchase { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Purchases != null)
            {
                Purchase = await _context.Purchases
                .Include(p => p.Supplier).ToListAsync();
            }
        }
    }
}
