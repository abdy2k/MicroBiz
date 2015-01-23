using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicroBiz
{
    public class Country
    {
        [Key]
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}