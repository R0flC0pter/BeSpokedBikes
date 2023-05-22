using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace BeSpokedBikes.Models
{
    // Represents a customer
    public class Customers
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Customer ID")]
        [Key]
        public int CustomerID { get; set; } // Customer ID

        [Display(Name = "First Name")]
        public string FirstName { get; set; } // First name

        [Display(Name = "Last Name")]
        public string LastName { get; set; } // Last name

        [Display(Name = "Address")]
        public string Address { get; set; } // Address

        [Display(Name = "Phone Number")]
        public long Phone { get; set; } // Phone number

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } // Start date

        public ICollection<Sales>? Sales { get; set; } // Collection of sales associated with the customer

        [Display(Name = "Customer Name")]
        public string FullName // Computed property for the customer's full name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
