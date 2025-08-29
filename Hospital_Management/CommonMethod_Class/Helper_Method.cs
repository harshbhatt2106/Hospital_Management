using Hospital_Management.Models;
using System.Data.SqlClient;

namespace Hospital_Management.CommonMethod
{
    public static class Helper_Method
    {

        public static bool CheckIfExists<T>(HospitalDbContext hospitalDbContext, string? gmail = null, int? phone = null) where T : class
        {
            if (!string.IsNullOrEmpty(gmail))
            {
                return hospitalDbContext.Set<T>().Any(e => EF.Property<string>(e, "Email") == gmail);
            }
            if (phone != null)
            {
                return hospitalDbContext.Set<T>().Any(e => EF.Property<int>(e, "MobileNo") == phone);
            }
            return false;
        }

    }
}
