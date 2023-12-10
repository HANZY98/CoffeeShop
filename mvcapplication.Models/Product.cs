using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace mvcapplication.Models
{
    public class Product
    {
        [Key]
        public int Product_Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string  Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Display(Name = "Price 1-50")]
        [Range(1,10000)]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Price for 50")]
        [Range(1, 10000)]
        public double Price50 { get; set; }

        [Required]
        [Display(Name = "Price for 100")]
        [Range(1, 10000)]
        public double Price100 { get; set; }

       
        public int Category_Id { get; set; }
        [ForeignKey("Category_Id")]
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string ImageURL { get; set; }

    }
}
