using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicroBiz
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Address:")]
        public string Address { get; set; }
        
        [Display(Name = "Address2:")]
        public string Address2 { get; set; }
        [Required]
        [Display(Name = "City:")]
        public string City { get; set; }
        [Required]
        [Display(Name = "State:")]
        public string State { get; set; }
        [Required]
        [Display(Name = "Zip Code:")]
        public string Zip { get; set; }
        
        [Display(Name = "Phone:")]
        public string Phone { get; set; }

        [Display(Name = "Email:")]
        public string Email { get; set; }
    }
}