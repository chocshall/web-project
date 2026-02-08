using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_project.Models;
using web_project.ResponseModels;

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
        public IEnumerable<StudentDTO> Get()
        {


            // then can access the 
            //so it wont become null include makes  this work because it tells to check more data in this example with grade
            var students = _context.Students.Include(Students => Students.Grade).ToList();
            // .
            List<StudentDTO> studentsList = new List<StudentDTO>();
            foreach (var item in students)
            {
                StudentDTO studentDTO = new StudentDTO();
                studentDTO.FirstName = item.FirstName;
                studentDTO.LastName = item.LastName;
                studentDTO.GradeName = item.Grade.GradeName;
                studentsList.Add(studentDTO);
            }
            return studentsList;
            //return new string[] { "value1", "value2" };
        }
        
        // GET api/<SchoolController>/5
        [HttpGet("Students")]
        public IEnumerable<Student> Get(string gradeSearchString)
        {
            var elemByName = _context.Students.Where(item => item.Grade.GradeName.StartsWith(gradeSearchString));
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
        [HttpDelete("Student/{id}")]
        public void Delete(int id)
        {
            var student = _context.Students.Single(item => item.StudentId == id);
            
           _context.Students.Remove(student);
           
            _context.SaveChanges();
        }
    }
}
