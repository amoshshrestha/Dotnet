using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webapp.Models;

namespace Models
{
    public class ShoppingViewModel
    {
        
        public IEnumerable<ShoppingCart> shoppingCarts { get; set; }
        public double total { get; set; }
    }
}
