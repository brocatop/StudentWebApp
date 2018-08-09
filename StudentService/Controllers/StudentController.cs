using StudentDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;

namespace StudentService.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        StudentsEntities std = new StudentsEntities();
 
       
        // GET: Student
        public ActionResult Index(string order, string search)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(order) ? "Students" : "";
            ViewBag.DataSortParm = order == "Major" ? "First Name" : "Major";
            IQueryable<Student> stu = std.Students;
            List<Student> studentList = stu.ToList();

            if (!String.IsNullOrEmpty(search))
            {
                studentList = stu.Where(s => s.LastName.Contains(search)
                                       || s.FirstName.Contains(search)
                                       || s.Major.Contains(search)).ToList();
                return View(studentList);
            }

            switch (order)
            {
                case "First Name":
                    studentList = stu.OrderByDescending(s => s.FirstName).ToList();
                    break;
                case "Major":
                    studentList.OrderByDescending(s => s.Major);
                    break;
                case "Last Name":
                    studentList.OrderByDescending(s => s.LastName);
                    break;
            }
            return View(studentList);
        }

        public ActionResult Create()
        {
            return View();
        }


        public ActionResult Edit(int id)
        {
            List<Student> studentList = std.Students.ToList();
            var student = studentList.Where(s => s.StudentID == id).FirstOrDefault();

            return View(student);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = std.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        public ActionResult Delete(FormCollection fcNotUsed, int id = 0)
        {
            Student student = std.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            std.Students.Remove(student);
            std.SaveChanges();
            return RedirectToAction("Home");
        }

        [HttpPost]
        public ActionResult Edit(Student std)
        {
            //write code to update student 

            return RedirectToAction("Home");
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {

            if (ModelState.IsValid)
            {
                std.Students.Add(student);
                std.SaveChanges();

                return RedirectToAction("Home");
            }
            else
            {
                return View(student);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }

        // This has no functionality with a control yet. However, it should be the proper logic for sorting the students
        /*
        public ActionResult OrderBy(string order)
        {
                       List<Student> studentList = std.Students.ToList();
            return View(studentList);
        } */
    }
}