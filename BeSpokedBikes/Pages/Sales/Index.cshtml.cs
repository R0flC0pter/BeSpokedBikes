using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeSpokedBikes.Data;
using BeSpokedBikes.Models;
using BeSpokedBikes.Models.SalesViewModel;

namespace BeSpokedBikes.Pages.Sales
{
    public class IndexModel : PageModel
    {
        private readonly BeSpokedBikes.Data.SalesContext _context;

        public IndexModel(BeSpokedBikes.Data.SalesContext context)
        {
            _context = context;
        }

        public IList<Models.Sales> Sales { get; set; } = default!;
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        // This method is executed when the index page is requested via HTTP GET
        public async Task OnGetAsync(DateTime? beginDate, DateTime? endDate)
        {
            // Set the default values for beginDate and endDate if they are null
            beginDate ??= DateTime.MinValue;
            endDate ??= DateTime.MaxValue;
            BeginDate = (DateTime)beginDate;
            EndDate = (DateTime)endDate;

            if (_context.Sales != null)
            {
                // Retrieve sales data from the database based on the specified date range
                Sales = await _context.Sales
                    .Include(s => s.Seller)
                    .Include(s => s.Customer)
                    .Include(s => s.Product)
                    .Where(s => s.SalesDate >= beginDate && s.SalesDate <= endDate)
                    .ToListAsync();
            }
        }

        // This method generates a commission report based on the given sales data
        private List<CommissionReportItem> GenerateCommissionReport(List<Models.Sales> sales)
        {
            // Perform the necessary calculations to generate the commission report
            // Here's a simplified example:

            var commissionReport = sales
                .GroupBy(s => new
                {
                    s.SellerID,
                })
                .Select(g => new CommissionReportItem
                {
                    SalespersonName = g.FirstOrDefault()?.Seller.FullName,
                    Commission = g.Sum(s => (s.Product.CommissionPercentage / 100) * s.Product.SalePrice)
                })
                .ToList();

            return commissionReport;
        }
    }
}
