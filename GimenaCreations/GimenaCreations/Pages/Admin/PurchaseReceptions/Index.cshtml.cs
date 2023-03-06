﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GimenaCreations.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GimenaCreations.Constants;

namespace GimenaCreations.Pages.Admin.PurchaseReceptions
{
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        public IndexModel(Data.ApplicationDbContext context, IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        public IList<PurchaseReception> PurchaseReception { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (!(await _authorizationService.AuthorizeAsync(User, Permissions.PurchaseReceptions.View)).Succeeded)
            {
                return new ForbidResult();
            }

            if (_context.PurchaseReceptions != null)
            {
                PurchaseReception = await _context.PurchaseReceptions
                .OrderByDescending(x => x.Id)
                .Include(x => x.PurchaseReceptionItems)
                .Include(p => p.Purchase).ToListAsync();
            }

            return Page();
        }
    }
}
