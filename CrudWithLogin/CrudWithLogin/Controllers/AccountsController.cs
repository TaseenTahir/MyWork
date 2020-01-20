using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudWithLogin.Models;
using System.Web.Security;

namespace CrudWithLogin.Controllers
{
    public class AccountsController : Controller
    {
        CRUD_OpeationContainer db = new CRUD_OpeationContainer();
        // GET: Accounts
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User loginForm)
        {
            bool loginCheck = db.Users.Any(x => x.EmailAddress == loginForm.EmailAddress && x.Password == loginForm.Password);
            if (loginCheck)
            {
                FormsAuthentication.SetAuthCookie(loginForm.EmailAddress, false);
                return Redirect("/Products/Index");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Email Password");
                return View();
            }
           
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User SignUpForm)
        {
            db.Users.Add(SignUpForm);
            db.SaveChanges();
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("Login");
        }
    }
}