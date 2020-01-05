using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ApiPaginationDemo
{
    public interface IStudentRepository
    {
        List<Student> GetStudents(Guid instructorId);
        PageResponse<Student> GetStudents(GetStudentsRequest request);
    }

    public class StudentRepository : IStudentRepository
    {
        private static IQueryable<Student> _students;

        static StudentRepository()
        {
            if (_students == null)
            {
                var json = File.ReadAllText("students.json");
                var stream = File.OpenRead("students.json");
                _students = JsonSerializer.Deserialize<List<Student>>(json).AsQueryable();
            }
        }

        public List<Student> GetStudents(Guid instructorId)
        {
            return _students
                .Where(student => student.InstructorId == instructorId)
                .ToList();
        }

        public PageResponse<Student> GetStudents(GetStudentsRequest request)
        {
            var limit = request.PageSize;
            var offset = limit * (request.Page - 1);

            var query = _students.Where(student => student.InstructorId == request.InstructorId);

            var totalCount = query.Count();
            var data = query.Skip(offset).Take(limit).ToList();

            return new PageResponse<Student>(request, data, totalCount);
        }
    }
}