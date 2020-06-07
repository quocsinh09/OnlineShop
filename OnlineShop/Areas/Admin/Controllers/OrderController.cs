using Models.DAO;
using Models.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Admin/Oder
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var dataAccessObj = new OrderDAO();
            var model = dataAccessObj.List(page, pageSize);
            return View(model);

        }

        public ActionResult MemberAccount(string username)
        {
            var user = new UserDAO().GetByID(username);
            Session.Add(CommonConstants.USER_SESSION, user);
            ViewBag.Review = new ReviewDAO().GetByUsername(username);
            return View();
        }
        public ActionResult Detail(Guid orderid)
        {
            ViewBag.Order = new OrderDAO().GetById(orderid);
            ViewBag.Customer = new UserDAO().ViewDetail(ViewBag.Order.CustomerID);
            return View(new OrderDAO().ViewDetailById(orderid));
        }

        public ActionResult Member(Guid id)
        {
            ViewBag.Customer = new UserDAO().ViewDetail(id);
            ViewBag.MemberOrder = new OrderDAO().OrderByUserId(id);
            ViewBag.Orders = new OrderDAO().ViewOrderDetail(ViewBag.MemberOrder);
            var getOrder = new OrderDAO();
            return View(getOrder);
        }
    }
}
