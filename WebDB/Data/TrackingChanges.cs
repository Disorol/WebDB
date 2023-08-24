using Npgsql;
using System;
using WebDB.Models;

namespace WebDB.Data
{
    public static class TrackingChanges
    {
        public static void AddChange(string username, string typeOfChange, string tableDate, string datetimeOfChange)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString: "Host=localhost; Database=WebTable; Username=postgres; Password=postgree77");
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = $"INSERT INTO public.\"TrackingChanges\" (username, \"typeOfChange\", \"tableDate\", \"datetimeOfChange\") VALUES ('{username}', '{typeOfChange}', '{tableDate}', '{datetimeOfChange}');";
            command.ExecuteNonQuery();
        }

        public static List<History> ReturnHistory()
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString: "Host=localhost; Database=WebTable; Username=postgres; Password=postgree77");
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT id, username, \"typeOfChange\", \"tableDate\", \"datetimeOfChange\" FROM public.\"TrackingChanges\" ORDER BY id;";
            command.ExecuteNonQuery();

            NpgsqlDataReader reader = command.ExecuteReader();

            List<History> histories = new List<History>();

            while (reader.Read())
            {
                histories.Add(new History
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    TypeOfChange = reader.GetString(2),
                    TableDate = reader.GetString(3),
                    DatetimeOfChange = reader.GetString(4),
                });
            }

            connection.Close();

            return histories;
        }
    }
}
