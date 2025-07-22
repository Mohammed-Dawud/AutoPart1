using EcommerceAutoPart.Models;

namespace EcommerceAutoPart.Models
{
    public class SearchBrandName
    {
        public Brand? BrandName { get; set;}
        public string? Name { get; set;}
        public IEnumerable<Brand>? Brands{ get; set; }
    }
}
