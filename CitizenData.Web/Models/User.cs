using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitizenData.Web.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Given name")]
        public string GivenName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Range(0, 130, ErrorMessage = "Age must be between 0 and 130 years")]
        [Display(Name = "Age")]
        public string Age { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public string DOB { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }


        [Required]
        [Display(Name = "Occupation")]
        public string Occupation { get; set; }

        [Display(Name = "Photo")]
        public string ImageUrl { get; set; }

        [NotMapped]
        [Display(Name = "Profile Photo")]
        public IFormFile FormFile { get; set; }
       

    }
}
