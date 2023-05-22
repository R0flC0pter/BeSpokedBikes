using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BeSpokedBikes.Models.SalesViewModel
{
    // Represents an item in the commission report
    public class CommissionReportItem
    {
        public string SalespersonName { get; set; } // Name of the salesperson
        [DisplayFormat(DataFormatString = "{0:C}")] // Specifies the display format for the Commission property
        public decimal Commission { get; set; } // Commission amount
        public DateTime BeginDate { get; set; } // Start date of the commission period
        public DateTime EndDate { get; set; } // End date of the commission period
    }

    // Represents the commission report model
    public class CommissionReportModel
    {
        public List<CommissionReportItem> CommissionReport { get; set; } // List of commission report items
    }
}
