using System.Web;
namespace Hospital_Management.CommonMethod_Class
{
    public static class SessionUtility
    {
        private static IHttpContextAccessor? _contextAccessor;
        public static void Configure(IHttpContextAccessor accessor)
        {
            _contextAccessor = accessor;
        }
        public static int GetAdminID()
        {
            int userID = (int)_contextAccessor.HttpContext.Session.GetInt32("AdminID");
            return userID;
        }
        public static int GetOPT()
        {
            return 0;
        }
      
        public static void ClearUserSession()
        {
            _contextAccessor.HttpContext.Session.Remove("AdminID");
        }
    }
}
