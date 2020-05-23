using Models;
using Models.DAO;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {   
        // GET: Admin/Home
        public ActionResult Index()
        {
            var order = new HomeViewModel();
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                //var model = order.numberOfMember();
                return View(order);
            }
        }
        [HttpGet]
        public ActionResult Account()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }
        }
    }
}