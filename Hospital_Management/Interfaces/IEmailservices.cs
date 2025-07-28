namespace Hospital_Management.Interfaces
{
    public interface IEmailservices
    {
        void SendEmailAsync(string toEmail, string subject, string body);
    }
}
