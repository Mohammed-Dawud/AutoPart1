using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace EcommerceAutoPart.Models.ViewModels
{
    public class AutoPartVM
    {
        public AutoPart? AutoPart { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? CarList { get; set; }
    }
}
