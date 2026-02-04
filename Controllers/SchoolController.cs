using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_project.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace web_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly SchoolContext _context;

        // used for accessing and working with the data of the database 
        public SchoolController(SchoolContext context)
        {
            _context = context;
        }


        // GET: api/<SchoolController>
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            // then can access the 
            var students = _context.Students.ToList();
            return students;
            //return new string[] { "value1", "value2" };
        }
        
        // GET api/<SchoolController>/5
        [HttpGet("{id}")]
        public IEnumerable<Student> Get(int id)
        {
            var elemByName = _context.Students.Where(item => item.Grade.GradeName.StartsWith(Convert.ToString(id)));
            return elemByName;
        }

        // POST api/<SchoolController>
        [HttpPost]
        public void Post([FromBody] DataOfStudent serverSideStudentData)
        {
            var grdNew = new Grade() { GradeName = $"{serverSideStudentData.GradeName}st Grade" };
            var stdNew = new Student() { FirstName = serverSideStudentData.FirstName, LastName = serverSideStudentData.LastName, Grade = grdNew };
            _context.Students.Add(stdNew);
            _context.SaveChanges();
            return;
        }

        // PUT api/<SchoolController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            
        }

        // DELETE api/<SchoolController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var elemById = _context.Students.Where(item => item.GradeId == id);
            
            foreach (var item in elemById)
            {
                _context.Students.Remove(item);
                
            }
            _context.SaveChanges();
        }
    }
}
