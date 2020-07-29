using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FedChoice_Bank.Models;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using System.Runtime.CompilerServices;

namespace FedChoice_Bank.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            var entities = new CustomerDbContext();

            return View(entities.Customer.ToList());

        }

        public IActionResult Search()
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
            CustomerDbContext cs = new CustomerDbContext();

            if (ModelState.IsValid == true)
            {
                var check = cs.Customer.Where(x => x.CustomerSsn == cust.CustomerSsn).FirstOrDefault();
                if (check == null)
                {
                    cs.Add(cust);
                    cs.SaveChanges();
                    ViewBag.message = "The Record " + cust.CustomerName + " is Saved Successfully.";
                    ModelState.Clear();
                    return View();
                }

                else
                {
                    ViewBag.message = "Customer SSN ID already exists.";
                    ModelState.Clear();
                    return View();
                }
            }


            return View();
        }

        
        public IActionResult Details(int? Id)
        {
            CustomerDbContext cs = new CustomerDbContext();


            if (Id == null)
            {
                ViewBag.ErrorMessage = "Customer ID IS NOT PRESENT IN DATA, PLEASE FILL CORRECT DATA";
                return View();
            }

            var value = cs.Customer.Where(m => m.CustomerId == Id).FirstOrDefault(); 
            if (value == null)
            {
                ViewBag.ErrorMessage = "Customer ID IS NOT PRESENT IN DATA, PLEASE FILL CORRECT DATA";
                return View();
            }
            else
            {
                return View(value);
            }
        }

        public IActionResult DetailsSSN(int? Id)
        {
            CustomerDbContext cs = new CustomerDbContext();
            
            var value = cs.Customer.Where(m => m.CustomerSsn == Id).FirstOrDefault();

            if (Id == null)
            {
                ViewBag.ErrorMessage = "Customer SSN ID IS NOT PRESENT IN DATA, PLEASE FILL CORRECT DATA";
                return View();
            }

            if (value == null)
            {
                ViewBag.ErrorMessage = "Customer SSN ID IS NOT PRESENT IN DATA, PLEASE FILL CORRECT DATA";
                return View();
            }
            else
            {
                return View(value);
            }
        }



        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }




        public IActionResult DeleteSearch()
        {
            return View();

        }



        public IActionResult Delete(int Id)
        {
            CustomerDbContext db = new CustomerDbContext();
            var value = db.Customer.Where( m=> m.CustomerId == Id).FirstOrDefault();
            if (value == null)
            {
                ViewBag.ErrorMessage = "Customer ID IS NOT PRESENT IN DATA, PLEASE FILL CORRECT DATA";
                return View();
            }
            else
            {   
                db.Customer.Remove(db.Customer.Find(Id));
                db.SaveChanges();
                ViewBag.message = "The Record " + Id + "is Deleted Successfully.";
                return View(value);

            }
        }


        
        public IActionResult EditSearch()
        {
            return View();

        }

        [Route("edit")]
        [HttpGet]
        public IActionResult Edit(int ? Id)
        {
            CustomerDbContext cs = new CustomerDbContext();

            var value = cs.Customer.Where(m => m.CustomerId == Id).FirstOrDefault();

            if (Id == null)
            {
                ViewBag.ErrorMessage = "Customer SSN ID IS NOT PRESENT IN DATA, PLEASE FILL CORRECT DATA";
                return View("EditSearch");
            }

            if (value == null)
            {
                ViewBag.ErrorMessage = "Customer SSN ID IS NOT PRESENT IN DATA, PLEASE FILL CORRECT DATA";
                return RedirectToAction("EditSearch");
            }
            else
            {
                return View("Edit", value);
            }
            
        }

        [Route("edit")]
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            CustomerDbContext db = new CustomerDbContext();
            db.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            ViewBag.message = "The Record is Updated Successfully.";
            return View("EditSearch");
        }


    }
}