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

namespace BeSpokedBikes.Pages.Sales
{
    public class CreateModel : PageModel
    {
        private readonly BeSpokedBikes.Data.SalesContext _context;

        public CreateModel(BeSpokedBikes.Data.SalesContext context)
        {
            _context = context;
        }

        // This method is executed when the create page is requested via HTTP GET
        public IActionResult OnGet()
        {
            // Populate the dropdown lists for seller, customer, and product
            ViewData["SellerID"] = new SelectList(_context.Sellers, "SellerID", "FullName");
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName");
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name");

            return Page();
        }

        [BindProperty]
        public Models.Sales Sales { get; set; } = default!;

        // This method is executed when the create form is submitted via HTTP POST
        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Sales != null && Sales != null)
            {
                // Generate the next sales ID
                int nextSalesID = await _context.Sales.MaxAsync(d => d.SalesID) + 1;
                Sales.SalesID = nextSalesID;

                // Retrieve the seller, customer, and product information
                var Seller = await _context.Sellers.FirstOrDefaultAsync(s => s.SellerID == Sales.SellerID);
                var Customer = await _context.Customers.FirstOrDefaultAsync(s => s.CustomerID == Sales.CustomerID);
                var Product = await _context.Products.FirstOrDefaultAsync(s => s.ProductID == Sales.ProductID);

                // Validate the customer, seller, and product for the sale
                if (Customer.StartDate >= DateTime.Now)
                {
                    ModelState.AddModelError(string.Empty, "This customer was not registered at the time of this sale");
                    return Page();
                }
                if (Seller.EndDate <= DateTime.Now || Seller.StartDate >= DateTime.Now)
                {
                    ModelState.AddModelError(string.Empty, "This salesperson is not an active salesperson for this sale date");
                    return Page();
                }
                if (Product.QuantityInStock < 1)
                {
                    ModelState.AddModelError(string.Empty, "This bike is no longer in stock");
                    return Page();
                }

                // Update the product quantity in stock and assign related entities to the sales record
                Product.QuantityInStock--;
                Sales.Product = Product;
                Sales.Customer = Customer;
                Sales.Seller = Seller;

                // Check if a sale with the same ID already exists
                var existingSale = await _context.Sales.FirstOrDefaultAsync(p => p.SalesID == Sales.SalesID);
                if (existingSale != null)
                {
                    ModelState.AddModelError(string.Empty, "A sale with the same ID already exists");
                    return Page();
                }
            }
            else
            {
                return Page();
            }

            // Add the sales record to the context and save the changes
            _context.Sales.Add(Sales);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
