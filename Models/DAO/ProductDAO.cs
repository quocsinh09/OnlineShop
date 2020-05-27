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
        /// <summary>
        /// Get list product by category
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        //public List<ProductViewModel> ListByCategoryId(long categoryID, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        //{
        //    totalRecord = dbContext.Products.Where(x => x.CategoryID == categoryID).Count();
        //    var model = (from a in dbContext.Products
        //                 join b in dbContext.ProductCategories
        //                 on a.CategoryID equals b.ID
        //                 where a.CategoryID == categoryID
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
        //                 }).AsEnumerable().Select(x => new ProductViewModel()
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
        //public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        //{
        //    totalRecord = dbContext.Products.Where(x => x.Name == keyword).Count();
        //    var model = (from a in dbContext.Products
        //                 join b in dbContext.ProductCategories
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
        //                 }).AsEnumerable().Select(x => new ProductViewModel()
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
        //public List<Product> ListFeatureProduct(int top)
        //{
        //    return dbContext.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        //}
        //public List<Product> ListRelatedProducts(long productId)
        //{
        //    var product = dbContext.Products.Find(productId);
        //    return dbContext.Products.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).ToList();
        //}

        //public void UpdateImages(long productId, string images)
        //{
        //    var product = dbContext.Products.Find(productId);
        //    product.MoreImages = images;
        //    dbContext.SaveChanges();
        //}
        //public Product ViewDetail(long id)
        //{
        //    return dbContext.Products.Find(id);
        //}
    }
}
