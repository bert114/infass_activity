using System.ComponentModel.DataAnnotations;

namespace PENANO.Models // <-- Change this to your actual project namespace
{
    public class User
    {
        // If you are using an ID for a database table, you can include it here:
        // public int Id { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        // Keeps track of whether the user checked the box on the form
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}