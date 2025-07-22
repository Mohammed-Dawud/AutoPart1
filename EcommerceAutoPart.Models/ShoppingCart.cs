using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAutoPart.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int AutoPartId { get; set; }
        [ForeignKey("AutoPartId")]
        [ValidateNever]
        public AutoPart? AutoPart { get; set; }
        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
        public int Count { get; set; }

        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }
        public int CarId { get; set; }
        [ForeignKey("CarId")]
        [ValidateNever]
        public Car? Car { get; set; }
        [NotMapped]
        public double Price { get; set; }

    }
}
