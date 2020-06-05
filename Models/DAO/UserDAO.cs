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
using System.Configuration;

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
        // Lấy thông tin đăng User theo email
        public User GetByMail(string email)
        {
            return dbContext.Users.SingleOrDefault(x => x.Email == email);
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
                if (result.TypeOfAccount != 1)
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
        
        // Lấy ra các tài khoản admin
        public IEnumerable<User> admins(int page, int pageSize)
        {
            return dbContext.Users.OrderByDescending(x => x.TypeOfAccount).ToPagedList(page, pageSize);//.Where(x => x.TypeOfAccount == 0)
        }

        // Lấy ra danh sách các tài khoản Member
        public IEnumerable<User> users(int page, int pageSize)
        {
            return dbContext.Users.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }


        // Xem thông tin chi tiết
        public User ViewDetail(Guid id)
        {
            return dbContext.Users.Find(id);
        }

        public bool Remove(Guid id)
        {
            try
            {
                var user = dbContext.Users.Find(id);
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        // Cập nhật thông tin | Sửa/Xóa
        public bool Update(User entity, bool regency, User added)
        {
            if (entity.Equals(added) || regency) 
            {
                var user = dbContext.Users.Find(entity.ID);
                if (user != null && regency)
                {
                    try
                    {
                        user.ID = entity.ID;
                        user.Name = entity.Name;
                        user.Address = entity.Address;
                        user.BirthDay = entity.BirthDay;
                        user.Email = entity.Email;
                        user.Mobile = entity.Mobile;
                        user.ModifiedDate = DateTime.Now;
                        user.ModifiedBy = added.Name;
                        dbContext.SaveChanges();
                        return true;
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                // raise a new exception nesting  
                                // the current instance as InnerException  
                                raise = new InvalidOperationException(message, raise);
                            }
                        }
                        throw raise;
                    }  
                }

            }
            return false;
        }

        public bool UpdatePassword(string password, string Username)
        {
            var user = dbContext.Users.SingleOrDefault(x => x.Username == Username);
            if (user != null)
            {
                try
                {
                    user.Password = password;
                    dbContext.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    // *TODO* log exception
                }  
            }
            return false;
        }

        public bool ChangeStatus(string username)
        {
            var user = dbContext.Users.SingleOrDefault(x => x.Username == username);
            if (user != null)
            {
                try
                {
                    user.Status = !user.Status;
                    dbContext.SaveChanges();
                    return user.Status;
                }
                catch (Exception ex)
                {
                    // *TODO* log exception
                }
            }
            return user.Status;
        }
        
        // Thêm tafi khoarn
        public int Insert(User entity)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Username == entity.Username);
            if (user != null && user.ID.ToString() != "")
            {
                return -1;
            }
            else
            {
                try
                {
                    entity.ID = Guid.NewGuid();
                    entity.CreatedDate = DateTime.Now;
                    
                    dbContext.Users.Add(entity);
                    dbContext.SaveChanges();
                    return 1;
                }
                catch (Exception ex)
                {
                    //*Todo* log exception
                }
            }
            return 0;
        }

        // Thêm tafi khoarn
        public int InsertBy(User entity, User Adder)
        {
            var userName = dbContext.Users.FirstOrDefault(x => x.Username == entity.Username);
            if (userName != null && userName.ID.ToString() != "")
            {
                return -1;
            }
            else
            {
                var userEmail = dbContext.Users.FirstOrDefault(x => x.Email == entity.Email);
                if (userEmail != null && userEmail.ID.ToString() != "")
                {
                    return -2;
                }
                else
                {
                    var userPhone = dbContext.Users.FirstOrDefault(x => x.Mobile == entity.Mobile);
                    if (userPhone != null && userPhone.ID.ToString() != "")
                    {
                        return -3;
                    }
                    else
                    {
                        try
                        {
                            entity.ID = Guid.NewGuid();
                            entity.CreatedDate = DateTime.Now;
                            entity.ModifiedDate = DateTime.Now;
                            entity.Status = true;
                            entity.CreatedBy = Adder.Name;
                            entity.ModifiedBy = Adder.Name;
                            dbContext.Users.Add(entity);
                            dbContext.SaveChanges();
                            return 1;
                        }
                        catch (Exception ex)
                        {
                            //*Todo* log exception
                        }
                    }
                }
            }
            return 0;
        }

        //Check tai khoan vua tao
        public string AccountError(User user, int added)
        {
            if (added == -1)
            {
                return "Đã tồn tại tài khoản tên : " + user.Username;
            }
            else if (added == -2)
            {
                return "Đã tồn tại tài khoản có mail : " + user.Email;
            }
            else if (added == -3)
            {
                return "Đã tồn tại tài khoản có sđt : " + user.Mobile;
            }
            else
            {
                return "Thêm tài khoản không thành công";
            }
        }

        // Xoa tai khoan
        public bool DeleteAccount(string username, bool regency)
        {
            if(regency)
            {
                try
                {
                    var user = dbContext.Users.SingleOrDefault(x => x.Username == username);
                    dbContext.Users.Remove(user);
                    dbContext.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }

        //public bool ChangeRegency(string username, int regency)
        //{
        //    try
        //    {
        //        var user = dbContext.Users.SingleOrDefault(x => x.Username == username);
        //        user.TypeOfAccount = regency;
        //        dbContext.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //}
    }
    
}
