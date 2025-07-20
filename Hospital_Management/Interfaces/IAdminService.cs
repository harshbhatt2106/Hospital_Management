using Hospital_Management.Models;

namespace Hospital_Management.Interfaces
{
    public interface IAdminService
    {
        //bool Logout(int adminID);
        //bool AddAdmin(Admin admin);
        //bool UpdateAdmin(Admin admin);
        //bool DeleteAdmin(Admin admin);
        

        bool UpdatePasswordofAdmin(int id, string password, string newpassword);
        User GetAdmin(int adminID);
        int adminCount();
    }
}
