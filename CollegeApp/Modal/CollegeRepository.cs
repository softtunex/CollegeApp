namespace CollegeApp.Modal
{
    public class CollegeRepository
    {
        public static List<Student> Students {  get; set; } = new List<Student>()
                {
                new Student
                {
                    Id = 1,
                    StudentName = "Student 1",
                    StudentEmail = "studentemail.com",
                    Address = "This is address 1"
                },
                new Student
                {
                    Id = 2,
                    StudentName = "Student 2",
                    StudentEmail = "studentemail2.com",
                    Address = "This is address 2"

                }
                };
    
}
}
