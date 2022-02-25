using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System.IO;
using System.Globalization;

namespace Väderdata.Core
{
    public class ExcelData
    {
        [Name("Datum")]
        public string? Date { get; set; }
        [Name("Plats")]
        public string? Place { get; set; }
        [Name("Temp")]
        public string? Temperature { get; set; }
        [Name("Luftfuktighet")]
        public string? Humidity { get; set; }



        //@"F:\Users\Simon\Documents\Drive\Arkitektur av applikationer i .NET C#\Väderdata (Lab 1)\TempFuktData.csv"
        public static IEnumerable<TData> ReadExcelFile<TData>(string path)
        {

            var streamReader = new StreamReader(path);
            var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            return csvReader.GetRecords<TData>();
            //var records = csvReader.GetRecords<ExcelData>();
            //return records;

            //using (var streamReader = new StreamReader(path))
            //{
            //    using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            //    {
            //        return csvReader.GetRecords<TData>();
            //        //var records = csvReader.GetRecords<ExcelData>();
            //        //return records;
            //    }

            //}

        }

        public static IEnumerable<ExcelData> VerifyWeatherDataSet<TData>(IEnumerable<ExcelData> Dataset)
        {

            var validData = new List<ExcelData>();
            var count = 0;

            if (Dataset == null)
            {
                Console.WriteLine("Dataset is empty.");
            }

            foreach (var item in Dataset!)
            {
                count++;
                if (ExcelData.IsValidDateTime(item) && ExcelData.IsValidIntOrDouble(item) && ExcelData.NotADouble(item))
                {
                    validData.Add(item);
                }

            }

            return validData;

            //Console.WriteLine(count);
            //Console.WriteLine(validData.Count);


            //foreach (var item in Dataset!)
            //{
            //    if (!DateTime.TryParse(item.Date, out DateTime date))
            //    {
            //        continue;
            //    }
            //    else if (item.Place != "Inne" || !item.Place != "Ute")
            //    {
            //        continue;

            //    }
            //    else if (!Double.TryParse(item.Temperature, out double temp) || !int.TryParse(item.Temperature, out double inttemp))
            //    {
            //        continue;
            //    }
            //    else if (int.TryParse(item.Humidity, out int humidity))
            //    {
            //        continue;
            //    }
            //}

        }

        public static bool IsValidDateTime(ExcelData excelData)
        {
            if (!DateTime.TryParse(excelData.Date, out DateTime date))
                return false;

            return true;
        }
        public static bool IsValidIntOrDouble(ExcelData excelData)
        {
            if (!Double.TryParse(excelData.Temperature, out double temp) && !int.TryParse(excelData.Temperature, out int inttemp))
                return false;

            return true;
        }


        public static bool NotADouble(ExcelData excelData)
        {
            //if (!(DateTime.Parse(excelData.Date) && (excelData.Place == "Inne"  || excelData.Place == "Ute") && excelData.Temperature) > 2)
            //{

            //}

            //if (excelData.Date.GroupBy(n => n).Any(c => c.Count() > 2))
            //{
            //    if (excelData.Place == "Inne" || excelData.Place == "Ute")
            //    {
            //        if (excelData.Temperature.GroupBy(n => n).Any(c => c.Count() > 2))
            //        {
            //            Console.WriteLine(excelData.Date + " " + excelData.Place + " " + excelData.Temperature + " " + excelData.Humidity);
            //        }

            //    }

            //// FUNGERAR INTE
            //var test = excelData.Date.GroupBy(b => b).Any(c => c.Count() > 8);

            //if (test)
            //{
            //    Console.WriteLine(excelData.Date + " " + excelData.Place + " " + excelData.Temperature + " " + excelData.Humidity);
            //    return false;
            //}
            //return true;

            //    for (int i = 0; i < excelData.Date.Count(); i++)
            //    {
            //        for (int j = i+1; j < excelData.Date.Count(); j++)
            //        {
            //            if (excelData.Date[i] == excelData.Date[j] && excelData.Place == excelData.Place && excelData.Temperature![i] == excelData.Temperature[j]
            //                && excelData.Humidity[i] == excelData.Humidity[j])
            //            {
            //                return false;
            //                break;
            //            }                    
            //        }
            //        return true;
            //    }
            //    return true;
            return true;
        }
    }
}



