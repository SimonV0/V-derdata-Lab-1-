using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System.IO;
using System.Globalization;
using Väderdata.DataAccess;

namespace Väderdata.Core
{
    public class ExcelData
    {
        public List<WeatherModel> weatherList { get; set; }
        public ExcelData()
        {
            weatherList = new List<WeatherModel>();
        }


        //@"F:\Users\Simon\Documents\Drive\Arkitektur av applikationer i .NET C#\Väderdata (Lab 1)\TempFuktData.csv"
        public void ReadFileAndUploadToDB(string path)
        {


            var streamReader = new StreamReader(path);
            string firstLine = streamReader.ReadLine()!;
            string secondLine = "";
            while ((secondLine = streamReader.ReadLine()!) != null)
            {
                var line = secondLine.Split(',');
                double temp = IsValidIntOrDouble(line);
                DateTime dateTime = IsValidDateTime(line);
                var humidity = int.Parse(line[3]);
                var myClass = new WeatherModel
                {
                    Date = dateTime,
                    Location = line[1],
                    Temperature = temp,
                    Humidity = humidity
                };
                weatherList.Add(myClass);
            }
            Duplicates();
            AddToDatabase();

        }

        private void AddToDatabase()
        {
            using (var context = new WeatherDataContext())
            {
                context.AddRange(weatherList);
                context.SaveChanges();
            }
        }

        static DateTime IsValidDateTime(string[] line)
        {
            bool dateTime = DateTime.TryParse(line[0], out DateTime result);
            return result;
        }
        double IsValidIntOrDouble(string[] line)
        {

            double.TryParse(line[2], out double result);
            return result;

        }

        void Duplicates()
        {
            for (int i = 0; i < weatherList.Count; i++)
            {
                for (int j = i+1; j < weatherList.Count; j++)
                {
                    if (weatherList[i].Date == weatherList[j].Date && weatherList[i].Location == weatherList[j].Location && 
                        weatherList[i].Temperature == weatherList[j].Temperature && weatherList[i].Humidity == weatherList[j].Humidity) 
                    {
                        weatherList.RemoveAt(j);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

    }
}



