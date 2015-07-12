using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcSales.Models;
using DAL.DBModel;

namespace MvcSales.Controllers
{
    public class ServiceController : Controller
    {
        //SalesContext db = new SalesContext();
        IModelRepository<DAL.ModelsFromEntity.Sales> _sales = new DAL.SalesRepository();
        IModelRepository<DAL.ModelsFromEntity.User> _user = new DAL.UserRepository();
        IModelRepository<DAL.ModelsFromEntity.Manager> _manager = new DAL.ManagerRepository();
        IModelRepository<DAL.ModelsFromEntity.Client> _client = new DAL.ClientRepository();
        IModelRepository<DAL.ModelsFromEntity.Goods> __goods = new DAL.GoodsRepository();
        
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Managers()
        {
            ViewBag.Managers = _manager.Items;
            return View();
        }

        [HttpPost]
        public ActionResult Managers(Manager manager)
        {
            DAL.ModelsFromEntity.Manager man = new DAL.ModelsFromEntity.Manager();
            if (ModelState.IsValid)
            {
                man.FirstName = manager.FirstName;
                man.LastName = manager.FirstName;
                _manager.Add(man);
                _manager.SaveChanges();
            }
            ViewBag.Managers = _manager.Items;
            return View(manager);
        }
        
        // Remove manager for id
        [Authorize(Roles="admin")]
        public ActionResult DeleteManager(int id)
        {
            _manager.Remove(id);
            _manager.SaveChanges();
            return RedirectToAction("Managers");
        }
        
        [HttpGet]
        public ActionResult Clients()
        {
            ViewBag.Clients = _client.Items;
            return View();
        }

        [HttpPost]
        public ActionResult Clients(Client client)
        {
            DAL.ModelsFromEntity.Client cli = new DAL.ModelsFromEntity.Client();
            if (ModelState.IsValid)
            {
                cli.FirstName = client.FirstName;
                cli.LastName = client.LastName;
                _client.Add(cli);
                _client.SaveChanges();
            }
            ViewBag.Clients = _client.Items;
            return View(client);
        }
        
        // Remove client for id
        [Authorize(Roles = "admin")]
        public ActionResult DeleteClient(int id)
        {
            _client.Remove(id);
            _client.SaveChanges();
            return RedirectToAction("Clients");
        }    
         
        [HttpGet]
        public ActionResult Goods()
        {
            ViewBag.Goods = __goods.Items;
            return View();
        }

        [HttpPost]
        public ActionResult Goods(Goods goods)
        {
            DAL.ModelsFromEntity.Goods goo = new DAL.ModelsFromEntity.Goods();
            if (ModelState.IsValid)
            {
                goo.Name = goods.Name;
                goo.Price = goods.Price;
                __goods.Add(goo);
                __goods.SaveChanges();
            }
            ViewBag.Goods = __goods.Items;
            return View(goods);
        }
        
        // Remove goods for id
        [Authorize(Roles = "admin")]
        public ActionResult DeleteGoods(int id)
        {
            __goods.Remove(id);
            __goods.SaveChanges();
            return RedirectToAction("Goods");
        }

        //public JsonResult GetManagerAgePie()
        //{
        //    var salesManager = (from x in db.Sales
        //                           group x by x.Manager.LastName into countManager
        //                           select new { amount = countManager.Count(), manager = countManager.Key }).ToList();
                    
        //    return Json(new { Salemanager = salesManager }, JsonRequestBehavior.AllowGet);
        //}


    }
}
