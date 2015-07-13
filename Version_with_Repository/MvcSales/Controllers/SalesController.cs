using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using MvcSales.Models;
using MvcSales.Repository;

namespace MvcSales.Controllers
{
    public class SalesController : Controller
    {
        //SalesContext db = new SalesContext();
        IModelRepository<Sales> _sales = new SalesRepository();
        IModelRepository<User> _user = new UserRepository();
        IModelRepository<Manager> _manager = new ManagerRepository();
        IModelRepository<Client> _client = new ClientRepository();
        IModelRepository<Goods> __goods = new GoodsRepository();
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var user = _user.Items.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
            if (user != null)
            {
                var sales = _sales.Items.OrderByDescending(r => r.Date).ToList();
                List<Manager> managers = _manager.Items.ToList();
                managers.Insert(0, new Manager { LastName = "Все", Id = 0 });
                ViewBag.Managers = new SelectList(managers, "Id", "LastName");

                List<Client> clients = _client.Items.ToList();
                clients.Insert(0, new Client { LastName = "Все", Id = 0 });
                ViewBag.Clients = new SelectList(clients, "Id", "LastName");
                
                List<Goods> goods = __goods.Items.ToList();
                goods.Insert(0, new Goods { Name = "Все", Id = 0 });
                ViewBag.Goods = new SelectList(goods, "Id", "Name");
                return View(sales);
            }
            return RedirectToAction("LogOff", "Account");
        }

        [HttpPost]
        public ActionResult Index(int client, int manager, int _goods)
        {
            IEnumerable<Sales> allSales = null;
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

            List<Manager> managers = _manager.Items.ToList();
            managers.Insert(0, new Manager { LastName = "Все", Id = 0 });
            ViewBag.Managers = new SelectList(managers, "Id", "LastName");

            List<Client> clients = _client.Items.ToList();
            clients.Insert(0, new Client { LastName = "Все", Id = 0 });
            ViewBag.Clients = new SelectList(clients, "Id", "LastName");

            List<Goods> goods = __goods.Items.ToList();
            goods.Insert(0, new Goods { Name = "Все", Id = 0 });
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
            User user = _user.Items.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
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
            User user = _user.Items.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
            Sales sal = new Sales();
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
            Sales sales = _sales.Items.FirstOrDefault(x=>x.Id==id);

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
