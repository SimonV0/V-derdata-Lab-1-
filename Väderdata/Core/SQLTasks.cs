using ChoETL;
using Väderdata.DataAccess;

namespace Väderdata.Core
{
    public class SQLTasks
    {

        public static void AddToDataBase()
        {
            var test = ExcelData.ReadExcelFile<ExcelData>(@"F:\Users\Simon\Documents\Drive\Arkitektur av applikationer i .NET C#\Väderdata (Lab 1)\TempFuktData.csv");
            var validData = ExcelData.VerifyWeatherDataSet<ExcelData>(test);

            using (var context = new WeatherDataContext())
            {
                foreach (var item in validData)
                {
                    WeatherModel weather = new WeatherModel
                    {
                        Date = DateTime.Parse(item.Date!),
                        Location = item.Place,
                        Temperature = double.Parse(item.Temperature!),
                        Humidity = int.Parse(item.Humidity!)
                    };
                    context.Add(weather);
                }
                context.SaveChanges();
            }

        }


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
                    Console.WriteLine($"{result.Max.Temperature} - {result.Max.Date}");
                    
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
            
            //var endDate = date.AddDays(1);
            using (var context = new WeatherDataContext())
            {
                               
                    var query = from w in context.Weather
                                    //where w.Date.ToString().Contains(date.ToString())
                                    //where EF.Functions.Like(w.Date.ToString(), $"{date}%") /*weather.Date == Convert.ToDateTime(date)*/
                                where w.Date.Date == date.Date
                                select w;
                
                
                if (query.Count() != 0)
                {
                    double lowestTemp = double.MaxValue;
                    int lowestHumidity = int.MaxValue;
                    double highestTemp = double.MinValue;
                    int highestHumidity = int.MinValue;
                    foreach (var item in query)
                    {
                        Console.WriteLine($"{item.Date} \t {item.Location} \t {item.Temperature} \t {item.Humidity}");
                        if ((item.Temperature < lowestTemp || item.Humidity < lowestHumidity) && item.Location == "Inne")
                        {
                            lowestTemp = item.Temperature;
                            lowestHumidity = item.Humidity;
                        }
                        if ((item.Temperature > highestTemp || item.Humidity > highestHumidity) && item.Location == "Ute")
                        {
                            highestTemp = item.Temperature;
                            highestHumidity = item.Humidity;
                        }
                    }
                    query.Count();
                    Console.WriteLine(lowestTemp + "\t" + highestTemp);
                    Console.WriteLine(lowestHumidity + "\t" + highestHumidity );
                
                }
                else
                {
                    Console.WriteLine($"There's no record from the requested date ({date.Date})");
                }
                
            }
            

        }


        //public static void ReadData()
        //{
        //    foreach (var rec in new ChoCSVReader<ExcelData>(@"F:\Users\Simon\Documents\Drive\Arkitektur av applikationer i.NET C#\Väderdata (Lab 1)\Testdata.csv");
        //    .WithFirstLineHeader())
        //    {
        //        using (var p = ChoCSVReader<EmployeeRec>.LoadText(csv)
        //        .WithFirstLineHeader()
        //        .WithField(c => c.Id)
        //        .WithField(c => c.Name)
        //        .ThrowAndStopOnMissingField(false)
        //        .
        //)

        //            Console.WriteLine($"Date: {rec.Date}");
        //        Console.WriteLine($"Place: {rec.Place}");
        //        Console.WriteLine($"Temperature: {rec.Temperature}");
        //        Console.WriteLine($"Humidity: {rec.Humidity}");
        //    }



            //public static void CSVtoDatabase()
            //{
            //    string connectionstring = "Server=(localdb)\\mssqllocaldb;Database=WeatherData;Trusted_Connection=True;";
            //    using (SqlBulkCopy bcp = new SqlBulkCopy(connectionstring))
            //    {
            //        using (var p = new ChoCSVReader(@"F:\Users\Simon\Documents\Drive\Arkitektur av applikationer i .NET C#\Väderdata (Lab 1)\Testdata.csv").WithFirstLineHeader())
            //        {
            //            bcp.DestinationTableName = "Weather";
            //            bcp.EnableStreaming = true;
            //            bcp.BatchSize = 10000;
            //            bcp.BulkCopyTimeout = 0;
            //            bcp.NotifyAfter = 100;
            //            bcp.SqlRowsCopied += delegate (object sender, SqlRowsCopiedEventArgs e),
            //            {
            //                Console.WriteLine(e.RowsCopied.ToString("#,##0") + " rows copied.");
            //            };
            //            bcp.WriteToServer(p.AsDataReader());
            //        }
            //    }
            //}


            //public static void FileToDatabase()
            //{
            //    var bulkCopyUtility = new BulkCopyUtility("Server=(localdb)\\mssqllocaldb;Database=WeatherData;Trusted_Connection=True;");
            //    var dataReader = new CsvDataReaderExtraColumns(@"F:\Users\Simon\Documents\Drive\Arkitektur av applikationer i .NET C#\Väderdata (Lab 1)\Testdata.csv",
            //        new List<TypeCode>(4)
            //    {
            //            //TypeCode.Int32,
            //            TypeCode.DateTime,
            //            TypeCode.String,
            //            TypeCode.Double,
            //            TypeCode.Int32
            //    });

            //    dataReader.AddExtraColumn("Id", -1);
            //    bulkCopyUtility.BulkCopy("Weather", dataReader);
            //}


            //public static void Test()
            //    {
            //        ExcelData.ReadExcelFile(@"F:\Users\Simon\Documents\Drive\Arkitektur av applikationer i .NET C#\Väderdata (Lab 1)\Testdata.csv");

            //        using (var context = new WeatherDataContext())
            //        {
            //            var weather = new WeatherModel
            //            {
            //                Date = DateTime.Now,
            //                Location = "Inne",
            //                Humidity = 37,
            //                Temperature = 25.3
            //            };

            //            context.Weather!.Add(weather);
            //            context.SaveChanges();
            //        }
            //    }

            //}
        }
    }

