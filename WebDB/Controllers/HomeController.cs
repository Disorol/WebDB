using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using WebDB.Data;
using WebDB.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Check1(User user)
        {
            if (ModelState.IsValid)
            {
                bool isExists = CheckUser.Check(user.Login, user.Password);

                if (isExists)
                {
                    DateOnly dateOnly = new DateOnly(2023, 08, 15);
                    List<SteamGenerator> steamGeneratorList = GetTabularInformation.GetDailyTable(new DateOnly(2023, 08, 15));

                    ViewBag.SteamList = steamGeneratorList;

                    // TempData отказывается хранить DateOnly
                    TempData["SelectedDate"] = dateOnly.ToString();

                    TempData["Login"] = user.Login;

                    TempData.Keep("SelectedDate");
                    TempData.Keep("Login");

                    return View("Table");
                }
                else
                    return Redirect("/");
            }

            return View("Index");
        }

        [HttpPost]
        public IActionResult Check2(UserReg userReg)
        {
            if (ModelState.IsValid)
            {
                bool isExists = CheckUser.Check(userReg.Login, userReg.Password);

                if (!isExists)
                {
                    NpgsqlConnection connection = new NpgsqlConnection(connectionString: "Host=localhost; Database=WebTable; Username=postgres; Password=postgree77");
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO public.\"User\"(\"Login\", \"Password\") VALUES ('{userReg.Login}', '{userReg.Password}')";
                    command.ExecuteNonQuery();
                    connection.Close();

                    return View("Index");
                }
                else
                {
                    return View("SignUp");
                }
            }

            return View("SignUp");
        }

        [HttpPost]
        public IActionResult Check3(List<SteamGenerator> steamGenerators)
        {
            List<int> allIdsByDate = GetTabularInformation.GetAllIdsByDate(DateOnly.Parse(TempData["SelectedDate"].ToString()));

            NpgsqlConnection connection = new NpgsqlConnection(connectionString: "Host=localhost; Database=WebTable; Username=postgres; Password=postgree77");
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;

            for (int i = 0; i < steamGenerators.Count; i++)
            {
                command.CommandText = $"UPDATE public.\"steamgenerator\" SET " +
                    $"cell1={steamGenerators[i].Cell1}, " +
                    $"cell2={steamGenerators[i].Cell2}, " +
                    $"cell3={steamGenerators[i].Cell3}, " +
                    $"cell4={steamGenerators[i].Cell4}, " +
                    $"cell5={steamGenerators[i].Cell5}, " +
                    $"cell6={steamGenerators[i].Cell6}, " +
                    $"cell7={steamGenerators[i].Cell7}, " +
                    $"cell8={steamGenerators[i].Cell8}, " +
                    $"cell9={steamGenerators[i].Cell9}, " +
                    $"cell10={steamGenerators[i].Cell10}, " +
                    $"cell11={steamGenerators[i].Cell11}, " +
                    $"cell12={steamGenerators[i].Cell12}, " +
                    $"cell13={steamGenerators[i].Cell13}, " +
                    $"cell14={steamGenerators[i].Cell14}, " +
                    $"cell15={steamGenerators[i].Cell15}, " +
                    $"cell16={steamGenerators[i].Cell16}, " +
                    $"cell17={steamGenerators[i].Cell17}, " +
                    $"cell18={steamGenerators[i].Cell18}, " +
                    $"cell19={steamGenerators[i].Cell19}, " +
                    $"cell20={steamGenerators[i].Cell20}, " +
                    $"cell21={steamGenerators[i].Cell21}, " +
                    $"cell22={steamGenerators[i].Cell22}, " +
                    $"cell23={steamGenerators[i].Cell23}, " +
                    $"cell24={steamGenerators[i].Cell24}, " +
                    $"cell25={steamGenerators[i].Cell25}, " +
                    $"cell26={steamGenerators[i].Cell26}, " +
                    $"cell27={steamGenerators[i].Cell27}, " +
                    $"cell28={steamGenerators[i].Cell28}, " +
                    $"cell29={steamGenerators[i].Cell29}, " +
                    $"cell30={steamGenerators[i].Cell30}, " +
                    $"cell31={steamGenerators[i].Cell31}, " +
                    $"cell32={steamGenerators[i].Cell32}, " +
                    $"cell33={steamGenerators[i].Cell33}, " +
                    $"cell34={steamGenerators[i].Cell34}, " +
                    $"cell35={steamGenerators[i].Cell35}, " +
                    $"cell36={steamGenerators[i].Cell36}, " +
                    $"cell37={steamGenerators[i].Cell37}, " +
                    $"cell38={steamGenerators[i].Cell38}, " +
                    $"cell39={steamGenerators[i].Cell39}, " +
                    $"cell40={steamGenerators[i].Cell40}, " +
                    $"cell41={steamGenerators[i].Cell41}, " +
                    $"cell42={steamGenerators[i].Cell42}, " +
                    $"cell43={steamGenerators[i].Cell43}, " +
                    $"cell44={steamGenerators[i].Cell44}, " +
                    $"cell45={steamGenerators[i].Cell45}, " +
                    $"cell46={steamGenerators[i].Cell46}, " +
                    $"cell47={steamGenerators[i].Cell47}, " +
                    $"cell48={steamGenerators[i].Cell48}, " +
                    $"cell49={steamGenerators[i].Cell49} WHERE id = {allIdsByDate[i]};";

                command.ExecuteNonQuery();
            }

            connection.Close();

            List<SteamGenerator> steamGeneratorList = GetTabularInformation.GetDailyTable(DateOnly.Parse(TempData["SelectedDate"].ToString()));

            ViewBag.SteamList = steamGeneratorList;

            TempData.Keep("SelectedDate");
            TempData.Keep("Login");

            TrackingChanges.AddChange(TempData["Login"].ToString(), "Редактирование значений ячеек таблицы", TempData["SelectedDate"].ToString(), DateTime.Now.ToString());

            return View("Table");
        }

        [HttpPost]
        public IActionResult Check4()
        {
            HashSet<string> allDates = GetTabularInformation.GetAllDates();

            ViewBag.DTList = allDates;
            TempData.Keep("SelectedDate");
            TempData.Keep("Login");

            return View("DateSelection");
        }

        [HttpPost]
        public IActionResult Check5(SelectedDate selectedDate)
        {
            HashSet<string> allDates = GetTabularInformation.GetAllDates();

            List<SteamGenerator> steamGeneratorList = GetTabularInformation.GetDailyTable(DateOnly.Parse(selectedDate.SDate));

            ViewBag.SteamList = steamGeneratorList;
            ViewBag.DTList = allDates;

            TempData["SelectedDate"] = selectedDate.SDate;

            TempData.Keep("Login");
            TempData.Keep("SelectedDate");

            return View("Table");
        }

        [HttpPost]
        public IActionResult CheckBack()
        {
            HashSet<string> allDates = GetTabularInformation.GetAllDates();

            ViewBag.DTList = allDates;

            string dateBack = DateOnly.Parse(TempData["SelectedDate"].ToString()).AddDays(-1).ToString();

            foreach (string i in allDates)
            {
                if (i == dateBack) // Дата позади выбранной существует.
                {
                    TempData["SelectedDate"] = dateBack;
                    break;
                }
            }

            List<SteamGenerator> steamGeneratorList = 
                GetTabularInformation.GetDailyTable(DateOnly.Parse(TempData["SelectedDate"].ToString()));

            ViewBag.SteamList = steamGeneratorList;
            ViewBag.DTList = allDates;

            TempData.Keep("SelectedDate");
            TempData.Keep("Login");

            return View("Table");
        }

        [HttpPost]
        public IActionResult CheckNext()
        {
            HashSet<string> allDates = GetTabularInformation.GetAllDates();

            ViewBag.DTList = allDates;

            string dateBack = DateOnly.Parse(TempData["SelectedDate"].ToString()).AddDays(1).ToString();

            foreach (string i in allDates)
            {
                if (i == dateBack) // Дата после выбранной существует.
                {
                    TempData["SelectedDate"] = dateBack;
                    break;
                }
            }

            List<SteamGenerator> steamGeneratorList =
                GetTabularInformation.GetDailyTable(DateOnly.Parse(TempData["SelectedDate"].ToString()));

            ViewBag.SteamList = steamGeneratorList;
            ViewBag.DTList = allDates;

            TempData.Keep("SelectedDate");
            TempData.Keep("Login");

            return View("Table");
        }

        [HttpPost]
        public IActionResult CheckCreateDate(SelectedDate selectedDate)
        {
            HashSet<string> allDates = GetTabularInformation.GetAllDates();

            ViewBag.DTList = allDates;

            List<SteamGenerator> steamGeneratorList;

            foreach (string i in allDates)
            {
                if (i == selectedDate.SDate) // Выбранная дата уже сущетвует.
                {
                    steamGeneratorList =
                        GetTabularInformation.GetDailyTable(DateOnly.Parse(selectedDate.SDate));

                    ViewBag.SteamList = steamGeneratorList;

                    TempData.Keep("SelectedDate");
                    TempData.Keep("Login");

                    return View("Table");
                }
            }

            NpgsqlConnection connection = new NpgsqlConnection(connectionString: "Host=localhost; Database=WebTable; Username=postgres; Password=postgree77");
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;

            for (int i = 0; i < 12; i++)
            {
                command.CommandText = $"INSERT INTO public.steamgenerator(cell1, cell2, cell3, " +
                    $"cell4, cell5, cell6, cell7, cell8, cell9, cell10, cell11, cell12, " +
                    $"cell13, cell14, cell15, cell16, cell17, cell18, cell19, cell20, " +
                    $"cell21, cell22, cell23, cell24, cell25, cell26, cell27, cell28, " +
                    $"cell29, cell30, cell31, cell32, cell33, cell34, cell35, cell36, " +
                    $"cell37, cell38, cell39, cell40, cell41, cell42, cell43, cell44, " +
                    $"cell45, cell46, cell47, cell48, cell49, \"Day\") VALUES (-999, " +
                    $"-999, -999, -999, -999, -999, -999, -999, -999, -999, -999, -999," +
                    $" -999, -999, -999, -999, -999, -999, -999, -999, -999, -999, -999," +
                    $" -999, -999, -999, -999, -999, -999, -999, -999, -999, -999, -999," +
                    $" -999, -999, -999, -999, -999, -999, -999, -999, -999, -999, -999," +
                    $" -999, -999, -999, -999, '{selectedDate.SDate}');";

                command.ExecuteNonQuery();
            }

            connection.Close();

            steamGeneratorList =
                        GetTabularInformation.GetDailyTable(DateOnly.Parse(selectedDate.SDate));

            ViewBag.SteamList = steamGeneratorList;

            TempData.Keep("SelectedDate");
            TempData.Keep("Login");

            TrackingChanges.AddChange(TempData["Login"].ToString(), "Создание таблицы", TempData["SelectedDate"].ToString(), DateTime.Now.ToString());

            return View("Table");
        }

        [HttpPost]
        public IActionResult CheckHistory()
        {
            List<History> histories = TrackingChanges.ReturnHistory();
            
            TempData.Keep("SelectedDate");
            TempData.Keep("Login");

            return View("History", histories);
        }

        [HttpPost]
        public IActionResult CheckComeBack()
        {
            List<SteamGenerator> steamGeneratorList =
                GetTabularInformation.GetDailyTable(DateOnly.Parse(TempData["SelectedDate"].ToString()));

            ViewBag.SteamList = steamGeneratorList;

            TempData.Keep("SelectedDate");
            TempData.Keep("Login");

            return View("Table");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult Table()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}