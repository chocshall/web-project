using Microsoft.AspNetCore.Authorization;
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
        
        [HttpGet("Students")]
        public IEnumerable<StudentDTO> Get()
        {

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
        [Authorize]
        [HttpGet("Students/ByGrade")]
        public IEnumerable<StudentDTO> Get(string gradeSearchString)
        {
            var studentsByGrade = _context.Students.Include(Students => Students.Grade).Where(item => item.Grade.GradeName.StartsWith(gradeSearchString)).ToList();

            List<StudentDTO> studentsListByGrade = new List<StudentDTO>();
            foreach (var item in studentsByGrade)
            {
                StudentDTO studentDTO = new StudentDTO();
                studentDTO.FirstName = item.FirstName;
                studentDTO.LastName = item.LastName;
                studentDTO.GradeName = item.Grade.GradeName;
                studentsListByGrade.Add(studentDTO);
            }
            return studentsListByGrade;

            
        }

        // POST api/<SchoolController>
        [Authorize]
        [HttpPost("Student/Add")]
        public void Post([FromBody] DataOfStudent serverSideStudentData)
        {
            // check if such grade already exists
            // fix so it checks for if data gotten is 1 check for 12 not correct
            var checkGotDataExists = _context.Grades.Where(item => item.GradeName.StartsWith($"{serverSideStudentData.GradeName}")).ToList();
           
          
            if (checkGotDataExists.Count() < 1)
            {
                // if not created, create a new one.
                var grdNew = new Grade() { GradeName = $"{serverSideStudentData.GradeName} Grade" };
                var newlyMadeGradeList = _context.Grades.Where(item => item.GradeName.StartsWith($"{serverSideStudentData.GradeName}")).ToList();
                // then checks if it  was created.
                if (newlyMadeGradeList.Count() >= 1)
                {
                    // if true add student data and save
                    var stdNew = new Student() { FirstName = serverSideStudentData.FirstName, LastName = serverSideStudentData.LastName, Grade = grdNew };
                    _context.Students.Add(stdNew);
                   
                    _context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Error no gradeName found, could not be created");
                }
            }

            else
            {
                // if already exists
                if (checkGotDataExists.Count() >= 1)
                {
                    // use the existing grade data of its name, but because id is unique
                    // the id of grade gets created automatically by specificat
                    var stdNew = new Student() { FirstName = serverSideStudentData.FirstName, LastName = serverSideStudentData.LastName, Grade = new Grade() { GradeName = checkGotDataExists[0].GradeName } };
                    _context.Students.Add(stdNew);
                    
                    _context.SaveChanges();
                    
                }
                else
                {
                    Console.WriteLine("Error no gradeName found, could not be created");
                }
            }
            return;
        }

        // PUT api/<SchoolController>/5
        [HttpPut("Student/{id}/Update")]
        public void Put(int id, [FromBody] string value)
        {
            
        }

        // DELETE api/<SchoolController>/5
        [Authorize]
        [HttpDelete("Student/{id}")]
        public void Delete(int id)
        {
            var student = _context.Students.Single(item => item.StudentId == id);
            
           _context.Students.Remove(student); 
           
            _context.SaveChanges();
        }
    }
}
