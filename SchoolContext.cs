using System.Collections.Generic;
using web_project.Models;
using Microsoft.EntityFrameworkCore;
namespace web_project
{
    public class SchoolContext : DbContext
    {
        // need this for passing the configurior for running service in program.cs
        // otherwise the website doesnt get the data
        public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
        {
        }

        // parametless constructor for working with database connection and adding to the database
        // need it for when using schoolcontext without parameters
        public SchoolContext() { }
        
        // makes the classes entities so framework could work
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        // now the database name and database server info gets configured by the needs by overriding OnConfiguring method
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            //optionsBuilder.UseSqlServer(appConfig.GetConnectionString("DBLocalConnection"));
            optionsBuilder.UseSqlServer("Server=DESKTOP-PHRR6KQ;Database=SchoolDb;Trusted_Connection=True;TrustServerCertificate=True;");

        }
    }
}
