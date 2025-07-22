
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAutoPart.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Brand Name")]
        public string? BrandName { get; set; }
        public string BrandPhoto { get; set; } = "/images/noImage.jpg";
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")]
        public int DisplayOrder { get; set; }
    }
}
