namespace Hospital_Management.Interfaces
{
    public interface IEmailservice
    {
        Task<bool> SendEmail(string toEmail, string subject, string body);
    }
}
