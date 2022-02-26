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
    }
}
