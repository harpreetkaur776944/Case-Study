using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FedChoice_Bank.Models;
namespace FedChoice_Bank.Controllers
{
    public class CustomersController : Controller
    {
        CustomerDbContext cs = new CustomerDbContext();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer cust)
        {
            if (ModelState.IsValid == true)
            {
                cs.Add(cust);
                cs.SaveChanges();       
                ViewBag.message = "The Record " + cust.CustomerName + "is Saved Successfully........";
                ModelState.Clear();
                return View();
            }
            else
            {
                ViewBag.ErrorMessage = "Incorrect Details";
                return View();
            }  
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }
    }
}