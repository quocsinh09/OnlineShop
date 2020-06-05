using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Models.ViewModel;

namespace Models.DAO
{
    public class ProductDAO
    {

        OnlineShopDbContext dbContext = null;
        public ProductDAO()
        {
            dbContext = new OnlineShopDbContext();
        }

        public List<Product> ListNewProduct(int top)
        {
            return dbContext.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public List<string> ListName(string keyword)
        {
            return dbContext.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }

        // Lấy ra danh sách sản phẩm
        public IEnumerable<Product> products(int page, int pageSize)
        {
            return dbContext.Products.OrderByDescending(x => x.ProductCode).ToPagedList(page, pageSize);
        }

        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = dbContext.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        // Lấy thông tin đăng User theo Id
        public Product GetByID(int id)
        {
            return dbContext.Products.SingleOrDefault(x => x.ProductCode == id);
        }

        // Thêm sản phẩm
        public int Insert(Product entity)
        {
            var product = dbContext.Products.SingleOrDefault(x => x.Name == entity.Name);
            if (product != null && product.ProductCode > 0)
            {
                return 0;
            }
            else{
                try
                {
                    dbContext.Products.Add(entity);
                    dbContext.SaveChanges();
                    return product.ProductCode;
                }
                catch (Exception ex)
                {
                    //*Todo* log exception
                }
            }
            return 1;
        }
         

        // Cập nhật sản phẩm | Sửa/Xóa
        public bool Update(Product entity)
        {
            var product = dbContext.Products.Find(entity.ProductCode);
            if (product != null)
            {
                try
                {
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
                catch (Exception ex)
                {
                    //*TODO* log exception
                }

            }
            return false;
        }

        // Xem thông tin chi tiết
        public Product ViewDetail(int Id)
        {
            return dbContext.Products.Find(Id);
        }

        public bool Remove(int id)
        {
            try
            {
                var product = dbContext.Products.Find(id);
                dbContext.Products.Remove(product);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }   

    }
}
