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
        public static int GetCurrentUserID()
        {
            int? userID = _contextAccessor?.HttpContext?.Session.GetInt32("UserID");
            return userID ?? 0;
        }
        public static int GetOPT()
        {
            int? otp = _contextAccessor?.HttpContext?.Session.GetInt32("AdminOTP");
            return otp ?? 0;
        }

        public static void ClearUserSession()
        {
            _contextAccessor?.HttpContext?.Session.Remove("UserID");
        }
    }
}
