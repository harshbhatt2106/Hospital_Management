using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Models;
using System.Reflection.Metadata.Ecma335;

namespace Hospital_Management.Services
{
    public class AdminServices : IAdminService
    {
        private readonly HospitalDbContext _context;
        public AdminServices(HospitalDbContext hospitalDbContext)
        {
            _context = hospitalDbContext;
        }

        public bool AddAdmin(User admin)
        {
            admin.Created = DateTime.Now;
            _context.Users.Add(admin);
            if (_context.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int adminCount()
        {
            return _context.Users.Count();
        }
        public bool AdminEmailValid(string userData)
        {
            bool a = _context.Users.Any(u => u.Email == userData);
            return a;
        }
        public User GetAdminByID(int adminID)
        {
            User admin = _context.Users.Find(adminID);

            if (admin != null)
            {
                return admin;
            }
            else
            {
                return null;
            }
        }

        public List<User> GetAdmins()
        {
            List<User> users = _context.Users.ToList();
            return users;
        }

        public bool UpdatePasswordfAdmin(int id, string newpassword, string password)
        {

            // get old password
            var oldpassword = _context.Users
                             .Where(x => x.UserID == id)
                             .Select(x => x.Password)
                             .FirstOrDefault();

            if (password != oldpassword)
            {
                return false;
            }
            try
            {
                var data = _context.Users.FirstOrDefault(x => x.UserID == id);
                if (data == null)
                    return false;

                data.Password = newpassword;
                bool IsUpdate = _context.SaveChanges() > 0 ? true : false;
                return IsUpdate;
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateUserForgetPassword(string Gmail, string password)
        {
            try
            {
                var data = _context.Users.FirstOrDefault(x => x.Email == Gmail);
                data.Password = password;
                bool result = _context.SaveChanges() > 0 ? true : false;
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
