using Models.EF;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Models.DAO
{
    public class OrderDAO
    {
        OnlineShopDbContext dbContext = null;
        public OrderDAO()
        {
            dbContext = new OnlineShopDbContext();
        }

        // Lấy ra danh sách Order
        public IEnumerable<Order> orders(int page, int pageSize)
        {
            return dbContext.Orders.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        public List<Order> ListNewOrder(int top)
        {
            return dbContext.Orders.OrderByDescending(x => x.OrderDate).Take(top).ToList();
        }
        
        public IEnumerable<Order> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = dbContext.Orders;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ID.ToString().Contains(searchString) || x.ID.ToString().Contains(searchString));
            }

            return model.OrderByDescending(x => x.OrderDate).ToPagedList(page, pageSize);
        }
        /// <summary>
        /// Get list product by category
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public IEnumerable<OrderViewModel> List( int pageIndex = 0, int pageSize = 15)
        {
            //totalRecord = dbContext.Orders.Where(x => x.CategoryID == categoryID).Count();
            var model = (from u in dbContext.Users
                         join o in dbContext.Orders
                         on u.ID equals o.CustomerID into OrderUser
                         from o in OrderUser
                         join od in dbContext.OrderDetails
                         on o.ID equals od.OrderID
                         
                        select new
                         {
                             ID = o.ID,
                             CustomerID = o.CustomerID,
                             Email = o.Email,
                             Mobile  = o.Mobile,
                             OrderDate = o.OrderDate,
                             ShippedDate = o.ShippedDate,
                             Address = o.Address,
                             Status = o.Status,
                             CostBy =  o.CostBy,
                             ChangeStatus = o.ChangeStatus,
                             Name =  u.Name,
                             BirthDay = u.BirthDay,
                             CreatedDate = u.CreatedDate,
                             ModifiedDate  = u.ModifiedDate,
                             AmountMissOrder = u.AmountMissOrder,
                             ProductCode = od.ProductCode,
                             ProductName = od.ProductName,
                             MetaTitle = od.MetaTitle,
                             Amount = od.Amount,
                             Price = od.Price
                         }).AsEnumerable().Select(x => new OrderViewModel()
                         {
                             ID = x.ID,
                             CustomerID = x.CustomerID,
                             Email = x.Email,
                             Mobile = x.Mobile,
                             OrderDate = x.OrderDate,
                             ShippedDate = x.ShippedDate,
                             Address = x.Address,
                             Status = x.Status,
                             CostBy = x.CostBy,
                             ChangeStatus = x.ChangeStatus,
                             Name = x.Name,
                             BirthDay = x.BirthDay,
                             CreatedDate = x.CreatedDate,
                             ModifiedDate = x.ModifiedDate,
                             AmountMissOrder = x.AmountMissOrder,
                             ProductCode = x.ProductCode,
                             ProductName = x.ProductName,
                             MetaTitle = x.MetaTitle,
                             Amount = x.Amount,
                             Price = x.Price
                         });

            return model.OrderByDescending(x => x.OrderDate).ToPagedList(pageIndex, pageSize);   //.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            //return model.ToList();
        }
        //public List<OrderViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        //{
        //    totalRecord = dbContext.Orders.Where(x => x.Name == keyword).Count();
        //    var model = (from a in dbContext.Orders
        //                 join b in dbContext.OrderCategories
        //                 on a.CategoryID equals b.ID
        //                 where a.Name.Contains(keyword)
        //                 select new
        //                 {
        //                     CateMetaTitle = b.MetaTitle,
        //                     CateName = b.Name,
        //                     CreatedDate = a.CreatedDate,
        //                     ID = a.ID,
        //                     Images = a.Image,
        //                     Name = a.Name,
        //                     MetaTitle = a.MetaTitle,
        //                     Price = a.Price
        //                 }).AsEnumerable().Select(x => new OrderViewModel()
        //                 {
        //                     CateMetaTitle = x.MetaTitle,
        //                     CateName = x.Name,
        //                     CreatedDate = x.CreatedDate,
        //                     ID = x.ID,
        //                     Images = x.Images,
        //                     Name = x.Name,
        //                     MetaTitle = x.MetaTitle,
        //                     Price = x.Price
        //                 });
        //    model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //    return model.ToList();
        //}
        ///// <summary>
        ///// List feature product
        ///// </summary>
        ///// <param name="top"></param>
        ///// <returns></returns>
        //public List<Order> ListFeatureOrder(int top)
        //{
        //    return dbContext.Orders.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        //}
        //public List<Order> ListRelatedOrders(long productId)
        //{
        //    var product = dbContext.Orders.Find(productId);
        //    return dbContext.Orders.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).ToList();
        //}

        //public void UpdateImages(long productId, string images)
        //{
        //    var product = dbContext.Orders.Find(productId);
        //    product.MoreImages = images;
        //    dbContext.SaveChanges();
        //}
        //public Order ViewDetail(long id)
        //{
        //    return dbContext.Orders.Find(id);
        //}
    }
}
