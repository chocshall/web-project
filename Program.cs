
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using web_project.Models;

namespace web_project
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
           

            // need this service added otherwise cant show data of the database or work with it in website
            builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer("Server=DESKTOP-PHRR6KQ;Database=SchoolDb;Trusted_Connection=True;TrustServerCertificate=True;"));

            // this is so identity context would work and  would be able register, login in local web
            builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=DESKTOP-PHRR6KQ;Database=SchoolDb;Trusted_Connection=True;TrustServerCertificate=True;"));
            
            builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                 .AddEntityFrameworkStores<AppDbContext>();


            builder.Services.AddAuthorization();

            builder.Services.AddSwaggerGen(c =>
            {
                // added config for swagger gen so it would create a authorize button which you would paste token for authorization
                // so yould could access api protected by it
               
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    // shows users how to format the token 
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    // where to put the token
                    
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    // what kind of token
                    BearerFormat = "JWT"
                });

                // shows the authorize button in top right 
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                
                    {
                        
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}  // Empty array means no specific scopes required
                    }
        });
            });


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var schoolContext = services.GetRequiredService<SchoolContext>();

                   
                    // Now add the data
                    if (!schoolContext.Students.Any())
                    {
                        var grade1 = new Grade { GradeName = "1st Grade" };
                        var grade2 = new Grade { GradeName = "2nd Grade" };
                        var grade3 = new Grade { GradeName = "3rd Grade" };

                        schoolContext.Grades.AddRange(grade1, grade2, grade3);
                        schoolContext.SaveChanges();

                        var students = new[]
                        {
                            new Student { FirstName = "Yash", LastName = "Malhotra", GradeId = grade1.GradeId, Grade = grade1 },
                            new Student { FirstName = "John", LastName = "Doe", GradeId = grade2.GradeId, Grade = grade2 },
                            new Student { FirstName = "Jane", LastName = "Smith", GradeId = grade2.GradeId, Grade = grade2 },
                            new Student { FirstName = "Bob", LastName = "Johnson", GradeId = grade3.GradeId, Grade = grade3 },
                            new Student { FirstName = "Alice", LastName = "Williams", GradeId = grade3.GradeId, Grade = grade3 }
                        };

                        schoolContext.Students.AddRange(students);
                        schoolContext.SaveChanges();

                    }

                    // Identity tables checks if created 
                    var appContext = services.GetRequiredService<AppDbContext>();
                    appContext.Database.EnsureCreated();

                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error: {ex.Message}");
                }
            }

            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            // order matters
            app.UseAuthentication();
            app.UseAuthorization();

            
            // adds login features register, login , refresh
            app.MapIdentityApi<IdentityUser>();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
