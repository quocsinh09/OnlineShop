using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(int page = 1,  int pageSize = 10)
        {
            var dataAccessObj = new UserDAO();
            var model = dataAccessObj.products(page, pageSize);
            return View(model);
        }


        // GET: Admin/Product/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDAO();
                int id = dao.Insert(product);
                if (id > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm sản phẩm không thành công");
                    //return View("Index");
                }
            }
            return View(product);
            
        }


        // GET: Admin/ProductCategory/Edit/5
        public ActionResult Edit(int id)
        {
            var dataAccessObj = new ProductDAO();
            var product = dataAccessObj.ViewDetail(id);
            return View(product);
        }
        // POST: Admin/Product/Create
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDAO();
                var result = dao.Update(product);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật sản phẩm không thành công");
                }
            }
            return View(product);

        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new ProductDAO().Remove(id);
            return RedirectToAction("Index");
        }
    }
}
