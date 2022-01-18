using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable

namespace Express.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            WayBills = new HashSet<WayBill>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Branch Name is required")]
        [Display(Name = "Branch Name")]
        public int BranchId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Make { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(255, ErrorMessage = "{0} cannot be greater than {1} characters")]
        public string Model { get; set; }


        [Required(ErrorMessage = "{0} is required")]
        public int Year { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(255, ErrorMessage = "{0} cannot be greater than {1} characters")]
        public string Color { get; set; }


        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(255, ErrorMessage = "{0} cannot be greater than {1} characters")]
        public string Registration { get; set; }


        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(1024, ErrorMessage = "{0} cannot be greater than {1} characters")]
        public string Vinumber { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        public virtual Branch Branch { get; set; }

        [JsonIgnore]
        public virtual ICollection<WayBill> WayBills { get; set; }


        [NotMapped]
        public string VehicleDescription
        {
            get
            {
                return $"{Make} {Model}({Year}) - {Registration}"; 
            }
        }
    }
}
