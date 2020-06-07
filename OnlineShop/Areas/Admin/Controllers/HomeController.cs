using Models;
using Models.DAO;
using Models.EF;
using Models.ViewModel;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {   
        // GET: Admin/Home
        public ActionResult Index()
        {
            var order = new OrderDAO();
            var getInfo = new HomeDAO();
            var model = order.List(1, 10);

            if ((User)Session[CommonConstants.ADMIN_SESSION] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                //var model = order.numberOfMember();
                return View(getInfo);
            }
        }
        [HttpGet]
        public ActionResult Account(string username)
        {
            if ((User)Session[CommonConstants.ADMIN_SESSION] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var user = new UserDAO().GetByID(username);
                return View(user);
            }
        }

        public ActionResult ChangePassword(string username)
        {

            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            var dao = new UserDAO();
            if (dao.UpdatePassword(Crypto.MD5Hash(model.NewPassword), model.Username))
            {
                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "Thay đổi mật khẩu không thành công");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid id)
        {
            var user = new UserDAO().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            var dao = new UserDAO();
            user.ModifiedBy = ((User)Session[CommonConstants.ADMIN_SESSION]).Name;
            var result = dao.Update(user);
            if (!result)
            {
                ModelState.AddModelError("", "Cập nhật thông tin không thành công");
            }
            Session.Add(CommonConstants.ADMIN_SESSION, user);
            return RedirectToAction("Index");
        }
    }
}