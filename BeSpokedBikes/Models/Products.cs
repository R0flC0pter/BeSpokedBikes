using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeSpokedBikes.Models
{
    // Represents a product
    public class Products
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Product ID")]
        [Key]
        public int ProductID { get; set; } // Product ID

        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Product Name")]
        public string Name { get; set; } // Product name

        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; } // Manufacturer

        [Display(Name = "Bike Style")]
        public string Style { get; set; } // Bike style

        public ICollection<Discounts>? Discounts { get; set; } // Collection of associated discounts

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "MSRP")]
        public decimal SalePrice { get; set; } // Manufacturer's suggested retail price

        [Display(Name = "Quantity in Stock")]
        public int QuantityInStock { get; set; } // Quantity in stock

        [Display(Name = "Commission Percentage")]
        public decimal CommissionPercentage { get; set; } // Commission percentage

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Purchase Price")]
        public decimal PurchasePrice { get; set; } // Purchase price

        public decimal Commission
        {
            get
            {
                return PurchasePrice * (CommissionPercentage / 100);
            }
        } // Calculated commission based on purchase price and commission percentage

        public ICollection<Sales>? Sales { get; set; } // Collection of associated sales
    }
}
