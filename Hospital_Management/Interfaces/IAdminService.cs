using Hospital_Management.Models;

namespace Hospital_Management.Interfaces
{
    public interface IAdminService
    {
        //bool Logout(int adminID);
        //bool DeleteAdmin(Admin admin);

        int AdminAuthanticate(string UserName, string Password);
        int UpdateAdmin(User admin,int UserID);
        bool AddAdmin(User admin);
        List<User> GetAdmins();
        User GetAdminByID(int adminID);
        bool AdminEmailValid(string userData);
        int adminCount();
    }
}
