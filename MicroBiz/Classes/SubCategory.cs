using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicroBiz
{
    public class SubCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Category Type:")]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Description:")]
        public string Description { get; set; }
    }
}