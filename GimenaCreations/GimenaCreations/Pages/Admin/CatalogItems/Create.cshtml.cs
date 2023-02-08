using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GimenaCreations.Data;
using GimenaCreations.Models;
using GimenaCreations.Helpers;
using Microsoft.AspNetCore.Hosting;

namespace GimenaCreations.Pages.Admin.CatalogItems
{
    public class CreateModel : PageModel
    {
        private readonly GimenaCreations.Data.ApplicationDbContext _context;
        private readonly IFileHelper _fileHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(GimenaCreations.Data.ApplicationDbContext context, IFileHelper fileHelper, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _fileHelper = fileHelper;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet()
        {
            ViewData["CatalogTypeId"] = new SelectList(_context.CatalogTypes, "Id", "Type");
            return Page();
        }

        [BindProperty]
        public CatalogItem CatalogItem { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (CatalogItem.FormFile != null && CatalogItem.FormFile.Length > 0)
            {
                await _fileHelper.CreateFileAsync($"{_webHostEnvironment.WebRootPath}\\{CatalogItem.FormFile.FileName}", CatalogItem.FormFile);
                CatalogItem.PictureFileName = CatalogItem.FormFile.FileName;
            }

            _context.CatalogItems.Add(CatalogItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
