using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeSpokedBikes.Data;
using BeSpokedBikes.Models;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedBikes.Pages.Sellers
{
    public class CreateModel : PageModel
    {
        private readonly BeSpokedBikes.Data.SalesContext _context;

        public CreateModel(BeSpokedBikes.Data.SalesContext context)
        {
            _context = context;
        }

        // This method is executed when the create page is requested via HTTP GET
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Sellers Salespersons { get; set; } = default!;

        // This method is executed when the create form is submitted via HTTP POST
        // It handles the creation of a new salesperson
        public async Task<IActionResult> OnPostAsync()
        {
            // Check if the model state is valid and if Sellers table or Salespersons property is null
            if (!ModelState.IsValid || _context.Sellers == null || Salespersons == null)
            {
                return Page();
            }

            // Check if a salesperson with the same ID already exists
            var existingSeller = await _context.Sellers.FirstOrDefaultAsync(p => p.SellerID == Salespersons.SellerID);
            if (existingSeller != null)
            {
                ModelState.AddModelError(string.Empty, "A Salesperson with the same ID already exists.");
                return Page();
            }

            // Add the new salesperson to the Sellers table and save the changes to the database
            _context.Sellers.Add(Salespersons);
            await _context.SaveChangesAsync();

            // Redirect to the index page after successful creation
            return RedirectToPage("./Index");
        }
    }
}
