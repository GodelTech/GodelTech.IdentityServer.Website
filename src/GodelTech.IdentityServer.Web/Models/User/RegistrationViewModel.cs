using System.ComponentModel.DataAnnotations;

namespace GodelTech.IdentityServer.Web.Models.User
{
    public class RegistrationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "New User Email Address")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New User Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
        
        [Display(Name = "New User Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string FirstName { get; set; }
        
        [Display(Name = "New User Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string LastName { get; set; }
    }
}
