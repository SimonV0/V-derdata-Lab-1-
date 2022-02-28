using Väderdata.Core;
using Väderdata.UI;



ExcelData excel = new ExcelData();
excel.ReadFileAndUploadToDB(@"F:\Users\Simon\Documents\Drive\Arkitektur av applikationer i .NET C#\Väderdata (Lab 1)\TempFuktData.csv");
Menu.DisplayAndSelectFromMenu();
