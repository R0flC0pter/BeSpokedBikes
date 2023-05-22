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
    public class DetailsModel : PageModel
    {
        private readonly BeSpokedBikes.Data.SalesContext _context;

        public DetailsModel(BeSpokedBikes.Data.SalesContext context)
        {
            _context = context;
        }

      public Models.Sales Sales { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales
                .Include(s=>s.Seller)
                .Include(s =>s.Customer)
                .Include(s => s.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.SalesID == id);
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
    }
}
