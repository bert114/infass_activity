using System.ComponentModel.DataAnnotations;

namespace PENANO.Models // <-- Change this to your actual project namespace
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; 

        public string Email { get; set; } = string.Empty;
    };

    



}