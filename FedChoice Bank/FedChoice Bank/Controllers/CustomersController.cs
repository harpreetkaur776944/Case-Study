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
            return View();
            //return View(entities.Customer.ToList());
        }

        public IActionResult Search()
        {
            return View();

        }
        public IActionResult Create()
        {
            return View();
        }


        /* Create new Customer */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer cust)
        {
            CustomerDbContext cs = new CustomerDbContext();
            StatusDbContext statusDbContext = new StatusDbContext();

            if (ModelState.IsValid == true)
            {
                var check = cs.Customer.Where(x => x.CustomerSsn == cust.CustomerSsn).FirstOrDefault();
                if (check == null)
                {
                    cs.Add(cust);
                    cs.SaveChanges();
                    string message = "The Record " + cust.CustomerName + " is Saved Successfully.";
                    ViewBag.message = message;
                    ModelState.Clear();

                    /* For Customer Status */
                    Status status = new Status();
                    status.CustomerId = cust.CustomerId;
                    status.CustomerSsnid = cust.CustomerSsn;
                    status.Message = "Customer is Created.";
                    status.LastUpdated = DateTime.Now;
                    status.Status1 = "Pending";
                    statusDbContext.Add(status);
                    statusDbContext.SaveChanges();

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

        public IActionResult CustomerStatus()
        {
            StatusDbContext statusDb = new StatusDbContext();
            return View(statusDb.Status.ToList());
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
            StatusDbContext statusDbContext = new StatusDbContext();

            var value = db.Customer.Where( m=> m.CustomerId == Id).FirstOrDefault();

            if (value == null)
            {
                ViewBag.ErrorMessage = "Customer ID not Found.";
                return View();
            }
            else
            {
                try
                {
                    db.Customer.Remove(db.Customer.Find(Id));
                    db.SaveChanges();
                    ViewBag.message = "The Customer " + Id + "is Deleted Successfully.";

                    var customer = statusDbContext.Status.Where(m => m.CustomerId == Id).FirstOrDefault();
                    customer.Message = "Customer is deleted.";
                    customer.LastUpdated = DateTime.Now;
                    customer.Status1 = "Closed";
                    statusDbContext.SaveChanges();

                    return View(value);
                }
                catch(DbUpdateException ex)
                {
                    return Redirect("DeleteSearch");
                }
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
                ViewBag.ErrorMessage = "Customer SSNID not Found.";
                return View("EditSearch");
            }

            if (value == null)
            {
                ViewBag.ErrorMessage = "Customer SSNID not Found.";
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
            StatusDbContext statusDbContext = new StatusDbContext();

            db.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            ViewBag.message = "Customer is Updated Successfully.";

            var cust = statusDbContext.Status.Where(m => m.CustomerId == customer.CustomerId).FirstOrDefault();
            cust.Message = "Customer is updated.";
            cust.LastUpdated = DateTime.Now;
            statusDbContext.SaveChanges();

            // return View("EditSearch");
            return View();
        }


    }
}