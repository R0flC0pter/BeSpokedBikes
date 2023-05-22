using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BeSpokedBikes.Models
{
    public class Sellers
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Display(Name = "Salesperson ID")]
        [Key]
        public int SellerID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        public long Phone { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
                       ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
                       ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Manager")]
        public string Manager { get; set; }
        [Display(Name = "Salesperson")]
        public string FullName
        {
            get {
                return FirstName + " " + LastName;
            }
        }

        public ICollection<Sales>? Sales { get; set; }
    }
}
