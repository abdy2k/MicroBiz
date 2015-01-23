using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicroBiz
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int SubCategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float? Price { get; set; }

        [Required]
        [DataType(DataType.Html)]
        public string Description { get; set; }

        [Required]
        public byte[] ImageUrl { get; set; }

        private DateTime? createdDate;
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate
        {
            get { return createdDate ?? DateTime.UtcNow; }
            set { createdDate = value; }
        }
    }
    public class ProductViewModel 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category:")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Sub Category:")]
        public int SubCategoryId { get; set; }

        [Required]
        [Display(Name = "Product Name:")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Price:")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public float? Price { get; set; }

        [Required]
        [Display(Name = "Description:")]
        [DataType(DataType.Html)]
        public string Description { get; set; }

        [Display(Name = "Image")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}