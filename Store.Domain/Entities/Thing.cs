using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Store.Domain.Entities
{
     public class Thing
    {
        public int ThingId { get; set; }
        [Display(Name="Name")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name="Category")]
        public string Category { get; set; }
        [Display(Name="Price")]
        [Required]
        [Range(0.01, double.MaxValue,ErrorMessage="Enter price")]
        public decimal Price { get; set; }

        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
    }
}
