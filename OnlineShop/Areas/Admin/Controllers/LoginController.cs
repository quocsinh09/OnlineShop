using Models.DAO;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        // GET
        public ActionResult Index()
        {
            Session["Admin"] = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel loginModel)
        {

            if (ModelState.IsValid)
            {
                var dataAccessObj = new UserDAO();
                var result = dataAccessObj.Login(loginModel.UserName, Crypto.MD5Hash(loginModel.Password));
                if (result == 1)
                {
                    var user = dataAccessObj.GetByID(loginModel.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.Username;
                    userSession.ID= user.ID;

                    Session.Add(CommonConstants.USER_SESSION, userSession);

                    Session["Admin"] = user;

                    return RedirectToAction("Index", "Home");
                }
                else if(result == 0)
                {
                    ModelState.AddModelError("", "Tai khoan khong ton tai");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tai khoan dang bi khoa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mat khau khong dung");
                }
                
                else
                {
                    ModelState.AddModelError("", "Mat khau khong dung");
                }
            }
            return View("Index");
        }
    }
}