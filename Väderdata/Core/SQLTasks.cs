using ChoETL;
using Microsoft.EntityFrameworkCore;
using Väderdata.DataAccess;

namespace Väderdata.Core
{
    public class SQLTasks
    {

        public static void TempAndHumidity()
        {
            using (var context = new WeatherDataContext())
            {

                //var weatherOrderList = from weather in context.Weather
                //              orderby weather.Location
                //              select weather;



                var results = from weather in context.Weather
                              group weather by weather.Location into weatherByLocation
                              select new
                              {
                                  Location = weatherByLocation.Key,
                                  Min = weatherByLocation.OrderBy(w => w.Temperature).FirstOrDefault(),
                                  Max = weatherByLocation.OrderByDescending(w => w.Temperature).FirstOrDefault(),
                                  MinHum = weatherByLocation.OrderBy(w => w.Humidity).FirstOrDefault(),
                                  MaxHum = weatherByLocation.OrderByDescending(w => w.Humidity).FirstOrDefault()

                              };

                foreach (var result in results)
                {
                    Console.WriteLine($"{result.Max.Temperature.ToString().FormatString($"{0:C2}")} - {result.Max.Date}");

                    Console.WriteLine($"{result.MaxHum.Humidity} - {result.Max.Date}");
                    Console.WriteLine();
                }



                //var x = from weather in context.Weather
                //group weather by weather.Location;






                //var LoTempOutside = weatherOrderList.Where(s => s.Location == "Ute").Min(s => s.Temperature);
                //var HiTempOutside = weatherOrderList.Max(s => s.Temperature);


                //var lowTempOutside = context.Weather.Where(s => s.Location == "Ute").OrderBy(s => s.Temperature).FirstOrDefault();
                //var lowTempInside = context.Weather.Where(s => s.Location == "Inne").OrderBy(s => s.Temperature).LastOrDefault();
                //var highTempOutside = context.Weather.OrderBy(s => s.Temperature).ThenByDescending(s => s.Location).LastOrDefault();
                //var highTempInside = context.Weather.Where(s => s.Location == "Inne").Max(s => s.Temperature);
                //Console.WriteLine($"{lowTempOutside.Date.Date} - {lowTempOutside.Temperature} - {lowTempOutside.Location}");
                //Console.WriteLine($"{lowTempInside.Date.Date} - {lowTempInside.Temperature} - {lowTempInside.Location}");
                //Console.WriteLine($"{highTempOutside.Date.Date} - {highTempOutside.Temperature} - {highTempOutside.Location}");
                //Console.WriteLine(LoTempOutside);

                //Console.WriteLine(lowTempOutside);
                //DateTime date = DateTime.Now;
                //var results = from weather in context.Weather
                //              where weather.Temperature < double.MaxValue
                //              select weather;
                //foreach (var result in results)
                //{
                //    if (result.Temperature < lowestTemp && result.Location == "Inne")
                //    {
                //        lowestTemp = result.Temperature;
                //        date = result.Date;
                //    }
                //}
                //Console.WriteLine($"{date} - {lowestTemp}");
                //Console.WriteLine(lowestTemp2);
            }

        }

        public static void TemperatureByDay(DateTime date)
        {

            using (var context = new WeatherDataContext())
            {
                var averageTemp = context.Weather!.Where(d => d.Date == date).Average(t => t.Temperature);
                var averageHumidity = context.Weather!.Where(d => d.Date == date).Average(t => t.Humidity);
                var lowestTemp = context.Weather!.Where(d => d.Date == date).Min(t => t.Temperature);
                var highestTemp = context.Weather!.Where(d => d.Date == date).Max(t => t.Temperature);
                var lowestHumidity = context.Weather!.Where(d => d.Date == date).Min(t => t.Humidity);
                var highestHumidity = context.Weather!.Where(d => d.Date == date).Max(t => t.Humidity);

                Console.WriteLine($"Lowest Temperature: {lowestTemp}\t Average Temperature: {averageTemp}\t Highest Temperature: {highestTemp}");
                Console.WriteLine($"Lowest Humidity: {lowestHumidity}\t Average Humidity: {averageHumidity}\t Highest Humidity: {highestHumidity}");
            }
        }



