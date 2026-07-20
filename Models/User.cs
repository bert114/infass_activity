using System.ComponentModel.DataAnnotations;

namespace PENANO.Models // <-- Change this to your actual project namespace
{
    public class User
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}