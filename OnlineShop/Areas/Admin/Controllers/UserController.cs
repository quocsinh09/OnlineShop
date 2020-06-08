using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.EF;
using OnlineShop.Common;

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
        public ActionResult Account(string username)
        {
            var user = new UserDAO().GetByID(username);
            Session.Add(CommonConstants.USER_SESSION, user);
            return View();
        }
        public ActionResult MemberAccount(string username)
        {
            var user = new UserDAO().GetByID(username);
            Session.Add(CommonConstants.USER_SESSION, user);
            ViewBag.Review = new ReviewDAO().GetByUsername(username);
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
                user.Password = Crypto.MD5Hash(user.Password);
                int added = dao.InsertBy(user, (User)Session[CommonConstants.ADMIN_SESSION]);
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
            var dao = new UserDAO();
            var result = dao.DeleteAccount(username);
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

        public ActionResult Edit(Guid id)
        {
            return View(new UserDAO().ViewDetail(id));
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            user.ModifiedBy = ((User)Session[CommonConstants.ADMIN_SESSION]).Name;
            var result = new UserDAO().Update(user);
            if (!result)
            {
                ModelState.AddModelError("", "Cập nhật thông tin không thành công");
            }
            return RedirectToAction("Index");
        }

    }
}