using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#nullable disable

namespace Express.Models
{
    public partial class Branch
    {
        public Branch()
        {
            Vehicles = new HashSet<Vehicle>();
        }
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        [MaxLength(255, ErrorMessage = "{0} cannot be greater than {1} characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [Display(Name = "Country")]
        [MaxLength(255, ErrorMessage = "{0} cannot be greater than {1} characters")]
        public string Country { get; set; }


        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Address")]
        [MaxLength(1024, ErrorMessage = "{0} cannot be greater than {1} characters")]
        public string Address { get; set; }

        [JsonIgnore]
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
