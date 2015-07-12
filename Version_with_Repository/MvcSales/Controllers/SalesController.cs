using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using MvcSales.Models;
using DAL.DBModel;
using DAL.ModelsFromEntity;

namespace MvcSales.Controllers
{
    public class SalesController : Controller
    {
        //SalesContext db = new SalesContext();
        IModelRepository<DAL.ModelsFromEntity.Sales> _sales = new DAL.SalesRepository();
        IModelRepository<DAL.ModelsFromEntity.User> _user = new DAL.UserRepository();
        IModelRepository<DAL.ModelsFromEntity.Manager> _manager = new DAL.ManagerRepository();
        IModelRepository<DAL.ModelsFromEntity.Client> _client = new DAL.ClientRepository();
        IModelRepository<DAL.ModelsFromEntity.Goods> __goods = new DAL.GoodsRepository();
        //[Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var user = _user.Items.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
            if (user != null)
            {
                var sales = _sales.Items.OrderByDescending(r => r.Date).ToList();
                List<DAL.ModelsFromEntity.Manager> managers = _manager.Items.ToList();
                managers.Insert(0, new DAL.ModelsFromEntity.Manager { LastName = "Все", Id = 0 });
                ViewBag.Managers = new SelectList(managers, "Id", "LastName");

                List<DAL.ModelsFromEntity.Client> clients = _client.Items.ToList();
                clients.Insert(0, new DAL.ModelsFromEntity.Client { LastName = "Все", Id = 0 });
                ViewBag.Clients = new SelectList(clients, "Id", "LastName");
                
                List<DAL.ModelsFromEntity.Goods> goods = __goods.Items.ToList();
                goods.Insert(0, new DAL.ModelsFromEntity.Goods { Name = "Все", Id = 0 });
                ViewBag.Goods = new SelectList(goods, "Id", "Name");
                return View(sales);
            }
            return RedirectToAction("LogOff", "Account");
        }

        [HttpPost]
        public ActionResult Index(int client, int manager, int _goods)
        {
            IEnumerable<DAL.ModelsFromEntity.Sales> allSales = null;
            if (manager == 0 && client == 0&&_goods==0)
            {
                return RedirectToAction("Index");
            }
            if (manager == 0 && _goods == 0 && client != 0)
            {
               allSales = from sal in _sales.Items//.Include(u => u.Manager).Include(u => u.Client).Include(u=>u.Goods)
                           where sal.ClientId == client
                           select sal;
            }
            else if (manager != 0 && client == 0 && _goods == 0)
            {
               allSales = from sal in _sales.Items//..Include(u => u.Manager).Include(u => u.Client).Include(u => u.Goods)
                           where sal.ManagerId == manager
                           select sal;
            }
            else if (manager == 0 && client == 0 && _goods != 0)
            {
               allSales = from sal in _sales.Items//.Include(u => u.Manager).Include(u => u.Client).Include(u => u.Goods)
                           where sal.GoodsId == _goods
                           select sal;
            }
            else
            {
               allSales = from sal in _sales.Items//.Include(u => u.Manager).Include(u => u.Client).Include(u=>u.Goods)
                           where sal.ManagerId == manager && sal.ClientId == client&&sal.GoodsId==_goods
                           select sal;
            }

            List<DAL.ModelsFromEntity.Manager> managers = _manager.Items.ToList();
            managers.Insert(0, new DAL.ModelsFromEntity.Manager { LastName = "Все", Id = 0 });
            ViewBag.Managers = new SelectList(managers, "Id", "LastName");

            List<DAL.ModelsFromEntity.Client> clients = _client.Items.ToList();
            clients.Insert(0, new DAL.ModelsFromEntity.Client { LastName = "Все", Id = 0 });
            ViewBag.Clients = new SelectList(clients, "Id", "LastName");

            List<DAL.ModelsFromEntity.Goods> goods = __goods.Items.ToList();
            goods.Insert(0, new DAL.ModelsFromEntity.Goods { Name = "Все", Id = 0 });
            ViewBag.Goods = new SelectList(goods, "Id", "Name");

            if (allSales != null)
            {
                return View(allSales.ToList());
            }
            return RedirectToAction("Index");
        }
        //[Authorize(Roles="admin")]
        [HttpGet]
        public ActionResult Create()
        {
            // get curent user
            DAL.ModelsFromEntity.User user = _user.Items.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
            if (user != null)
            {
                ViewBag.Clients = new SelectList(_client.Items, "Id", "LastName");
                ViewBag.Managers = new SelectList(_manager.Items, "Id", "LastName");
                ViewBag.Goods = new SelectList(__goods.Items, "Id", "Name");

                return View();
            }
            return RedirectToAction("LogOff", "Account");
        }

        // create new sales
        [HttpPost]
        public ActionResult Create(MvcSales.Models.Sales sales)
        {
            DAL.ModelsFromEntity.User user = _user.Items.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
            DAL.ModelsFromEntity.Sales sal = new DAL.ModelsFromEntity.Sales();
            if (user == null)
            {
                return RedirectToAction("LogOff", "Account");
            }
            if (ModelState.IsValid)
            {
                sal.Date = DateTime.Now;
                sal.Cost = sales.Cost;
                sal.ClientId = sales.ClientId;
                sal.GoodsId = sales.GoodsId;
                sal.ManagerId = sales.ManagerId;
                _sales.Add(sal);
                _sales.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(sales);
        }
        
        // detailed data on sale
        public ActionResult Details(int id)
        {
            DAL.ModelsFromEntity.Sales sales = _sales.Items.FirstOrDefault(x=>x.Id==id);

            if (sales != null)
            {
                //get client
                sales.Client = _client.Items.Where(m => m.Id == sales.ClientId).First();
                //get manager
                sales.Manager = _manager.Items.Where(m => m.Id == sales.ManagerId).First();
                //get goods
                sales.Goods = __goods.Items.Where(m => m.Id == sales.GoodsId).First();
        
                return PartialView("_Details", sales);
            }
            return View("Index");
        }

        // Remove sales for id
       // [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            //DAL.ModelsFromEntity.Sales sales = d.Find(id);
            _sales.Remove(id);
            _sales.SaveChanges();
            return RedirectToAction("Index");
        }

        //[Authorize]
        public ActionResult Diagram()
        {
            return PartialView("Diagram");
        }

        public JsonResult GetManagerAgePie()
        {
            var salesManager = (from x in _sales.Items
                                group x by x.Manager.LastName into countManager
                                select new { amount = countManager.Count(), manager = countManager.Key }).ToList();

            return Json(new { Salemanager = salesManager }, JsonRequestBehavior.AllowGet);
        }
       
    }
}
