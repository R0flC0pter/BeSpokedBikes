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
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var salespersons =  await _context.Sellers.FirstOrDefaultAsync(m => m.SellerID == id);
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Salespersons).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
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
          return (_context.Sellers?.Any(e => e.SellerID == id)).GetValueOrDefault();
        }
    }
}
