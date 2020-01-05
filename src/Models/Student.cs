using System;

namespace ApiPaginationDemo
{
    public class Student
    {
        public int Id { get; set; }
        public Guid InstructorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
