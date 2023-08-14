using Npgsql;
using WebDB.Models;

namespace WebDB.Data
{
    public static class CheckUser
    {
        public static Boolean Checking(string login, string password)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString: "Host=localhost; Database=WebTable; Username=postgres; Password=postgree77");
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT * FROM public.\"User\" WHERE \"Login\"='{login}' and \"Password\"='{password}'";
            NpgsqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }
    }
}
