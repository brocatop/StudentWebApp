using StudentDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentService.Controllers
{
    public class StudentController : ApiController
    {
        public IEnumerable<Student> Get()
        {
            using (StudentsEntities e = new StudentsEntities())
            {
                return e.Students.ToList();
            }
        }

        public Student Get(int id)
        {
            using (StudentsEntities e = new StudentsEntities())
            {
                return e.Students.FirstOrDefault(s => s.StudentID == id);
            }
        }
    }
}
