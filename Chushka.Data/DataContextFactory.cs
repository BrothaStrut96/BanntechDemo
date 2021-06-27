using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chushka.Data
{
    public class DataContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Chushka.Web"))
                .AddJsonFile("appsettings.json").Build();

            // Here we create the DbContextOptionsBuilder manually.        
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Build connection string. This requires that you have a connectionstring in the appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            // Create our DbContext.
            return new ApplicationDbContext(builder.Options);
        }
    }
}
