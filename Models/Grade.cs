namespace web_project.Models
{
    public class Grade
    {
        public Grade()
        {
            // initalizes the list   
            Students = new List<Student>();
        }

        public int GradeId { get; set; }
        public string GradeName { get; set; }

        // shows what it is
        public IList<Student> Students { get; set; }
    }
}
