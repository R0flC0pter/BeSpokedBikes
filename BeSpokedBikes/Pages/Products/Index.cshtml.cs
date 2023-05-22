using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeSpokedBikes.Data;
using BeSpokedBikes.Models;

namespace BeSpokedBikes.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly BeSpokedBikes.Data.SalesContext _context;

        public IndexModel(BeSpokedBikes.Data.SalesContext context)
        {
            _context = context;
        }

        public IList<Models.Products> Products { get;set; } = default!;
        public async Task OnGetAsync(int? id)
        {


            if (_context.Products != null)
            {
                Products = await _context.Products
                    .Include(p => p.Discounts)
                    .ToListAsync();

            }
        }
    }
}
