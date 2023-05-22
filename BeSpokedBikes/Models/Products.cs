using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace BeSpokedBikes.Models
{
    public class Products
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Product ID")]
        [Key]
        public int ProductID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }
        [Display(Name = "Bike Style")]
        public string Style { get; set; }
        public ICollection<Discounts>? Discounts { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "MSRP")]
        public decimal SalePrice { get; set; }
        [Display(Name = "Quantity in Stock")]
        public int QuantityInStock { get; set; }
        [Display(Name = "Commission Percentage")]
        public decimal CommissionPercentage { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Purchase Price")]
        public decimal PurchasePrice { get; set; }
        public decimal Commission
        {
            get
            {
                return PurchasePrice * (CommissionPercentage / 100);
            }
        }
        public ICollection<Sales>? Sales { get; set; }  
    }
}
