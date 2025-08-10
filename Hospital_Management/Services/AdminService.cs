namespace Hospital_Management.Services
{
    public class AdminService : IAdminService
    {

        private readonly HospitalDbContext _context;
        private readonly ILogger _loger;

        public AdminService(HospitalDbContext hospitalDbContext, ILogger<AdminService> logger)
        {
            _context = hospitalDbContext;
            _loger = logger;
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

        public int AdminAuthanticate(string UserName, string Password)
        {
            try
            {
                var adminID = _context.Users.
                                      Where(x => x.UserName.ToLower() == UserName.ToLower()
                                      && (x.Password.ToLower() == Password.ToLower()))
                                        .Select(x => x.UserID)
                                        .FirstOrDefault();

                if (adminID != 0)
                {
                    return adminID;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                _loger.LogError("Error by AdiminAuthaticate");
                throw;
            }
        }

        public int adminCount()
        {
            try
            {
                return _context.Users.Count();
            }
            catch
            {
                throw;
            }
        }
        
        public bool AdminEmailValid(string userData)
        {
            try
            {
                bool a = _context.Users.Any(u => u.Email == userData);
                return a;
            }
            catch
            {
                throw;
            }
        }
        public User GetAdminByID(int adminID)
        {

            User? admin = _context.Users.Find(adminID);

            if (admin != null)
            {
                return admin;
            }
            else
            {
                throw new Exception("Admin not Found");
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
            if (existingUser != null)
            {
                existingUser.UserName = admin.UserName;
                existingUser.Email = admin.Email;
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
