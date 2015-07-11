using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Mvc.Models;

namespace Mvc.Controllers
{
    public class ServiceController : Controller
    {
        
        
            SalesContext db = new SalesContext();
        //[Authorize(Roles = "Администратор")]
            [HttpGet]
            public ActionResult Managers()
            {
                ViewBag.Managers = db.Managers;
                return View();
            }

            [HttpPost]
            public ActionResult Managers(Manager manager)
            {
                if (ModelState.IsValid)
                {
                    db.Managers.Add(manager);
                    db.SaveChanges();
                }
                ViewBag.Managers = db.Managers;
                return View(manager);
            }
            // Remove manager for id
            public ActionResult DeleteManager(int id)
            {
                Manager manager = db.Managers.Find(id);
                db.Managers.Remove(manager);
                db.SaveChanges();
                return RedirectToAction("Managers");
            }
            [HttpGet]
            public ActionResult Clients()
            {
                ViewBag.Clients = db.Clients;
                return View();
            }

            [HttpPost]
            public ActionResult Clients(Client client)
            {
                if (ModelState.IsValid)
                {
                    db.Clients.Add(client);
                    db.SaveChanges();
                }
                ViewBag.Clients = db.Clients;
                return View(client);
            }
            // Remove client for id
            public ActionResult DeleteClients(int id)
            {
                Client client = db.Clients.Find(id);
                db.Clients.Remove(client);
                db.SaveChanges();
                return RedirectToAction("Clients");
            }
            
            [HttpGet]
            public ActionResult Goods()
            {
                ViewBag.Goods = db.Goods;
                return View();
            }

            [HttpPost]
            public ActionResult Goods(Goods goods)
            {
                if (ModelState.IsValid)
                {
                    db.Goods.Add(goods);
                    db.SaveChanges();
                }
                ViewBag.Goods = db.Goods;
                return View(goods);
            }
            // Remove goods for id
            public ActionResult DeleteGoods(int id)
            {
                Goods goods= db.Goods.Find(id);
                db.Goods.Remove(goods);
                db.SaveChanges();
                return RedirectToAction("Goods");
            }    
    }

    }
