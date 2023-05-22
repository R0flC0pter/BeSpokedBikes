using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeSpokedBikes.Data;
using BeSpokedBikes.Models;

namespace BeSpokedBikes.Pages.Sales
{
    public class DeleteModel : PageModel
    {
        private readonly BeSpokedBikes.Data.SalesContext _context;

        public DeleteModel(BeSpokedBikes.Data.SalesContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Models.Sales Sales { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales.FirstOrDefaultAsync(m => m.SalesID == id);

            if (sales == null)
            {
                return NotFound();
            }
            else 
            {
                Sales = sales;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }
            var sales = await _context.Sales.FindAsync(id);

            if (sales != null)
            {
                Sales = sales;
                _context.Sales.Remove(Sales);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
