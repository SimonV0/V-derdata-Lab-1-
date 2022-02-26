

using CsvHelper;
using System.Globalization;
using Väderdata.Core;
using Väderdata.DataAccess;
using Väderdata.UI;

//ExcelData data = new ExcelData();
//data.ReadFileAndUploadToDB(@"F:\Users\Simon\Documents\Drive\Arkitektur av applikationer i .NET C#\Väderdata (Lab 1)\TempFuktData.csv"); // FUNGERAR


//SQLTasks.TempAndHumidity();

//Console.WriteLine();
//Menu.ShowDates();


SQLTasks.SortByTemp();








//ExcelData.ReadFileAndUploadToDB(@"F: \Users\Simon\Documents\Drive\Arkitektur av applikationer i.NET C#\Väderdata (Lab 1)\Testdata.csv");
//SQLTasks.TempAndHumidity();
//Menu.ShowDates();
//SQLTasks.TemperatureByDay(); // Fungerar
//SQLTasks.LowestTemperature();

//SQLTasks.AddToDataBase(); // Fungerar

// FUNGERAR
//var test = ExcelData.ReadExcelFile<ExcelData>(@"F:\Users\Simon\Documents\Drive\Arkitektur av applikationer i .NET C#\Väderdata (Lab 1)\Testdata.csv");
//var validData = ExcelData.VerifyWeatherDataSet<ExcelData>(test);
// FUNGERAR


//var csv = new ExcelData();


//ExcelData.ReadExcelFile(@"F:\Users\Simon\Documents\Drive\Arkitektur av applikationer i .NET C#\Väderdata (Lab 1)\TempFuktData.csv");

//SQLTasks.InsertDataIntoSQLServerUsingSQLBulkCopy(SQLTasks.GetDataTabletFromCSVFile(@"F:\Users\Simon\Documents\Drive\Arkitektur av applikationer i .NET C#\Väderdata (Lab 1)\Testdata.csv"));

//SQLTasks.Test();

//SQLTasks.FileCSVBulk();

//SQLTasks.FileToDatabase();

//ExcelData test = new ExcelData();

//test.ReadExcelFile();

//SQLTasks.CSVtoDatabase();

//SQLTasks.ReadData();


//using (var streamReader = new StreamReader(@"F:\Users\Simon\Documents\Drive\Arkitektur av applikationer i .NET C#\Väderdata (Lab 1)\TempFuktData.csv"))
//{
//    using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
//    {
//        var records = csvReader.GetRecords<ExcelData>().ToList();

//    }

//}


