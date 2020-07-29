using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FedChoice_Bank.Models;
using Microsoft.AspNetCore.Http;

namespace FedChoice_Bank.Controllers
{
    public class LoginController : Controller
    {

        LoginDbContext loginDbContext = new LoginDbContext();
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Userstore u)
        {
            if(ModelState.IsValid == true)
            {
                var credentials = loginDbContext.Userstore.Where(x => x.Login == u.Login && x.Password == u.Password).FirstOrDefault();
                if(credentials==null)
                {
                    ViewBag.ErrorMessage = "Login Failed";
                    return View();
                }
                else
                {
                    HttpContext.Session.SetString("username", u.Login);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

    }
}
