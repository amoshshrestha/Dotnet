using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webapp.Models;

namespace Models
{
    public class ShoppingCart
    {
        [Key]
        public int ID { get; set; }
        public int productID { get; set; }
        [ForeignKey("productID")]
        [ValidateNever]
        public Product Product { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
}
