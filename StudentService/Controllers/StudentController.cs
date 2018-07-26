using StudentDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StudentService.Controllers
{
    public class StudentController : Controller
    {
        StudentsEntities std = new StudentsEntities();
 
       
        // GET: Student
        public ActionResult Index()
        {
            List<Student> studentList = std.Students.ToList();
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
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Student std)
        {
            //write code to update student 

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {

            if (ModelState.IsValid)
            {
                std.Students.Add(student);
                std.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(student);
            }
        }
    }
}