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

namespace BeSpokedBikes.Pages.Products
{
    public class CreateDiscountModel : PageModel
    {
        private readonly SalesContext _context;

        public CreateDiscountModel(SalesContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "ProductID");
            return Page();
        }

        [BindProperty]
        public Models.Discounts Discount { get; set; } = new Models.Discounts();

        public async Task<IActionResult> OnPostAsync()
        {
            int nextDiscountId = await _context.Discounts.MaxAsync(d => d.DiscountID) + 1;
            Discount.DiscountID = nextDiscountId;
            
            var product = await _context.Products.FirstOrDefaultAsync(s => s.ProductID == Discount.ProductID);
            Discount.Product = product;

            if (product == null)
            {
                ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "ProductID");
                return RedirectToPage("./Index");
            }

            _context.Discounts.Add(Discount);
            await _context.SaveChangesAsync();
            
            return RedirectToPage("./Index");
        }
    }
}
