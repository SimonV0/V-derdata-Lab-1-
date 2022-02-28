using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Väderdata.DataAccess
{
    public class WeatherDataContext : DbContext
    {
        public WeatherDataContext()
        {
        }

        public WeatherDataContext(DbContextOptions<WeatherDataContext> options) : base(options)
        {
        }
        public DbSet<WeatherModel>? Weather { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {

                // Fungerade inte men lämnar kvar koden iallafall.

                //var builder = new ConfigurationBuilder()
                //        .AddJsonFile($"appsettings.json", true, true);
                //string connectionString =
                //builder.Build().GetConnectionString("DefaultConnection");
                //optionsBuilder.UseSqlServer(connectionString);

                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WeatherData;Trusted_Connection=True;");
                
            }
        }

        
    }
}


