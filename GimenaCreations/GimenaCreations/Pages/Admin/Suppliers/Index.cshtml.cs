using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GimenaCreations.Data;
using GimenaCreations.Models;

namespace GimenaCreations.Pages.Admin.Suppliers
{
    public class IndexModel : PageModel
    {
        private readonly GimenaCreations.Data.ApplicationDbContext _context;

        public IndexModel(GimenaCreations.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Supplier> Supplier { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Suppliers != null)
            {
                Supplier = await _context.Suppliers.ToListAsync();
            }
        }
    }
}
