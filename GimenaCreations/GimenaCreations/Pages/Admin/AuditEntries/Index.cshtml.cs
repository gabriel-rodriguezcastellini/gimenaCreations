using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GimenaCreations.Data;
using GimenaCreations.Entities;

namespace GimenaCreations.Pages.Admin.AuditEntries
{
    public class IndexModel : PageModel
    {
        private readonly GimenaCreations.Data.ApplicationDbContext _context;

        public IndexModel(GimenaCreations.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<AuditEntry> AuditEntry { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.AuditEntries != null)
            {
                AuditEntry = await _context.AuditEntries.OrderByDescending(x => x.Id).ToListAsync();
            }
        }
    }
}
