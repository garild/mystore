using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyStore.Web.Models
{
    public class CreateProductViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        
        [Required]
        [Range(1, 100000)]
        public decimal Price { get; set; }
        
        [Required]
        public string Category { get; set; }

        public List<SelectListItem> Categories { get; }
            = new List<SelectListItem>
            {
                new SelectListItem("Phones", "Phones"),
                new SelectListItem("Computers", "Computers"),
            };
    }
}