using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Express.Models
{
    public partial class WayBill
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Assign To Vehicle")]
        public int? AssignedToVehicleId { get; set; }

        [Required(ErrorMessage = "Total weight count is required")]
        [Display(Name = "Total Weight")]
        public decimal TotalWeight { get; set; }

        [Required(ErrorMessage = "Parcel count is required")]
        [Display(Name = "Parcel Count")]
        public int ParcelCount { get; set; }

        //[Required(ErrorMessage = "{0} is required")]

        [MaxLength(255, ErrorMessage = "{0} cannot be greater than {1} characters")]
        public string Reference { get; set; }

        [Required(ErrorMessage = "Content Description is required")]
        [MaxLength(4000, ErrorMessage = "{0} cannot be greater than {1} characters")]
        [Display(Name = "Contents Description ")]
        public string ContentDescription { get; set; }

        [Required(ErrorMessage = "Vehicle Starts From is required")]
        [MaxLength(4000, ErrorMessage = "{0} cannot be greater than {1} characters")]
        [Display(Name = "Vehicle Starts From ")]
        public string VehicleStartsFrom { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(4000, ErrorMessage = "{0} cannot be greater than {1} characters")]
        [Display(Name = "Vehicle Destination ")]
        public string Destination { get; set; }

        [Display(Name = "Created By")]
        public int CreatedById { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime Date { get; set; }

        [Display(Name = "Assign To Vehicle")]
        public virtual Vehicle AssignedToVehicle { get; set; }

        [Display(Name = "Created By")]
        public virtual User CreatedBy { get; set; }
    }
}
