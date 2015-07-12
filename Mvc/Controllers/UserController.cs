using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc.Models;

namespace Mvc.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private SalesContext db = new SalesContext();

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Index()
        {
            var users = db.Users.Include(x => x.Role).ToList();
            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            SelectList roles = new SelectList(db.Roles, "Id", "Name");
            ViewBag.Roles = roles;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            SelectList roles = new SelectList(db.Roles, "Id", "Name");
            ViewBag.Roles = roles;

            return View(user);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            User user = db.Users.Find(id);
            SelectList roles = new SelectList(db.Roles, "Id", "Name", user.RoleId);
            ViewBag.Roles = roles;

            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            SelectList roles = new SelectList(db.Roles, "Id", "Name");
            ViewBag.Roles = roles;

            return View(user);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
