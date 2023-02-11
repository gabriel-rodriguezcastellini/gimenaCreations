﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using GimenaCreations.Contracts;
using GimenaCreations.Entities;

namespace GimenaCreations.Pages.Admin.OrderItems
{
    public class EditModel : PageModel
    {
        private readonly GimenaCreations.Data.ApplicationDbContext _context;
        private readonly IBus bus;

        public EditModel(GimenaCreations.Data.ApplicationDbContext context, IBus bus)
        {
            _context = context;
            this.bus = bus;
        }

        [BindProperty]
        public OrderItem OrderItem { get; set; } = default!;

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            if (id == null || _context.OrderItems == null)
            {
                return NotFound();
            }

            var orderitem = await _context.OrderItems.Include(x=>x.Order).FirstOrDefaultAsync(m => m.Id == id);
            if (orderitem == null)
            {
                return NotFound();
            }
            OrderItem = orderitem;
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

            if (OrderItem.FormFile == null)
            {
                ErrorMessage = "Please select a file.";
                return RedirectToPage("./Edit", new { id = OrderItem.Id });
            }

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await OrderItem.FormFile.CopyToAsync(memoryStream);

                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        _context.RemoveRange(await _context.Files.Where(x => x.OrderItemId == OrderItem.Id).ToListAsync());
                        await _context.SaveChangesAsync();

                        await _context.Files.AddAsync(new Entities.File()
                        {
                            Content = memoryStream.ToArray(),
                            OrderItemId = OrderItem.Id,
                            Name = OrderItem.FormFile.FileName
                        });
                    }
                    else
                    {
                        ErrorMessage = "The file is too large.";
                        RedirectToPage("./Edit", new { id = OrderItem.Id });
                    }
                }

                await _context.SaveChangesAsync();
                await bus.Publish(new FileUploaded { OrderId = OrderItem.OrderId, UserId = OrderItem.Order.ApplicationUserId });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(OrderItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Admin/Orders/Edit", new { id = OrderItem.OrderId });
        }

        private bool OrderItemExists(int id)
        {
            return _context.OrderItems.Any(e => e.Id == id);
        }
    }
}
