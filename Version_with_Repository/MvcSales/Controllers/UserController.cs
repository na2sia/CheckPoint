using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSales.Models;
using MvcSales.Repository;

namespace MvcSales.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        //private SalesContext db = new SalesContext();
        private IModelRepository<User> _user = new UserRepository();
        private IModelRepository<Role> _role = new RoleRepository();
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Index()
        {
            var users = _user.Items.ToList();
            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            SelectList roles = new SelectList(_role.Items, "Id", "Name");
            ViewBag.Roles = roles;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(User user)
        {
            //DAL.ModelsFromEntity.User us = new DAL.ModelsFromEntity.User();
            if (ModelState.IsValid)
            {
                //us.Login = user.Login;
                //us.Name = user.Name;
                //us.Password = user.Password;
                //us.RoleId = user.RoleId;
                _user.Add(user);
                _user.SaveChanges();
                return RedirectToAction("Index");
            }

            SelectList roles = new SelectList(_role.Items, "Id", "Name");
            ViewBag.Roles = roles;

            return View(user);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            User user = _user.Items.FirstOrDefault(x=>x.Id==id);
            SelectList roles = new SelectList(_role.Items, "Id", "Name", user.RoleId);
            ViewBag.Roles = roles;

            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(User user)
        {
            //DAL.ModelsFromEntity.User us = new DAL.ModelsFromEntity.User();
            if (ModelState.IsValid)
            {
                //us.Login = user.Login;
                //us.Name = user.Name;
                //us.Password = user.Password;
                //us.RoleId = user.RoleId;
                _user.Update(user);
                _user.SaveChanges();
                return RedirectToAction("Index");
            }

            SelectList roles = new SelectList(_role.Items, "Id", "Name");
            ViewBag.Roles = roles;

            return View(user);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            _user.Remove(id);
            _user.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
