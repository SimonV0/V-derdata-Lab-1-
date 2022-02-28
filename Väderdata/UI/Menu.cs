using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Väderdata.DataAccess;
using Väderdata.Core;

namespace Väderdata.UI
{
    public class Menu
    {
        public static void ShowDates()
        {

            using (var context = new WeatherDataContext())
            {
                var dates = from weather in context.Weather
                            where weather.Date < DateTime.Now
                            orderby weather.Date
                            select weather;
                Console.WriteLine("Search for weather by day.");
                Console.WriteLine($"Enter a date between {dates.FirstOrDefault()!.Date.ToShortDateString()} - {dates.LastOrDefault()!.Date.ToShortDateString()}");
                SQLTasks.TemperatureByDay(SelectDate());

            }
        }

        public static DateTime SelectDate()
        {
            DateTime date;
            var input = DateTime.TryParse(Console.ReadLine(), out date);
            return date;
        }


        public static void DisplayAndSelectFromMenu()
        {

            bool menu = true;
            while (menu)
            {
                Console.WriteLine();
                Console.WriteLine("1. Search for temperature information by date.");
                Console.WriteLine("2. Sort by temperature in decending order");
                Console.WriteLine("3. Sort by humidity in decending order");
                Console.WriteLine("4. Sort by mold risk in ascending order");
                Console.WriteLine("5. Date of meteorological autumn and winter");
                Console.WriteLine("6. Exit program");
                Console.WriteLine();

                Console.Write($"Select menu: ");
                string select = Console.ReadLine()!;
                Console.WriteLine();

                switch (select)
                {
                    case "1":
                        ShowDates();
                        break;
                    case "2":
                        SQLTasks.SortByTemp();
                        break;

                    case "3":
                        SQLTasks.SortByHumidity();
                        break;
                    case "4":
                        SQLTasks.MoldIndex();
                        break;
                    case "5":
                        SQLTasks.AutumnOrWinter();
                        break;
                    case "6":
                        menu = false;
                        break;

                    default:
                        Console.WriteLine("Select something between 1-6.");
                        break;
                }
            }
        }
    }
}
