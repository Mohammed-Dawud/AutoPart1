using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EcommerceAutoPart.Models.ViewModels
{
    public class CarVM
    {
        public Car? Car { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? BrandList { get; set; }
    }
}
