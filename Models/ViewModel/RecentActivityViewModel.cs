using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class RecentActivityViewModel
    {
        public DateTime OrderDate { get; set; }

        public string ProductNameBuy { get; set; }

        public int Amount { get; set; }

        public int ProductCodeReview { get; set; }

        public string Text { get; set; }

        public double ReviewPoint { get; set; }

        public DateTime? AddDate { get; set; }
        public string Username { get; set; }

    }
}
