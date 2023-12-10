using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvcapplication.Models
{
    public class OrderDetail
    {
            public int Id { get; set; }

        [Required]
        public int OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }

        [Required]
        public int Product_Id { get; set; }
        [ForeignKey("Product_Id")]
        [ValidateNever]
        public Product Product { get; set; }

        public int Count { get; set; }
        public double Price { get; set; }
        
    }
}
