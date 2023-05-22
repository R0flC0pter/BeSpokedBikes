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
    public class CreateModel : PageModel
    {
        private readonly BeSpokedBikes.Data.SalesContext _context;

        public CreateModel(BeSpokedBikes.Data.SalesContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ProductID"] = new SelectList(_context.Discounts, "ProductID", "ProductID");
            return Page();
        }

        [BindProperty]
        public Models.Products Products { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Products == null || Products == null)
            {
                return Page();
            }

            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == Products.ProductID);
            if (existingProduct != null)
            {
                ModelState.AddModelError(string.Empty, "A product with the same ID already exists.");
                return Page();
            }

            var salesPrice = Products.SalePrice;
            var productId = Products.ProductID;
            var discount = await _context.Discounts.FirstOrDefaultAsync(d => d.ProductID == productId);

            if (discount != null)
            {
                var discountPercentage = discount.DiscountPercentage;
                var purchasePrice = salesPrice - (salesPrice * discountPercentage);
                Products.PurchasePrice = purchasePrice;
            }

            _context.Products.Add(Products);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
