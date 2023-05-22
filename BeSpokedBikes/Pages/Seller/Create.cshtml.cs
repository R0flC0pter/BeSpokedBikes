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

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Sellers Salespersons { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Sellers == null || Salespersons == null)
            {
                return Page();
            }
            var existingSeller = await _context.Sellers.FirstOrDefaultAsync(p => p.SellerID == Salespersons.SellerID);
            if (existingSeller != null)
            {
                ModelState.AddModelError(string.Empty, "A Salesperson with the same ID already exists.");
                return Page();
            }
            _context.Sellers.Add(Salespersons);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
