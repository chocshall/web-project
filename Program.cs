
using Microsoft.EntityFrameworkCore;
using web_project.Controllers;
using web_project.Models;

namespace web_project
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).
               AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

            using (var context = new SchoolContext())
            {
               
                ////creates db if not exists 
                context.Database.EnsureCreated();

                ////create entity objects
                var grd1 = new Grade() { GradeName = "1st Grade" };
                var std1 = new Student() { FirstName = "Yash", LastName = "Malhotra", Grade = grd1 };

                var elem = context.Students.Where(item => item.FirstName.StartsWith("C"));


                var optionsOne = 1;
                var optionsTwo = 2;

                Console.WriteLine($"{optionsOne}. add people to the Students table");
                Console.WriteLine($"{optionsTwo}. find people that are in second grade");
                Console.WriteLine();

                Console.WriteLine("Type a number ");
                var input = Convert.ToInt32(Console.ReadLine());
                if (input == optionsOne)
                {
                    Console.WriteLine("Type Name and LastName");
                    var name = Console.ReadLine();
                    var Lastname = Console.ReadLine();
                    var grd3 = new Grade() { GradeName = "2st Grade" };
                    var std3 = new Student() { FirstName = name, LastName = Lastname, Grade = grd3 };
                    context.Students.Add(std3);
                    context.SaveChanges();
                }

                if (input == optionsTwo)
                {
                    //var elemByGradeTwo = context.Grades.Where(item => item.GradeName.StartsWith("2")).Select(item => item.GradeId).ToList();

                    // easier way with accesing the grade not using lists checking with contain if the items 
                    // is there or not
                    var elemByName = context.Students.Where(item => item.Grade.GradeName.StartsWith("2"));

                    foreach (var item in elemByName)
                    {
                        Console.WriteLine(item.FirstName + " " + item.LastName);
                    }

                }

            }

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // need this service added otherwise cant show data of the database or work with it in website
            builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer("Server=DESKTOP-PHRR6KQ;Database=SchoolDb;Trusted_Connection=True;TrustServerCertificate=True;"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
