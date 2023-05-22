using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSpokedBikes.Models
{
    public class Discounts
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int DiscountID { get; set; }
        public int ProductID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",
ApplyFormatInEditMode = true)]
        public DateTime BeginDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}",
ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public decimal DiscountPercentage { get; set; }

        public Products Product { get; set; }
    }
}
