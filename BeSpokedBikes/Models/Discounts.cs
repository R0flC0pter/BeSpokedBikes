using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSpokedBikes.Models
{
    // Represents a discount for a product
    public class Discounts
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int DiscountID { get; set; } // Discount ID

        public int ProductID { get; set; } // Product ID

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BeginDate { get; set; } // Start date of the discount

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; } // End date of the discount

        public decimal DiscountPercentage { get; set; } // Discount percentage

        public Products Product { get; set; } // Associated product
    }
}
