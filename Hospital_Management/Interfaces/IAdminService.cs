using Hospital_Management.Models;

namespace Hospital_Management.Interfaces
{
    public interface IAdminService
    {
        //bool Logout(int adminID);
        //bool UpdateAdmin(Admin admin);
        //bool DeleteAdmin(Admin admin);



        bool AddAdmin(User admin);
        List<User> GetAdmins();
        bool UpdatePasswordfAdmin(int id, string newpassword,string password);
        bool UpdateUserForgetPassword(string Gmail, string password);

        User GetAdminByID(int adminID);

        bool AdminEmailValid(string userData);

        int adminCount();
    }
}
