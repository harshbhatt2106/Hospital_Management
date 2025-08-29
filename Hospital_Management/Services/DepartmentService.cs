using Hospital_Management.CommonMethod;
using Hospital_Management.Interfaces;
using Hospital_Management.Models;
using System.Data.SqlClient;

namespace Hospital_Management.Services
{

    public class DepartmentService : IDepartmentService
    {
        private readonly HospitalDbContext hospitalDbContext;

        public DepartmentService(HospitalDbContext hospitalDbContext)
        {
            this.hospitalDbContext = hospitalDbContext;            
        }
        private List<Department> _departments = new();

        public List<Department> departments()
        {
            _departments.Clear();
            string procedure = "SP_GetAllDepartment";
            using (SqlDataReader reader = DBHelper.ExecuteReder(procedure))
            {
                while (reader.Read())
                {
                    Department d = new Department()
                    {
                        DepartmentId= Convert.ToInt32(reader["DepartmentID"]),
                        DepartmentName = reader["DepartmentName"].ToString(),
                        Description = reader["Description"].ToString(),
                        Modified = Convert.ToDateTime(reader["TimeOnly"]),
                        IsActive = (bool)reader["IsActive"]
                    };
                    _departments.Add(d);
                }
            }
            return _departments;
        }
        public bool AddDepartment(Department department)
        {
            string procedure = "SP_Add_Department";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@DepartmentName", department.DepartmentName),
                new SqlParameter("@Description", department.Description),
                new SqlParameter("@Modified", DateTime.Now),
                new SqlParameter("@UserID", department.UserId)
            };
            bool isAdded = DBHelper.ExecuteNonQuery(procedure, parameters);
            //ReloadDepartments();
            return isAdded;
        }
        public bool CheckDepartment(string DepartmentName)
        {
            SqlDataReader? reader = null;
            try
            {
                string procedure = "SP_Department_IsExits";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@DName",DepartmentName.Trim().ToUpper())
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
        public bool DeleteDepartment(int departmentId)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@DID",departmentId)
            };
            string Procedure = "SP_Delete_Department";
            bool Isdeleted = DBHelper.ExecuteNonQuery(Procedure, sqlParameters);
            ReloadDepartments();
            return Isdeleted;
        }
        private void ReloadDepartments()
        {
            _departments = departments();
        }
        public bool UpdateDepartment(Department department)
        {
            string procedure = "SP_Update_Departement";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter ("@Description",department.Description),
                new SqlParameter("@DepartmentName",department.DepartmentName),
                new SqlParameter("@DepartmentID",department.DepartmentId),
            };
            bool isUpdated = DBHelper.ExecuteNonQuery(procedure, sqlParameters);
            return isUpdated;
        }
        public bool setDepartmentStatus(int departmentID)
        {
            string procedure = "SP_ChangeActiveStatus_Department";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter ("@DID",departmentID)
            };
            bool isUpdated = DBHelper.ExecuteNonQuery(procedure, sqlParameters);
            ReloadDepartments();
            return isUpdated;
        }

        public int CountDepartments()
        {
            string procedure = "SP_COUNT_DEPARTMENT";
            int departemntCount = DBHelper.ExecuteScaler(procedure);
            return departemntCount;
        }

        public List<Department> SearchDepartmentByDoctorID(int DoctorID)
        {
            List<Department> departments = new List<Department>();
            var Department = hospitalDbContext.DoctorDepartments
                .Where(dd => dd.DoctorId == DoctorID)
                .Include(d=>d.Department)
                .Select(d=>d.Department)
                .ToList();

            departments = Department;
            return departments;
        }
    }
}
