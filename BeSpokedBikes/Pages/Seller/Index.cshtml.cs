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
    public class IndexModel : PageModel
    {
        private readonly BeSpokedBikes.Data.SalesContext _context;

        public IndexModel(BeSpokedBikes.Data.SalesContext context)
        {
            _context = context;
        }

        public IList<Models.Sellers> Salespersons { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Sellers != null)
            {
                Salespersons = await _context.Sellers.ToListAsync();
            }
        }
    }
}
