using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CitizenData.Web.Models
{
    public class UserIndexModel
    {
        [Display(Name = "Email")]
        public string searchString { get; set; }
        public IEnumerable<UserIndexListingModel> Assets { get; set; }
    }

    public class UserIndexListingModel
    {
        public int Id { get; set; }
        [Display(Name = "Given name")]
        public string GivenName { get; set; }
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Display(Name = "Age")]
        public string Age { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Photo")]
        public string ImageUrl { get; set; }
    }
}
