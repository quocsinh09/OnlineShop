using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class RecentActivityViewModel
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


        public int ProductName { get; set; }

        public Guid UserID { get; set; }

        public string Text { get; set; }

        public double ReviewPoint { get; set; }

        public DateTime? AddDate { get; set; }


        public string Name { get; set; }

        public DateTime? BirthDay { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? AmountMissOrder { get; set; }

        public string username { get; set; }

    }
}
