using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PENANO.Models 
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;







        public static string DisplayQuery(string table_name, string[] value)
        {
            string values = "";

            for (int i = 0; i < value.Length; i++)
            {
                 values += value[i];
                

                if (i < value.Length - 1)
                {
                    values += ", ";
                }
            }

            return $"INSERT INTO {table_name} VALUES({values})";
        }
    }
 };
