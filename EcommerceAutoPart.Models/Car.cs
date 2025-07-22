using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAutoPart.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Car Brand ID")]
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Car Name is Required!")]
        [MinLength(3, ErrorMessage = "Minimum Length is 3 char!")]
        [Display(Name = "Car Name", Prompt = "Enter 3 char at least")]
        public string? CarName { get; set; }
        public string? CarPhoto { get; set; }

        //Navigation
        [ForeignKey("BrandId")]
        public Brand? Brand { get; set; }
    }
}
