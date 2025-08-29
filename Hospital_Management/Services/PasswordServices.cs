using Hospital_Management.Data;
using Hospital_Management.Interfaces;

namespace Hospital_Management.Services
{
    public class PasswordServices : IPasswordService
    {
        
        private readonly HospitalDbContext _context;

        public PasswordServices(HospitalDbContext hospitalDbContext)
        {
            _context = hospitalDbContext;
        }

        public bool UpdatePasswordfAdmin(int id, string newpassword, string password, string EnterdOldPassword)
        {
            // get old password
            var oldpassword = _context.Users
                             .Where(x => x.UserID == id)
                             .Select(x => x.Password)
                             .FirstOrDefault();

            if (EnterdOldPassword != oldpassword)
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
                if (data == null)
                    return false;

                data.Password = password;
                bool result = _context.SaveChanges() > 0;
                return result;
            }
            catch
            {
                throw;
            }
        }

    }
}
