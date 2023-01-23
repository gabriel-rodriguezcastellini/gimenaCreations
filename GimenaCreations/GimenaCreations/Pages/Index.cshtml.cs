using GimenaCreations.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace GimenaCreations.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public IndexModel(Data.ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public PaginatedList<CatalogItem> CatalogItem { get; set; } = default!;
        public IList<CatalogType> CatalogType { get; set; } = default!;
        public string CurrentFilter { get; set; } = null!;
        public int? CatalogTypeId { get; set; }

        public async Task OnGetAsync(string searchString, string currentFilter, int? catalogTypeId, int? pageIndex)
        {
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;
            CatalogTypeId = catalogTypeId;

            if (_context.CatalogItems != null)
            {
                IQueryable<CatalogItem> catalogItems = _context.CatalogItems.AsQueryable();

                if (!string.IsNullOrEmpty(searchString))
                {
                    catalogItems = catalogItems.Where(s => s.Name.Contains(searchString));
                }

                if (catalogTypeId.HasValue)
                {
                    catalogItems = catalogItems.Where(s => s.CatalogTypeId == catalogTypeId);
                }

                var pageSize = _configuration.GetValue("PageSize", 4);
                CatalogItem = await PaginatedList<CatalogItem>.CreateAsync(catalogItems.AsNoTracking().Include(c => c.CatalogBrand).Include(c => c.CatalogType), pageIndex ?? 1, pageSize);
            }

            if (_context.CatalogTypes != null)
            {
                CatalogType = await _context.CatalogTypes.ToListAsync();
            }
        }
    }
}