        public static void SortByTemp()
        {
            using (var context = new WeatherDataContext())
            {
                //var sortedByTemp = context.Weather!.Where(l=>l.Location == "Inne").OrderByDescending(d => d.Temperature).DistinctBy(d=>d.Date.DayOfYear).ToList();

                //var sortedByInsideTemperature = context.Weather!.Where(l => l.Location == "Inne").OrderByDescending(t => t.Temperature).DistinctBy(d=>d.Date).ToList();
                //var sortedByInsideTemperature = context.Weather!.Where(l => l.Location == "Inne").OrderByDescending(t => t.Temperature).GroupBy(g => g.Date.Date.DayOfYear).Select(a => a.FirstOrDefault()).Take(10).ToList();
                //var sortedByOutsideTemperature = context.Weather!.Where(l => l.Location == "Ute").OrderByDescending(t => t.Temperature).ToList();

                //var sortedByInsideTemperature = context.Weather!.Where(l => l.Location == "Inne")/*.OrderBy(l => l.Temperature)*/.GroupBy(d => d.Date.DayOfYear && d.Temperature)
                //    .Select(f => f.OrderByDescending(l => l.Temperature).FirstOrDefault()).Take(10);

                //var sortedByInsideTemperature = context.Weather!.Where(l => l.Location == "Inne").OrderByDescending(l => l.Temperature).Distinct().ToList();

                //var sortedByInsideTemperature = context.Weather!.Where(l => l.Location == "Inne").OrderByDescending(l => l.Temperature).GroupBy(d => new { d.Date.DayOfYear, d.Temperature }).ToList();

                //var FilteredInsideList= new List<WeatherModel>();

                //var SortedByInsideTemperature = context.Weather!.GroupBy(w => w.Date.Date).Select ( g => new { Day = g.Key, })
                //context
                //.Weather
                //.GroupBy(w => w.Date.Date) 
                //.Select(grouping => new { Day = grouping.Key, AverageTemperature = grouping.Average(weatherOnSameDay => weatherOnSameDay.Temperature))

                var sortedByInsideTemperature = context.Weather!.Where(l => l.Location == "Inne").GroupBy(w => w.Date.Date).Select(t=> new { averageTemp = t.Average(t=>t.Temperature), t.Key.Date.Date }).OrderByDescending(t=>t.averageTemp).Take(10);
                var sortedByOutsideTemperature = context.Weather!.Where(l => l.Location == "Ute").GroupBy(w => w.Date.Date).Select(t=> new { averageTemp = t.Average(t=>t.Temperature), t.Key.Date.Date }).OrderByDescending(t=>t.averageTemp).Take(10);

                Console.WriteLine("Average inside temperature sorted by day");
                foreach (var item in sortedByInsideTemperature)
                {
                    Console.WriteLine($"{item.Date.Date}\t {item.averageTemp}");
                    //Console.WriteLine($"{item.Date}\t{item.Location}\t{item.Temperatur}\t{item.Humidity}\t");
                }

                Console.WriteLine();
                Console.WriteLine("Average outside temperature sorted by day");
                foreach (var item in sortedByOutsideTemperature)
                {
                    Console.WriteLine($"{item.Date}\t {item.averageTemp}");

                }





                //if (item.Date.DayOfYear != date.Date.DayOfYear && count != 10)
                //{

                //date = item.Date;
                //count++;
                //}
                //if (count == 30)
                //    break;

                //Console.WriteLine($"{item.Date}\t{item.Location}\t{item.Temperature}\t{item.Humidity}\t");

                //for (int i = 0; i < 10; i++)
                //{
                //    Console.WriteLine($"{sortedByInsideTemperature[i]!.Date}\t{sortedByInsideTemperature[i]!.Location}\t {sortedByInsideTemperature[i]!.Temperature}\t{sortedByInsideTemperature[i]!.Humidity}");
                //}
                //for (int i = 0; i < 10; i++)
                //{
                //    Console.WriteLine($"{sortedByOutsideTemperature[i]!.Date}\t{sortedByOutsideTemperature[i]!.Location}\t {sortedByOutsideTemperature[i]!.Temperature}\t{sortedByOutsideTemperature[i]!.Humidity}");
                //}
            }

        }
    }
}

    

