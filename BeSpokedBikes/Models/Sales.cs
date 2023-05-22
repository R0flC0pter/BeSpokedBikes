using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSpokedBikes.Models
{
    public class Sales
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int SalesID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int SellerID { get; set; }
        [Required]
        public int CustomerID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
       ApplyFormatInEditMode = true)]
        public DateTime SalesDate { get; set; }
        [Required]
        [ForeignKey("ProductID")]
        public Products Product { get; set; }
        [Required]
        [ForeignKey("SellerID")]
        public Sellers Seller { get; set; }
        [Required]
        [ForeignKey("CustomerID")]
        public Customers Customer { get; set; }
    }
}
