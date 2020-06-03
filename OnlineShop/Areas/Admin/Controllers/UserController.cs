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
        public ActionResult LockUser(string username)
        {
            var dao = new UserDAO().LockUser(username);

            return View("MemberAccount");
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
                int added = dao.Insert(user);
                if (added == 1)
                {
                    return RedirectToAction("Index");
                }
                else if (added == -1)
                {
                    ModelState.AddModelError("", "Đã tồn tại tài khoản tên : " + user.Username);
                }
                else
                {
                    ModelState.AddModelError("", "Voo ly");
                    //return View("Index");
                }
            }
            return View(user);

        }

    }
}