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

namespace BeSpokedBikes.Pages.Sales
{
    public class EditModel : PageModel
    {
        private readonly BeSpokedBikes.Data.SalesContext _context;

        public EditModel(BeSpokedBikes.Data.SalesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Sales Sales { get; set; } = default!;

        // This method is executed when the edit page is requested via HTTP GET
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            // Set the view data for dropdown lists
            ViewData["SellerID"] = new SelectList(_context.Sellers, "SellerID", "FullName");
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName");
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name");

            // Retrieve the sales data from the database based on the specified ID
            var sales = await _context.Sales.FirstOrDefaultAsync(m => m.SalesID == id);
            if (sales == null)
            {
                return NotFound();
            }

            Sales = sales;

            return Page();
        }

        // This method is executed when the edit form is submitted via HTTP POST
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Update the sales data in the database
            _context.Attach(Sales).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesExists(Sales.SalesID))
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

        // This method checks if a sales record with the given ID exists in the database
        private bool SalesExists(int id)
        {
            return (_context.Sales?.Any(e => e.SalesID == id)).GetValueOrDefault();
        }
    }
}
