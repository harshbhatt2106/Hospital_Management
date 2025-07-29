using Hospital_Management.Models;

namespace Hospital_Management.Interfaces
{
    public interface IAdminService
    {
        //bool Logout(int adminID);
        //bool AddAdmin(Admin admin);
        //bool UpdateAdmin(Admin admin);
        //bool DeleteAdmin(Admin admin);


        
        bool UpdatePasswordfAdmin(int id, string newpassword, string? password, bool IsForget = false);
        
        User GetAdmin(int adminID);

        bool AdminEmailValid(string userData);

        int adminCount();
    }
}
