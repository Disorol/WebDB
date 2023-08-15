using Npgsql;
using System;
using WebDB.Models;

namespace WebDB.Data
{
    public static class GetTabularInformation
    {
        public static List<SteamGenerator> GetDailyTable(DateOnly dateOnly)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString: "Host=localhost; Database=WebTable; Username=postgres; Password=postgree77");
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT * FROM public.\"steamgenerator\" where \"Day\" = '{dateOnly}' ORDER BY id";
            NpgsqlDataReader reader = command.ExecuteReader();

            List<SteamGenerator> steamGeneratorList = new List<SteamGenerator>();

            while (reader.Read())
            {
                steamGeneratorList.Add(new SteamGenerator
                {
                    Id = reader.GetInt32(0),
                    Cell1 = reader.GetInt32(1),
                    Cell2 = reader.GetInt32(2),
                    Cell3 = reader.GetInt32(3),
                    Cell4 = reader.GetInt32(4),
                    Cell5 = reader.GetInt32(5),
                    Cell6 = reader.GetInt32(6),
                    Cell7 = reader.GetInt32(7),
                    Cell8 = reader.GetInt32(8),
                    Cell9 = reader.GetInt32(9),
                    Cell10 = reader.GetInt32(10),
                    Cell11 = reader.GetInt32(11),
                    Cell12 = reader.GetInt32(12),
                    Cell13 = reader.GetInt32(13),
                    Cell14 = reader.GetInt32(14),
                    Cell15 = reader.GetInt32(15),
                    Cell16 = reader.GetInt32(16),
                    Cell17 = reader.GetInt32(17),
                    Cell18 = reader.GetInt32(18),
                    Cell19 = reader.GetInt32(19),
                    Cell20 = reader.GetInt32(20),
                    Cell21 = reader.GetInt32(21),
                    Cell22 = reader.GetInt32(22),
                    Cell23 = reader.GetInt32(23),
                    Cell24 = reader.GetInt32(24),
                    Cell25 = reader.GetInt32(25),
                    Cell26 = reader.GetInt32(26),
                    Cell27 = reader.GetInt32(27),
                    Cell28 = reader.GetInt32(28),
                    Cell29 = reader.GetInt32(29),
                    Cell30 = reader.GetInt32(30),
                    Cell31 = reader.GetInt32(31),
                    Cell32 = reader.GetInt32(32),
                    Cell33 = reader.GetInt32(33),
                    Cell34 = reader.GetInt32(34),
                    Cell35 = reader.GetInt32(35),
                    Cell36 = reader.GetInt32(36),
                    Cell37 = reader.GetInt32(37),
                    Cell38 = reader.GetInt32(38),
                    Cell39 = reader.GetInt32(39),
                    Cell40 = reader.GetInt32(40),
                    Cell41 = reader.GetInt32(41),
                    Cell42 = reader.GetInt32(42),
                    Cell43 = reader.GetInt32(43),
                    Cell44 = reader.GetInt32(44),
                    Cell45 = reader.GetInt32(45),
                    Cell46 = reader.GetInt32(46),
                    Cell47 = reader.GetInt32(47),
                    Cell48 = reader.GetInt32(48),
                    Cell49 = reader.GetInt32(49)
                });
            }

            connection.Close();

            return steamGeneratorList;
        }

        public static HashSet<string> GetAllDates()
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString: "Host=localhost; Database=WebTable; Username=postgres; Password=postgree77");
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT * FROM public.\"steamgenerator\" ORDER BY id";
            NpgsqlDataReader reader = command.ExecuteReader();

            HashSet<string> allDates = new HashSet<string>();

            while (reader.Read())
                allDates.Add((DateOnly.FromDateTime(reader.GetDateTime(50))).ToString());

            connection.Close();

            return allDates;
        }

        public static List<int> GetAllIdsByDate(DateOnly dateOnly)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString: "Host=localhost; Database=WebTable; Username=postgres; Password=postgree77");
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = $"SELECT * FROM public.\"steamgenerator\" where \"Day\" = '{dateOnly}' ORDER BY id";
            NpgsqlDataReader reader = command.ExecuteReader();

            List<int> allIdsByDate = new List<int>();

            while (reader.Read())
                allIdsByDate.Add(reader.GetInt32(0));

            connection.Close();

            return allIdsByDate;
        }
    }
}
