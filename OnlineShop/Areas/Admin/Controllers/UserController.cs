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
            if (!CheckRegency) return RedirectToAction("Index", "Home");
            return View(model);
        }
        public ActionResult MemberAccount(string username)
        {
            Session.Add(CommonConstants.USER_SESSION, new UserDAO().GetByID(username));
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
            var result = dao.DeleteAccount(username, CheckRegency);
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

        //[HttpPost]
        //public JsonResult ChangeRegency(string username, int regency)
        //{
        //    var dao = new UserDAO().ChangeRegency(username, regency);
        //    return Json(new
        //    {
        //        status = dao
        //    });
        //}

        public ActionResult Edit(string username)
        {
            var user = new UserDAO().GetByID(username);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {

            var dao = new UserDAO();
            var result = dao.Update(user, CheckRegency, (User)Session[CommonConstants.ADMIN_SESSION]);
            if (result)
            {
                Session.Add(CommonConstants.ADMIN_SESSION, user);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật thông tin không thành công");
            }

            return View(user);

        }
    }
}