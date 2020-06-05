using Models.EF;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using System.Data.SqlClient;

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
        
        public IEnumerable<OrderViewModel> List( int pageIndex = 1, int pageSize = 10)
        {
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
                            Price = od.Price,
                            username = u.Username
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
                            Price = x.Price,
                            username = x.username
                        });

            return model.OrderByDescending(x => x.OrderDate).ToPagedList(pageIndex, pageSize);   //.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            //return model.ToList();
        }
        public SqlDataReader sqlConnect(string select)
        {
            SqlCommand sqlCommand = new SqlCommand(select);
            sqlCommand.CommandType = System.Data.CommandType.Text;
            sqlCommand.Connection = Models.DAO.DBContext.sqlConnection;
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            return dataReader;
        }
        public string getTotal(int month)
        {
            SqlDataReader dataReader = sqlConnect("SELECT sum(od.Amount*od.Price) as total from [OnlShopCNPM].[dbo].[Order] as o join [OnlShopCNPM].[dbo].[OrderDetail] as od on o.ID = od.OrderID where MONTH(o.OrderDate) = " + month + "  and o.Status = 'complete' ");
            string result = "";
            while (dataReader.Read())
            {
                result = dataReader["total"].ToString();
            }
            return result; 
        }
    }
}
