using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FedChoice_Bank.Models;

namespace FedChoice_Bank.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
        public IActionResult Create(Account account)
        {
            AccountDbContext cs = new AccountDbContext();
            StatusDbContext statusDbContext = new StatusDbContext();

            if (ModelState.IsValid == true)
            {
                var check = cs.Account.Where(x => x.AccountId == account.AccountId).FirstOrDefault();

                if (check != null)
                {
                    ViewBag.ErrorMessage = "Account ID  already exists.";
                    ModelState.Clear();
                    return View();
                }
                CustomerDbContext db = new CustomerDbContext();
                var verify = db.Customer.Where(x => x.CustomerId == account.CustomerId).FirstOrDefault();

                if (verify == null)
                {
                    ViewBag.ErrorMessage = "Customer ID Does not exists.";
                    ModelState.Clear();
                    return View();
                }

                var repeat = cs.Account.Where(x => x.CustomerId == account.CustomerId && x.AccountType == account.AccountType).FirstOrDefault();
                
                if (repeat != null)
                {
                    ViewBag.ErrorMessage = "Customer already have this type of account.";
                    ModelState.Clear();
                    return View();
                }

               
                cs.Add(account);
                cs.SaveChanges();
                ViewBag.message = "Account " + account.AccountId + " is Saved Successfully.";
                var statusUpdate = statusDbContext.Status.Where(x => x.CustomerId == account.CustomerId).FirstOrDefault();
                statusUpdate.AccountId = account.AccountId;
                statusUpdate.AccountType = account.AccountType;
                statusUpdate.Message = "Account is created";
                statusUpdate.Status1 = "Active";
                statusUpdate.LastUpdated = DateTime.Now;
                statusDbContext.SaveChanges();
                ModelState.Clear();
                return View();
            }


            return View();
        }


        public IActionResult Details(int? Id)
        {
            AccountDbContext cs = new AccountDbContext();


            if (Id == null)
            {
                ViewBag.ErrorMessage = "Account ID is not present";
                return View();
            }

            var value = cs.Account.Where(m => m.AccountId == Id).FirstOrDefault();
            if (value == null)
            {
                ViewBag.ErrorMessage = "Account ID is not present";
                return View();
            }
            else
            {
                return View(value);
            }
        }

        public IActionResult DetailsCustomerId(int? Id, string type)
        {
            AccountDbContext cs = new AccountDbContext();

            var value = cs.Account.Where(m => m.CustomerId == Id && m.AccountType == type).FirstOrDefault();

            if (Id == null)
            {
                ViewBag.ErrorMessage = "Customer ID not Found";
                return View();
            }

            if (value == null)
            {
                ViewBag.ErrorMessage = "Customer ID Or AccountType not present ";
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



        public IActionResult Delete(int Id, string type)
        {
            AccountDbContext db = new AccountDbContext();
            StatusDbContext statusDbContext = new StatusDbContext();
            var value = db.Account.Where(m => m.AccountId == Id && m.AccountType == type).FirstOrDefault();
            if (value == null)
            {
                ViewBag.ErrorMessage = "Account ID not Found";
                return View();
            }
            else
            {
                db.Account.Remove(value);
                db.SaveChanges();
                ViewBag.message = "The Record " + Id + "is Deleted Successfully.";

                var statusUpdate = statusDbContext.Status.Where(m => m.AccountId==Id && m.AccountType == type).FirstOrDefault();
                statusUpdate.Message = "Account is deleted.";
                statusUpdate.Status1 = "Closed";
                statusUpdate.LastUpdated = DateTime.Now;
                statusDbContext.SaveChanges();
                return View(value);

            }
        }


        public IActionResult AccountStatus()
        {
            StatusDbContext statusDbContext = new StatusDbContext();
            return View(statusDbContext.Status.ToList());
        }


    }
}