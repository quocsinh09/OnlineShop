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

        
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                int added = dao.InsertBy(user, (User)Session["Admin"]);
                if (added == 1)
                {
                    return RedirectToAction("Index");
                }
                else 
                {
                    ModelState.AddModelError("", dao.AccountError(user, added));
                }
            }
            return View(user);
        }

        [HttpPost]
        public JsonResult Delete(string username)
        {
            var dao = new UserDAO().DeleteAccount(username, ((User)Session["Admin"]).TypeOfAccount);
            return Json(new
            {
                delete = dao
            });
        }

        [HttpPost]
        public JsonResult ChangeStatus(string username)
        {
            var dao = new UserDAO().ChangeStatus(username);
            return Json(new
            {
                status = dao
            });
        }

        [HttpPost]
        public JsonResult ChangeRegency(string username, int regency)
        {
            var dao = new UserDAO().ChangeRegency(username, regency);
            return Json(new
            {
                status = dao
            });
        }
    }
}