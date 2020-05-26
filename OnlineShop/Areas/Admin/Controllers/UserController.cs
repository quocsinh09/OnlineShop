using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.EF;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            var dataAccessObj = new UserDAO();
            var model = dataAccessObj.admins(1, 5);
            return View(model);
        }
        public ActionResult MemberAccount(string id)
        {
           
            var dataAccessObj = new UserDAO();
            var user = dataAccessObj.GetByID(id);
            Session["Member"] = user;

            return View();
        }
        /*public ActionResult Create(Admin _admin)
        {
            if (ModelState.IsValid)
            {
                var dataAccessObj = new UserDAO();
                int id = dataAccessObj.Insert(_admin);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Succed");
                }
            }
            return View("Index");
            
        }*/
    }
}