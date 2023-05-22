using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeSpokedBikes.Data;
using BeSpokedBikes.Models;

namespace BeSpokedBikes.Pages.Products
{
    public class EditDiscountModel : PageModel
    {
        private readonly SalesContext _context;

        public EditDiscountModel(SalesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Discounts Discounts { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Discounts == null)
            {
                return NotFound();
            }

            var discounts = await _context.Discounts.FirstOrDefaultAsync(m => m.DiscountID == id);
            if (discounts == null)
            {
                return NotFound();
            }
            Discounts = discounts;
            // Set the value of ViewBag.ProductID
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {


            _context.Attach(Discounts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscountsExists(Discounts.DiscountID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DiscountsExists(int id)
        {
            return (_context.Discounts?.Any(e => e.DiscountID == id)).GetValueOrDefault();
        }
    }
}
