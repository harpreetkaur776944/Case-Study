using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FedChoice_Bank.Models;
using System.Data.Entity;

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

                var getAccountId = cs.Account.OrderByDescending(m => m.AccountId).ToList().FirstOrDefault();
                int temp = getAccountId.AccountId;

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

                account.AccountId = ++temp;
                account.Crdate = DateTime.Now.Date;
                account.Duration = 5;
                account.CrlastDate = DateTime.Now.AddYears(5).Date; 
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


        /*
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
        */

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

        public IActionResult DetailsCustomerId(int? Id1, string type)
        {
            AccountDbContext cs = new AccountDbContext();

            var value = cs.Account.Where(m => m.CustomerId == Id1 && m.AccountType == type).FirstOrDefault();

            if (Id1 == null)
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
                ViewBag.message = "The Record " + Id + " is Deleted Successfully.";

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

        public IActionResult Deposit(int? Id)
        {
            AccountDbContext cs = new AccountDbContext();

            var value = cs.Account.Where(m => m.AccountId == Id).FirstOrDefault();

            if (Id == null)
            {
                ViewBag.ErrorMessage = "Account ID not Found";
                return View();
            }

            if (value == null)
            {
                ViewBag.ErrorMessage = "Account ID not Found";
                return View();
            }
            return View("Deposit", value);
        }

        public IActionResult Deposit1(Account account, int id, int val)
        {
            AccountDbContext db = new AccountDbContext();

            db.Account.Find(id).Balance += val;

            db.SaveChanges();


            var value = db.Account.Find(id);

            StatusDbContext statusDbContext = new StatusDbContext();
            var temp = statusDbContext.Status.Where(m => m.AccountId == id).FirstOrDefault();
            temp.Message = "Amount Deposited";
            temp.LastUpdated = DateTime.Now;
            statusDbContext.SaveChanges();

            return View("DepositAfter1", value);



        }

        public IActionResult DepositAfter1()
        {
            return View();
        }

        public IActionResult Withdraw(int? Id)
        {
            AccountDbContext cs = new AccountDbContext();

            var value = cs.Account.Where(m => m.AccountId == Id).FirstOrDefault();

            if (Id == null)
            {
                ViewBag.ErrorMessage = "Account ID not found";
                return View();
            }

            if (value == null)
            {
                ViewBag.ErrorMessage = "Account ID not found";
                return View();
            }
            return View("Withdraw", value);
        }

        public IActionResult Withdraw1(Account account, int id, int val)
        {
            AccountDbContext db = new AccountDbContext();

            db.Account.Find(id).Balance -= val;

            db.SaveChanges();
            var value = db.Account.Find(id);

            StatusDbContext statusDbContext = new StatusDbContext();
            var temp = statusDbContext.Status.Where(m => m.AccountId == id).FirstOrDefault();
            temp.Message = "Amount Withdrawed";
            temp.LastUpdated = DateTime.Now;
            statusDbContext.SaveChanges();

            return View("WithdrawAfter1", value);



        }

        public IActionResult WithdrawAfter1()
        {
            return View();
        }

        public IActionResult Transfer(int? Id)
        {
            AccountDbContext cs = new AccountDbContext();

            var value = cs.Account.Where(m => m.AccountId == Id).FirstOrDefault();

            if (Id == null)
            {
                ViewBag.ErrorMessage = "Account ID not Found";
                return View();
            }

            if (value == null)
            {
                ViewBag.ErrorMessage = "Account ID not Found";
                return View();
            }
            return View("Transfer", value);
        }

        public IActionResult Transfer1(Account account, int id1, int? id2, int val)
        {
            AccountDbContext db = new AccountDbContext();

            var find = db.Account.Where(m => m.AccountId == id2).FirstOrDefault();

            if (id2 == null)
            {
                ViewBag.ErrorMessage = "Account ID not Found";
                return View("Transfer");
            }

            if (find == null)
            {
                ViewBag.ErrorMessage = "Account ID not Found";
                return View("Transfer");
            }

            db.Account.Find(id1).Balance -= val;
            db.Account.Find(id2).Balance += val;


            db.SaveChanges();
            var value1 = db.Account.Find(id1);
            var value2 = db.Account.Find(id2);

            StatusDbContext statusDbContext = new StatusDbContext();
            var temp = statusDbContext.Status.Where(m => m.AccountId == id1).FirstOrDefault();
            temp.Message = "Amount Tranferred";

            temp.LastUpdated = DateTime.Now;

            statusDbContext.SaveChanges();
            return View("TransferAfter1", value1);



        }

        public IActionResult TransferAfter1()
        {
            return View();
        }

        public IActionResult SearchForDeposit()
        {
            return View();
        }

        public IActionResult SearchForWithdraw()
        {
            return View();
        }

        public IActionResult SearchForTransfer()
        {
            return View();
        }

    }
}