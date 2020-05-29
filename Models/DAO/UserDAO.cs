using Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Models.ViewModel;

namespace Models.DAO
{

    public class UserDAO
    {
        OnlineShopDbContext dbContext = null;
        public UserDAO()
        {
            dbContext = new OnlineShopDbContext();
        }
   
        

        // Lấy thông tin đăng User theo Id
        public User GetByID(string username)
        {
            return dbContext.Users.SingleOrDefault(x => x.Username == username);
        }

        // Kiểm tra đăng nhập
        public int Login(string username, string password)
        {
            var result = dbContext.Users.SingleOrDefault(x => x.Username == username);
            // Check có tài khoản đấy không
            if (result == null)
            {
                return 0; // Không tồn tại tài khoản
            }
            else
            {
                // Check quyền
                if (result.TypeOfAccount == 0)
                {
                    // Check trạng thái có bị khóa không ?
                    if (result.Status == false)
                    {
                        return -1; // Tài khoản bị khóa
                    }
                    else
                    {
                        // Kiểm tra mật khẩu
                        if (result.Password.Trim() == password)
                        {
                            return 1; // Đăng nhập thành công
                        }
                        else return -2; // Sai mật khẩu
                    }
                }
                else
                {
                    return 0; // Không có quyền -> Tài khoản không tồn tại
                }
                
            }
        }
        

        // Lấy ra danh sách sản phẩm
        public IEnumerable<Product> products(int page, int pageSize)
        {
            return dbContext.Products.OrderByDescending(x => x.ProductCode).ToPagedList(page, pageSize);
        }

        // Lấy ra các tài khoản admin
        public IEnumerable<User> admins(int page, int pageSize)
        {

            return dbContext.Users.OrderBy(x => x.TypeOfAccount).ToPagedList(page, pageSize);//.Where(x => x.TypeOfAccount == 0)
        }

        // Lấy ra danh sách Order
        public IEnumerable<Order> orders(int page, int pageSize)
        {
            return dbContext.Orders.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        // Lấy ra danh sách các tài khoản Member
        public IEnumerable<User> users(int page, int pageSize)
        {
            return dbContext.Users.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }
        

    }
    
}
