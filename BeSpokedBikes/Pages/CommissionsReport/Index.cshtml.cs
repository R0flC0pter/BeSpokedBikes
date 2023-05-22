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

namespace BeSpokedBikes.Pages.CommissionsReport
{
    public class IndexModel : PageModel
    {
        private readonly BeSpokedBikes.Data.SalesContext _context;

        public IndexModel(BeSpokedBikes.Data.SalesContext context)
        {
            _context = context;
        }

        public List<CommissionReportItem> CommissionReport { get; set; }
        public int Quarter { get; set; }
        public int Year { get; set; }
        public Dictionary<string, decimal> CommissionData { get; set; } = new Dictionary<string, decimal>();

        public async Task OnGetAsync(int? quarter ,int? year)
        {
            quarter ??= 1;
            year ??= DateTime.Now.Year; 
            Quarter = (int)quarter;
            Year = (int)year;
            (DateTime beginDate, DateTime endDate) = GetQuarterDateRange((int)year, (int)quarter);
            var sales = await _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Product)
                .Include(s => s.Seller)
                .Where(s => s.SalesDate >= beginDate && s.SalesDate <= endDate)
                .ToListAsync();

            CommissionReport = GenerateCommissionReport(sales);
            CommissionData = CommissionReport.ToDictionary(item => item.SalespersonName, item => item.Commission);

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
        private string GetQuarterFromDate(DateTime date)
        {
            int quarter = (date.Month - 1) / 3 + 1;
            string quarterPeriod = $"Q{quarter}";

            return quarterPeriod;
        }
        private (DateTime startDate, DateTime endDate) GetQuarterDateRange(int year, int quarter)
        {
            int month = (quarter - 1) * 3 + 1;
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(3).AddDays(-1);
            return (startDate, endDate);
        }
    }
}
