using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable

namespace Express.Models
{
    public partial class User
    {
        public User()
        {
            WayBills = new HashSet<WayBill>();
        }
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Username")]
        [MaxLength(255, ErrorMessage = "{0} cannot be greater than {1} characters")]
        [RegularExpression(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Invalid username format")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        public virtual ICollection<WayBill> WayBills { get; set; }

        [JsonIgnore]
        [NotMapped]
        [Display(Name = " Remember Me")]
        public bool RememberMe { get; set; }
        [JsonIgnore]
        [NotMapped]
        public string ReturnUrl { get; set; }
    }
}
