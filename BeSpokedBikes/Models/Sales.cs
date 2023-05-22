using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSpokedBikes.Models
{
    // Represents a sales transaction
    public class Sales
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int SalesID { get; set; } // Sales ID

        [Required]
        public int ProductID { get; set; } // Product ID of the sold product

        [Required]
        public int SellerID { get; set; } // ID of the seller

        [Required]
        public int CustomerID { get; set; } // ID of the customer

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SalesDate { get; set; } // Date of the sales transaction

        [Required]
        [ForeignKey("ProductID")]
        public Products Product { get; set; } // Reference to the sold product

        [Required]
        [ForeignKey("SellerID")]
        public Sellers Seller { get; set; } // Reference to the seller

        [Required]
        [ForeignKey("CustomerID")]
        public Customers Customer { get; set; } // Reference to the customer
    }
}
