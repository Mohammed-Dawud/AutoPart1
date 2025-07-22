using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAutoPart.Models
{
    public class AutoPartViewModel
    {
        public List<AutoPart>? AutoParts { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
    }
}
