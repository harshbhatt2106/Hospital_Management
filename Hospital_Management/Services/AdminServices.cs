using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Hospital_Management.Models;

namespace Hospital_Management.Services
{
    public class AdminServices : IAdminService
    {
        private readonly HospitalDbContext _context;
        public AdminServices(HospitalDbContext hospitalDbContext)
        {
            _context = hospitalDbContext;
        }

        public int adminCount()
        {
            return _context.Users.Count();
        }


        public User GetAdmin(int adminID)
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

        public bool UpdatePasswordfAdmin(int id, string newpassword, string? password = null, bool IsForget = false)
        {

            if (!IsForget)
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
            }

            try
            {
                var data = _context.Users.FirstOrDefault(x => x.UserID == id);
                if (data == null)
                    return false;

                data.Password = newpassword;
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}
