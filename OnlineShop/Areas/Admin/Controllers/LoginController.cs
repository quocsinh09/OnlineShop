using Common;
using Models.DAO;
using Models.EF;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
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
                    ModelState.AddModelError("Username", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("Username", "Tài khoản đang bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("Password", "Mật khẩu không đúng");
                }
                
            }
            return View("Index");
        }
        public ActionResult ForgotPassword() 
        {
            return View("ForgotPassword");
        }
        
        [HttpPost]

        public ActionResult ForgotPassword(ForgotPasswordModel entity)
        {
            var user = new UserDAO().GetByMail(entity.Email);
            entity.Password = "1234567";

            string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/ClientTemplate/NewOrder.html"));

            content = content.Replace("{{CustomerName}}", user.Name);
            content = content.Replace("{{Phone}}", user.Mobile);
            content = content.Replace("{{Email}}", user.Email);
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

            new MailHelper().SendMail(user.Email, "Đơn hàng mới từ OnlineShop", content);
            new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);
            return View("Index");
        }  
    }
}