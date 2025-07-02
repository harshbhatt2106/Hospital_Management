using Hospital_Management.CommonMethod;
using Hospital_Management.Interfaces;
using Hospital_Management.Models;
using System.Data.SqlClient;

namespace Hospital_Management.CommonMethod_Class
{

    public class DepartmentServices : IDepartmentService
    {

        //private readonly IDepartmentService _departmentService;

        private List<Department> _departments = new();

        //public DepartmentServices(IDepartmentService departmentService)
        //{
        //    _departmentService = departmentService;
        //}


        public List<Department> departments()
        {
            SqlDataReader? reader = null;
            try
            {
                _departments.Clear();
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
                    _departments.Add(d);
                }
                return _departments;
            }
            finally
            {
                reader?.Close();
            }
        }

        public bool AddDepartment(Department department)
        {
            string procedure = "SP_Add_Department";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@DepartmentName", department.DepartmentName),
                new SqlParameter("@Description", department.Description),
                new SqlParameter("@Modified", DateTime.Now),
                new SqlParameter("@UserID", department.UserID)
            };
            bool i = DBHelper.ExecuteNonQuery(procedure, parameters);
            if (i)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckDepartment(string DepartmentName)
        {
            SqlDataReader? reader = null;
            try
            {
                string procedure = "SP_Department_IsExits";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@DName",DepartmentName.ToUpper())
                };
                reader = DBHelper.ExecuteReder(procedure, parameters);
                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                reader?.Close();
            }
        }




        //public bool DeleteDepartment(Department department)
        //{
        //    return _departmentService.DeleteDepartment(department);
        //}


        //public void ReloadDepartments()
        //{
        //    _departmentService.ReloadDepartments();
        //}

        //public bool UpdateDepartment(Department department)
        //{
        //    return _departmentService.UpdateDepartment(department);
        //}
    }
}
