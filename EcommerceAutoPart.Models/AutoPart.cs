using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAutoPart.Models
{
    public class AutoPart
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Auto Part Name is Required!")]
        [MinLength(3, ErrorMessage = "Minimum Length is 3 char!")]
        [Display(Name = "Auto Part Name", Prompt = "Enter 3 char at least")]
        public string? AutoPartName { get; set; }
		
		public string? AutoPartPhoto { get; set; }
		public string? AutoPartDescription { get; set; }
        [Required]
        [Display(Name = "Price for 1-50")]
        [Range(1, 1000)]
        public double Price { get; set; }
        [Display(Name = "List Price")]
        [Range(1, 10000)]
        public double ListPrice { get; set; }
        [Required]
        [Display(Name = "Price for 50+")]
        [Range(1, 10000)]
        public double Price50 { get; set; }
        [Required]
        [Display(Name = "Price for 100+")]
        [Range(1, 10000)]
        public double Price100 { get; set; }

        
        public int CarId { get; set; }
        [Required(ErrorMessage = "Number of Pieces is Required!")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of Pieces must be at least 1")]
        [Display(Name = "Number of Pieces Available")]
        public int NumberOfPieces { get; set; }

        [Display(Name = "Car ID")]

        //Navigation
        [ForeignKey("CarId")]
        public Car? Car { get; set; }

        


    }
}
