using Hospital_Management.CommonMethod;
using System.Data.SqlClient;

namespace Hospital_Management.Models
{
    public class Admin
    {
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
        public int IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public static List<Admin> admins = new List<Admin>() { };

        public static void GetAllAdmin()
        {
            admins.Clear();
            string procedure = "SP_GetAllData_User";
            SqlDataReader ?reader = null;
            try
            {
                reader = DBHelper.ExecuteReder(procedure);
                while (reader.Read())
                {
                    Admin admin = new Admin()
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        Password = reader["Password"].ToString(),
                        Email = reader["Email"].ToString(),
                        MobileNo = reader["MobileNo"].ToString(),
                        IsActive = Convert.ToInt32(reader["IsActive"]),
                        Created = Convert.ToDateTime(reader["Created"]),
                        Modified = Convert.ToDateTime(reader["Modified"])
                    };
                    admins.Add(admin);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Generate In GetAdmin data", ex);
            }
            finally
            {
                reader.Close();
            }
        }
    }
}
