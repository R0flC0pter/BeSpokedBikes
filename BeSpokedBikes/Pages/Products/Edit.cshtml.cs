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

namespace BeSpokedBikes.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly BeSpokedBikes.Data.SalesContext _context;

        public EditModel(BeSpokedBikes.Data.SalesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Products Products { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products =  await _context.Products.FirstOrDefaultAsync(m => m.ProductID == id);
            if (products == null)
            {
                return NotFound();
            }
            Products = products;
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
            var SalesPrice = Products.SalePrice;
            var ProductID = Products.ProductID;
            var Discount = await _context.Discounts.FirstOrDefaultAsync(d => d.ProductID == ProductID && d.BeginDate <= DateTime.Now && d.EndDate >= DateTime.Now);

            if (Discount != null)
            {
                var DiscountPercentage = Discount.DiscountPercentage;
                var PurchasePrice = SalesPrice - (SalesPrice * (DiscountPercentage / 100));
                Products.PurchasePrice = PurchasePrice;
            }
            else {
                Products.PurchasePrice = SalesPrice;
            }
            _context.Attach(Products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(Products.ProductID))
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

        private bool ProductsExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductID == id)).GetValueOrDefault();
        }
    }
}
