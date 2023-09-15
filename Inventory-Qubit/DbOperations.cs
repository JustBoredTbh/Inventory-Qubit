using Microsoft.UI.Xaml.Media.Animation;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Inventory_Qubit
{
    public class DbOperations
    {
        public static NpgsqlConnection con;
        public static bool ConnectToDb(string ConnectionString) {
            bool Connected;
            try
            {
                con = new NpgsqlConnection(
                            connectionString: ConnectionString);
                con.Open();
                Connected = true;
            }
            catch 
            {
                Connected = false;
            }
            return Connected;
        }

        public static string GetCurrentUserRole()
        {
            // SQL query to get current user role
            string query = "SELECT current_user;";
            using (NpgsqlCommand command = new NpgsqlCommand(query, con))
            {
                // Execute the query and retrieve the result
                string userRole = command.ExecuteScalar()?.ToString();

                if (!string.IsNullOrEmpty(userRole))
                {
                    return userRole;
                }
                else
                {
                    return "noRoleAttached";
                }
            }
        }
    }
}
