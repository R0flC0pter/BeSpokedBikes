using System.ComponentModel.DataAnnotations;

namespace BeSpokedBikes.Models.SalesViewModel
{
    public class CommissionReportItem
    {
        public string SalespersonName { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Commission { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class CommissionReportModel
    {
        public List<CommissionReportItem> CommissionReport { get; set; }
    }
}
