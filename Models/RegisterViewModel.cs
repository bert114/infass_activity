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







        public static string DisplayQuery(string table_name, string[] values, string[] fields)
        {
            string CleanValues = "";
            string CleanFields = "";

            for (int i = 0; i < fields.Length; i++)
            {
                CleanFields += fields[i];

                if (i < fields.Length - 1)
                {
                    CleanFields += ", ";
                }
            }

            for (int i = 0; i < values.Length; i++)
            {
                CleanValues += values[i];

                if (i < values.Length - 1)
                {
                    CleanValues += ", ";
                }
            }

            return $"INSERT INTO {table_name} ({CleanFields}) \n VALUES ({CleanValues});";
        }
    }
 };
