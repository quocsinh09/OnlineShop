using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace OnlineShop.Common
{
    [Serializable]
    public class UserLogin
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
    }
}