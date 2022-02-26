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

                // Works but crashes??? On SqlTask

                //var builder = new ConfigurationBuilder()
                //        .AddJsonFile($"appsettings.json", true, true);
                //string connectionString =
                //builder.Build().GetConnectionString("DefaultConnection");
                //optionsBuilder.UseSqlServer(connectionString);

                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WeatherData;Trusted_Connection=True;");
                //optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WeatherData;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        
    }
}


