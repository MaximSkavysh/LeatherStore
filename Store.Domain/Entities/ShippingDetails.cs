using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities
{
    public class ShippingDetails
    {
        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name="Surname")]
        public string Surname { get; set; }

        [Required]
        [StringLength(30,MinimumLength=2,ErrorMessage="The length of field must be from 2 to 30 simbols")]
        [Display(Name="Country")]
        public string Country { get; set; }

        [Required]
        [Display(Name="City")]
        public string City { get; set; }

        [Required]
        [Display(Name="Street")]
        public string Street { get; set; }

        [Display(Name="Flat")]
        public string Flat { get; set; }

        
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",ErrorMessage="Invalid Email!")]
        public string Email { get; set; }
        // to do
    }
}
