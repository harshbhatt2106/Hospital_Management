
namespace Hospital_Management.Services
{
    public class AdminService : IAdminService
    {
        private readonly HospitalDbContext _context;
        public AdminService(HospitalDbContext hospitalDbContext)
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

        public int UpdateAdmin(User admin, int userID)
        {
            var existingUser = _context.Users.FirstOrDefault(x => x.UserID == userID);
            if(existingUser!=null)
            {
                existingUser.UserName = admin.UserName;
                existingUser.Email= admin.Email;
                existingUser.MobileNo = admin.MobileNo;
                existingUser.Modified = DateTime.Now;
            }
            try
            {
                _context.Update(existingUser);
                int IsUpdate = _context.SaveChanges();
                return IsUpdate;
            }
            catch
            {
                throw;
            }
        }
    }
}
