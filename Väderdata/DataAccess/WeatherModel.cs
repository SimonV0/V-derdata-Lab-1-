using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Väderdata.DataAccess
{
    public class WeatherModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Location { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }


    }
}
