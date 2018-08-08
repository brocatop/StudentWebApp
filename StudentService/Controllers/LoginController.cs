using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using StudentDataAccess;
using StudentService.Classes;

namespace StudentService.Controllers
{
    public class LoginController : Controller
    {

        LoginHelper loginHelper = new LoginHelper();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        
        [System.Web.Mvc.HttpPost]
        public ActionResult Login(Credential credential)
        {
            Credential c = null;

            if (ModelState.IsValid)
            {
                using (StudentsEntities e = new StudentsEntities())
                {
                   c = e.Credentials.FirstOrDefault(u => u.Username == credential.Username && u.Userpassword == credential.Userpassword);
                }
                if (c != null)
                {
                    loginHelper.SetAuthenticationToken(c.Username, true, c);
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