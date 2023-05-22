using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeSpokedBikes.Data;
using BeSpokedBikes.Models;

namespace BeSpokedBikes.Pages.Sellers
{
    public class DetailsModel : PageModel
    {
        private readonly BeSpokedBikes.Data.SalesContext _context;

        public DetailsModel(BeSpokedBikes.Data.SalesContext context)
        {
            _context = context;
        }

      public Models.Sellers Salespersons { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var salespersons = await _context.Sellers
                .Include(s => s.Sales)
                .ThenInclude(e =>e.Product)
                .ThenInclude(e => e.Discounts)
                .FirstOrDefaultAsync(m => m.SellerID == id);
            if (salespersons == null)
            {
                return NotFound();
            }
            else 
            {
                Salespersons = salespersons;
            }
            return Page();
        }
    }
}
