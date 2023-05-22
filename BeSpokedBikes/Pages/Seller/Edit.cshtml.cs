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

namespace BeSpokedBikes.Pages.Sellers
{
    public class EditModel : PageModel
    {
        private readonly BeSpokedBikes.Data.SalesContext _context;

        public EditModel(BeSpokedBikes.Data.SalesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Sellers Salespersons { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // Check if the provided ID is null or Sellers table is null
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            // Retrieve the salesperson with the specified ID from the Sellers table
            var salespersons = await _context.Sellers.FirstOrDefaultAsync(m => m.SellerID == id);
            if (salespersons == null)
            {
                return NotFound();
            }
            Salespersons = salespersons;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Attach the Salespersons object to the context and set its state to Modified
            _context.Attach(Salespersons).State = EntityState.Modified;

            try
            {
                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check if the Salespersons object still exists in the database
                if (!SalespersonsExists(Salespersons.SellerID))
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

        private bool SalespersonsExists(int id)
        {
            // Check if there is a salesperson with the specified ID in the Sellers table
            return (_context.Sellers?.Any(e => e.SellerID == id)).GetValueOrDefault();
        }
    }
}
