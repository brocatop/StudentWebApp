using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using StudentDataAccess;
using StudentService.Classes;

namespace StudentService.Controllers
{
    public class LoginController : Controller
    {

        LoginHelper loginHelper = new LoginHelper();
        StudentsEntities s = new StudentsEntities();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }


        [System.Web.Mvc.HttpPost]
        public ActionResult Login(Credential credential)
        {
            Credential c = null;

            if (ModelState.IsValid)
            {
                c = s.Credentials.FirstOrDefault(u => u.Username == credential.Username && u.Userpassword == credential.Userpassword);

                if (c != null)
                {
                    loginHelper.SetAuthenticationToken(c.Username, true, c);
                    return RedirectToAction("Index", "Student");
                }
                else
                {

                }
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public ActionResult Register(Credential c)
        {

            if (ModelState.IsValid)
            {
                s.Credentials.Add(c);
                s.SaveChanges();

                return RedirectToAction("Login");
            }
            else
            {
                return View("Register");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}