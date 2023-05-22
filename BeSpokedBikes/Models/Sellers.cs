using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeSpokedBikes.Models
{
    // Represents a seller
    public class Sellers
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int SellerID { get; set; } // Seller ID

        [Display(Name = "First Name")]
        public string FirstName { get; set; } // First name of the seller

        [Display(Name = "Last Name")]
        public string LastName { get; set; } // Last name of the seller

        [Display(Name = "Address")]
        public string Address { get; set; } // Address of the seller

        [Display(Name = "Phone Number")]
        public long Phone { get; set; } // Phone number of the seller

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } // Start date of the seller's employment

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; } // End date of the seller's employment (nullable)

        [Display(Name = "Manager")]
        public string Manager { get; set; } // Manager of the seller

        [Display(Name = "Salesperson")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        } // Full name of the seller

        public ICollection<Sales>? Sales { get; set; } // Collection of sales made by the seller
    }
}
