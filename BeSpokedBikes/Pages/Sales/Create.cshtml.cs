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

        public IActionResult OnGet()
        {
        ViewData["SellerID"] = new SelectList(_context.Sellers, "SellerID", "FullName");
        ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName");
        ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name");
        
            return Page();
        }

        [BindProperty]
        public Models.Sales Sales { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Sales != null && Sales != null)
            {
                int nextSalesID = await _context.Sales.MaxAsync(d => d.SalesID) + 1;
                Sales.SalesID = nextSalesID;
                var Seller = await _context.Sellers.FirstOrDefaultAsync(s => s.SellerID == Sales.SellerID);
                var Customer = await _context.Customers.FirstOrDefaultAsync(s => s.CustomerID == Sales.CustomerID);
                var Product = await _context.Products.FirstOrDefaultAsync(s => s.ProductID == Sales.ProductID);
                if (Customer.StartDate >= DateTime.Now)
                {
                    ModelState.AddModelError(string.Empty, "This customer was not registerred at the time of this sale");
                    return Page();
                }
                if (Seller.EndDate <= DateTime.Now || Seller.StartDate >= DateTime.Now)
                {
                    ModelState.AddModelError(string.Empty, "This Salesperson in not an active salesperson for this sale date");
                    return Page();
                }
                if (Product.QuantityInStock < 1){
                    ModelState.AddModelError(string.Empty, "This bike is no longer in stock");
                    return Page();
                }
                Product.QuantityInStock--;
                Sales.Product = Product;
                Sales.Customer = Customer;
                Sales.Seller = Seller;
                var existingSale= await _context.Sales.FirstOrDefaultAsync(p => p.SalesID == Sales.SalesID);
                if (existingSale != null)
                {
                    ModelState.AddModelError(string.Empty, "A Sale with the same ID already exists.");
                    return Page();
                }
            }
            else { 
                return Page();
            }
            _context.Sales.Add(Sales);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
