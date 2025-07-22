using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAutoPart.Models.ViewModels
{
    public class AutoPartCartViewModel
    {
        public AutoPart AutoPart { get; set; }
        public ShoppingCart CartItem { get; set; }
    }
}
