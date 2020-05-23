using Models.EF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class OrderViewModel
    {
        public Guid ID { get; set; }

        public Guid CustomerID { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public string Address { get; set; }

        public string Status { get; set; }

        public int? CostBy { get; set; }

        public bool? ChangeStatus { get; set; }


        public int ProductCode { get; set; }

        public string ProductName { get; set; }

        public string MetaTitle { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }

       
        public string Name { get; set; }

        public DateTime? BirthDay { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? AmountMissOrder { get; set; }

    }
}
