using Models.EF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class HomeDAO
    {
        OnlineShopDbContext dbContext = null;
        public HomeDAO()
        {
            dbContext = new OnlineShopDbContext();
        }
        public SqlDataReader sqlConnect(string select)
        {
            SqlCommand sqlCommand = new SqlCommand(select);

            sqlCommand.CommandType = System.Data.CommandType.Text;
            sqlCommand.Connection = Models.DAO.DBContext.sqlConnection;
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            return dataReader;
        }

        public int numberOfOrder()
        {
            return dbContext.Orders.Count();
        }
        public int numberOfMember()
        {
            return dbContext.Users.Where(x => x.TypeOfAccount == 1).Count();
        }
        public string totalEarning()
        {
            SqlDataReader dataReader = sqlConnect("select sum(Price)  as totalEarning from OrderDetail");
            int result = 0;
            while (dataReader.Read())
            {
                result = int.Parse(dataReader["totalEarning"].ToString());
            }
            string whatYouWant = result.ToString("#,##0");
            return whatYouWant;
        }
        public int thisWeek()
        {
            SqlDataReader dataReader = sqlConnect("SELECT *from [OnlShopCNPM].[dbo].[Order] where datediff(day, OrderDate,Getdate()) <= 7 ");
            int result = 0;
            while (dataReader.Read()) { result++; }
            return result;
        }

        public User GetByID(string Username)
        {
            return dbContext.Users.SingleOrDefault(x => x.Username == Username);
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

        public bool CheckRegency(int typeOfAccount)
        {
            return typeOfAccount == 2 ? true : false;
        }

    }
}
