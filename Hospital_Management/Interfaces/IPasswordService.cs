using Hospital_Management.Models;

namespace Hospital_Management.Interfaces
{
    public interface IPasswordService
    {
        bool UpdatePasswordfAdmin(int id, string newpassword, string password, string EnterdOldPassword);
        bool UpdateUserForgetPassword(string Gmail, string password);


        
    }
}
