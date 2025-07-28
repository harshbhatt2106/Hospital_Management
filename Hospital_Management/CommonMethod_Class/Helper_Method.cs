using Hospital_Management.Models;
using System.Data.SqlClient;

namespace Hospital_Management.CommonMethod
{
    public static class Helper_Method
    {
        public static List<Department> departmentsList = new List<Department>();
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
    }
}
