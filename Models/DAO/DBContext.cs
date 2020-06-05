using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class DBContext
    {
        public static SqlConnection sqlConnection;
        public static void openConnect()
        {
            sqlConnection = new SqlConnection("data source=.;initial catalog=OnlShopCNPM;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
            sqlConnection.Open();
        }
    }
}
