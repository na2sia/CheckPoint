using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using Mvc.Models;

namespace Mvc.Controllers
{
    public class SalesController : Controller
    {
        SalesContext db = new SalesContext();
        [HttpGet]
        public ActionResult Index()
        {
            User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
            if (user != null)
            {
                var sales = db.Sales.Include(r => r.Manager)  
                                        .Include(r => r.Client)
                                        .Include(r=>r.Goods)
                                        .OrderByDescending(r => r.Date).ToList();

                return View(sales);
            }
            return RedirectToAction("LogOff", "Account");
        }
        [HttpGet]
        public ActionResult Create()
        {
            // get curent user
            User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
            if (user != null)
            {
                ViewBag.Clients = new SelectList(db.Clients, "Id", "LastName");
                ViewBag.Managers = new SelectList(db.Managers, "Id", "LastName");
                ViewBag.Goods = new SelectList(db.Goods, "Id", "Name");

                return View();
            }
            return RedirectToAction("LogOff", "Account");
        }

        // create new sales
        [HttpPost]
        public ActionResult Create(Sales sales)
        {
            User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("LogOff", "Account");
            }
            if (ModelState.IsValid)
            {
                db.Sales.Add(sales);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(sales);
        }
        // detailed data on sale
        public ActionResult Details(int id)
        {
            Sales sales = db.Sales.Find(id);

            if (sales != null)
            {
                //get client
                var client = db.Clients.Where(m => m.Id == sales.ClientId);
                //get manager
                var manager = db.Managers.Where(m => m.Id == sales.ManagerId);
                //get goods
                var goods = db.Goods.Where(m => m.Id == sales.GoodsId);
                return PartialView("_Details", sales);
            }
            return View("Index");
        }
    }
}
