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
        public async Task OnGetAsync(DateTime? beginDate, DateTime? endDate)
        {
            beginDate ??= DateTime.MinValue; 
            endDate ??= DateTime.MaxValue;
            BeginDate = (DateTime)beginDate;
            EndDate = (DateTime)endDate;

            if (_context.Sales != null)
            {
                Sales = await _context.Sales
                    .Include(s => s.Seller)
                .Include(s => s.Customer)
                .Include(s => s.Product)
                .Where(s => s.SalesDate >= beginDate && s.SalesDate <= endDate)
                .ToListAsync();
            }
        }
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

            //  .GroupBy(s => s.SellerID)
            //  .Select(g => new CommissionReportItem
            //  {
            //      SalespersonName = g.FirstOrDefault().Seller.FullName,
            //      Commission = g.Sum(s => (s.Product.CommissionPercentage/100 )* s.Product.SalePrice)
            //  })
            //  .ToList();

            return commissionReport;
        }
    }
}
