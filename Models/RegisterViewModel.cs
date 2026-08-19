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

        public string Email { get; set; } = string.Empty;


        // CREATE (INSERT)
        public static string GenerateInsertQuery(string tableName, string[] fields, string[] values)
        {
            string cleanFields = "";
            string cleanValues = "";

            for (int i = 0; i < fields.Length; i++)
            {
                cleanFields += fields[i];
                if (i < fields.Length - 1)
                {
                    cleanFields += ", ";
                }
            }

            for (int i = 0; i < values.Length; i++)
            {
                cleanValues += $"'{values[i]}'";
                if (i < values.Length - 1)
                {
                    cleanValues += ", ";
                }
            }

            return $"INSERT INTO {tableName} ({cleanFields}) \nVALUES ({cleanValues});";
        }

        // READ (SELECT)
        public static string GenerateSelectQuery(string tableName, string[] columns, string whereClause = "")
        {
            string cleanColumns = "";

            if (columns == null || columns.Length == 0)
            {
                cleanColumns = "*";
            }
            else
            {
                for (int i = 0; i < columns.Length; i++)
                {
                    cleanColumns += columns[i];
                    if (i < columns.Length - 1)
                    {
                        cleanColumns += ", ";
                    }
                }
            }

            string formattedWhere = string.IsNullOrWhiteSpace(whereClause) ? "" : $"\nWHERE {whereClause}";

            return $"SELECT {cleanColumns} \nFROM {tableName}{formattedWhere};";
        }

        // UPDATE
        public static string GenerateUpdateQuery(string tableName, string[] fields, string[] values, string whereClause)
        {
            string columnValuePairs = "";

            for (int i = 0; i < fields.Length; i++)
            {
                columnValuePairs += $"{fields[i]} = '{values[i]}'";
                if (i < fields.Length - 1)
                {
                    columnValuePairs += ", ";
                }
            }

            string formattedWhere = string.IsNullOrWhiteSpace(whereClause) ? "" : $"\nWHERE {whereClause}";

            return $"UPDATE {tableName} \nSET {columnValuePairs}{formattedWhere};";
        }

        // DELETE
        public static string GenerateDeleteQuery(string tableName, string whereClause)
        {
            string formattedWhere = string.IsNullOrWhiteSpace(whereClause) ? "" : $"\nWHERE {whereClause}";

            return $"DELETE FROM {tableName}{formattedWhere};";
        }




    }
 };
