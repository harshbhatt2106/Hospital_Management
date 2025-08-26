using Hospital_Management.Models;
using System.Data.SqlClient;

namespace Hospital_Management.CommonMethod
{
    public static class Helper_Method
    {
        public static User? CheckLogin(string password, string name)
        {
            string procedure = "SP_Check_Login_Credentials";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserName",name),
                new SqlParameter("@Password",password),
            };

            using (var reader = DBHelper.ExecuteReder(procedure, parameters))
            {
                if (reader.Read())
                {
                    User admin = new User();
                    admin.UserID = Convert.ToInt32(reader["UserId"]);
                    return admin;
                }
                return null;
            }
        }

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
