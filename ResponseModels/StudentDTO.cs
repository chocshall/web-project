using web_project.Models;

namespace web_project.ResponseModels
{
    public class StudentDTO
    {
        //public StudentDTO(Student student)
        //{
        //    FirstName = student.FirstName;
        //    LastName = student.LastName;
        //    GradeName = student.Grade.GradeName;
        //}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GradeName { get; set; }
    }
}
