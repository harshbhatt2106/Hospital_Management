using Hospital_Management.Models;
using System.Data.SqlClient;

namespace Hospital_Management.CommonMethod
{
    public static class Helper_Method
    {
        public static List<Department> departmentsList = new List<Department>();
        public static Admin CheckLogin(string password, string name)
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
                    Admin admin = new Admin();
                    admin.UserID = Convert.ToInt32(reader["UserId"]);
                    return admin;
                }
                return null;
            }
        }
        //public static bool CheckDepartmentExitsOrNot(string DepartmentName)
        //{
        //    //SqlDataReader? reader = null;
        //    //try
        //    //{
        //    //    string procedure = "SP_Department_IsExits";
        //    //    SqlParameter[] parameters = new SqlParameter[]
        //    //    {
        //    //        new SqlParameter("@DName",DepartmentName.ToUpper())
        //    //    };
        //    //    reader = DBHelper.ExecuteReder(procedure, parameters);
        //    //    if (reader.HasRows)
        //    //    {
        //    //        return true;
        //    //    }
        //    //    else
        //    //    {
        //    //        return false;
        //    //    }
        //    //}
        //    //finally
        //    //{
        //    //    reader.Close();
        //    //}
        //}

        public static List<Department> GetDepartmentList()
        {
            SqlDataReader? reader = null;
            try
            {
                string procedure = "SP_GetAllDepartment";

                reader = DBHelper.ExecuteReder(procedure);
                while (reader.Read())
                {
                    Department d = new Department()
                    {
                        DepartmentID = Convert.ToInt32(reader["DepartmentID"]),
                        DepartmentName = reader["DepartmentName"].ToString(),
                        Description = reader["Description"].ToString(),
                        Modified = Convert.ToDateTime(reader["TimeOnly"])
                    };
                    departmentsList.Add(d);
                }
                return departmentsList;
            }
            finally
            {
                reader.Close();
            }
        }
    }
}
