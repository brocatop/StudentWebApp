using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using StudentDataAccess;

namespace StudentService.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Credential credential)
        {
            Credential c = null;

            if (ModelState.IsValid)
            {
                using (StudentDataAccess.StudentsEntities e = new StudentDataAccess.StudentsEntities())
                {
                   c = e.Credentials.FirstOrDefault(u => u.Username == credential.Username && u.Userpassword == credential.Userpassword);
                }
                if (c != null)
                {
                    return RedirectToAction("Home", "Student");
                }
                else
                {

                }
            }
            return RedirectToAction("Login", "Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}