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
    public class RecentActivityDAO
    {
        OnlineShopDbContext dbContext = null;
        public RecentActivityDAO()
        {
            dbContext = new OnlineShopDbContext();
        }
        public RecentActivityViewModel List(int pageIndex = 1, int pageSize = 10)
        {
            var model = (from orderDetail in dbContext.OrderDetails
                         join order in dbContext.Orders
                         on orderDetail.OrderID equals order.ID into OrderTotal
                         from  orderTotal in OrderTotal
                         join review in dbContext.Reviews
                         on orderTotal.ID equals review.UserID

                         select new
                             {
                                 OrderDate = orderTotal.OrderDate,
                                 ProductNameBuy = orderDetail.ProductName,
                                 Amount = orderDetail.Amount,
                                 ProductCodeReview = review.ProductCode,
                                 Text = review.Text,
                                 ReviewPoint = review.ReviewPoint,
                                 AddDate = review.CreatedDate,
                                 Username = Or
                             }).AsEnumerable().Select(x => new RecentActivityViewModel()
                        {
                            OrderDate = x.OrderDate,
                            ProductNameBuy = x.ProductNameBuy,
                            Amount = x.Amount,
                            ProductCodeReview = x.ProductCodeReview,
                            Text = x.Text,
                            ReviewPoint = x.ReviewPoint,
                            AddDate = x.AddDate,
                        });

            return model.OrderByDescending(x => x.OrderDate).FirstOrDefault();
        }
    }
}
