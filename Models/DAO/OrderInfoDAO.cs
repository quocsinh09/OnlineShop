using Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Models.DAO
{

    public class OrderInfoDAO
    {
        OnlineShopDbContext dbContext = null;
        public OrderInfoDAO()
        {
            dbContext = new OnlineShopDbContext();
        }

        // Thêm sản phẩm
        public int Insert(Product product)
        {
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            return product.ProductCode;
        }

        // Cập nhật sản phẩm | Sửa/Xóa
        public bool Update(Product entity)
        {
            try
            {
                var product = dbContext.Products.Find(entity.ProductCode);
                product.ProductCode = entity.ProductCode;
                product.Name = entity.Name;
                product.MetaTitle = entity.MetaTitle;
                product.Description = entity.Description;
                product.MetaDescription = entity.MetaDescription;
                product.ImageFirst = entity.ImageFirst;
                product.ImageSecond = entity.ImageSecond;
                product.Price = entity.Price;
                product.PercentSale = entity.PercentSale;
                product.Promotion = entity.Promotion;
                product.CategoryIDParent = entity.CategoryIDParent;
                product.CategoryIDChild = entity.CategoryIDChild;
                product.CreatedDate = DateTime.Now;
                product.Quanity = entity.Quanity;
                product.Status = entity.Status;
                product.BuyCount = entity.BuyCount;
                product.ViewCount = entity.ViewCount;
                product.ReviewPoint = entity.ReviewPoint;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                return false;

            }
        }

        // Xem thông tin chi tiết
        public Product ViewDetail(int Id)
        {
            return dbContext.Products.Find(Id);
        }

        // Lấy thông tin User theo Id
        public User GetUserByID(Guid Id)
        {
            return dbContext.Users.SingleOrDefault(x => x.ID == Id);
        }

       
        // Lấy ra danh sách các tài khoản Member
        public IEnumerable<User> users(int page, int pageSize)
        {
            return dbContext.Users.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }



    }

}
