using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class ReviewDAO
    {
        OnlineShopDbContext dbContext = null;
        public ReviewDAO()
        {
            dbContext = new OnlineShopDbContext();
        }

        public List<Review> GetByUsername(string username)
        {
            var user = new UserDAO().GetByID(username);
            return dbContext.Reviews.Where(x => x.UserID == user.ID).OrderBy(x => x.CreatedDate).ToList();
        }
    }
}
