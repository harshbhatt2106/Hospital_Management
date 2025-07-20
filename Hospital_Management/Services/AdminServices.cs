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

        public bool UpdatePasswordofAdmin(int id, string password,string newpassword)
        {
            var oldpassword = _context.Users
                             .Where(x => x.UserID == id)
                             .Select(x => x.Password)
                             .FirstOrDefault();


            if (password != oldpassword)
            {
                return false;
            }
            else
            {
                try
                {
                    var data = _context.Users.FirstOrDefault(x => x.UserID == id);
                        if (data == null)
                            return false;

                        data.Password = newpassword;
                        _context.SaveChanges();

                }
                catch(Exception e)
                {
                    throw;
                }
                return true;
            }
        }
    }
}
