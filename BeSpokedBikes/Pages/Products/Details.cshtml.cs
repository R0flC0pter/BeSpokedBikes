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
    public class DetailsModel : PageModel
    {
        private readonly BeSpokedBikes.Data.SalesContext _context;

        public DetailsModel(BeSpokedBikes.Data.SalesContext context)
        {
            _context = context;
        }

      public Models.Products Products { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(s => s.Discounts)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (products == null)
            {
                return NotFound();
            }
            else 
            {
                Products = products;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAddDiscountAsync(int productId, int discountPercentage, DateTime beginDate, DateTime endDate)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            // Get the next available discount ID
            int nextDiscountId = await _context.Discounts.MaxAsync(d => d.DiscountID) + 1;

            var discount = new Models.Discounts
            {
                DiscountID = nextDiscountId,
                DiscountPercentage = discountPercentage,
                BeginDate = beginDate,
                EndDate = endDate,
                ProductID = productId
            };

            _context.Discounts.Add(discount);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = productId });
        }
    }
}
