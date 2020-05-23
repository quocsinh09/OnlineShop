using System.Web;

namespace OnlineShop.Common
{
    public static class SessionHelper
    {
        public static void SetSession(UserLogin userSession)
        {
            HttpContext.Current.Session["loginSession"] = userSession;
        }

        public static UserLogin GetSession()
        {
            var session = HttpContext.Current.Session["loginSession"];
            return session as UserLogin;
        }
    }
}